using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class bomb_controller : MonoBehaviour
{
    UI ui;
    public GameObject bombPrefab;
    public GameObject explodePrefab;
    [SerializeField] float fuseTime = 3f;
    public int bombAmount = 1;
    public int explodeSize = 1;
    public Tilemap tilemap;
    public Tilemap BombTilemap;
    public Tile brick;
    public int remainBomb;
    public int max_bomb = 5;
    public int max_explodeSize = 5;
    [Range(0f, 1f)] public float ItemProb = 0.1f;
    public int lives = 1;
    public int score = 0;
    public int max_lives = 5;
    public float max_ItemProb = 0.5f;
    Vector3 portal_pos;
    audio_manager aud;

    
    void Start()
    {
        loadData();
        aud = FindObjectOfType<audio_manager>();
        ui = FindObjectOfType<UI>();
        portal_pos = randomBrick();
    }

   
    void Update()
    {
        Vector3Int cell = tilemap.WorldToCell(transform.position);
        Vector3 pos = tilemap.GetCellCenterWorld(cell);
        RaycastHit2D hit = Physics2D.Raycast(pos, Vector2.zero);
        bool checkExist = false;
        if (hit.collider != null && hit.collider.CompareTag("Bomb"))
            checkExist = true;

        if (remainBomb > 0 && Input.GetKeyDown(KeyCode.Space) && !checkExist) 
        {
            StartCoroutine(PlaceBomb());
        }
    }

    bool explosion_block(Vector3Int cell, ref bool check)
    {
        Tile tile = tilemap.GetTile<Tile>(cell);
        if (tile != brick && tile != null) return false;
        if (tile == brick)
        {
            explodePrefab.GetComponent<destroy>().isBrick = true;
            tilemap.SetTile(cell, null);
            check = false;
        }
        else explodePrefab.GetComponent<destroy>().isBrick = false;
        explodePrefab.GetComponent<destroy>().spawnItemProb = ItemProb;
        explodePrefab.GetComponent<destroy>().spawnPortalPos = portal_pos;
        Vector3 pos = tilemap.GetCellCenterWorld(cell);
        GameObject explosion = Instantiate(explodePrefab, pos, Quaternion.identity);
        BombTilemap.SetTile(cell, null);
        return true;
    }

   void explode(Vector2 curPos)
    {
        Vector3Int cell = tilemap.WorldToCell(curPos);
        bool check1 = true, check2 = true, check3 = true, check4 = true, dummy = true;
        aud.playSFX(aud.explode);
        explosion_block(cell, ref dummy);
        for (int i = 1; i <= explodeSize; i++)
        {
            if (check1 && !explosion_block(cell + Vector3Int.down * i, ref check1)) check1 = false;
            if (check2 && !explosion_block(cell + Vector3Int.up * i, ref check2)) check2 = false;
            if (check3 && !explosion_block(cell + Vector3Int.right * i, ref check3)) check3 = false;
            if (check4 && !explosion_block(cell + Vector3Int.left * i, ref check4)) check4 = false;
        }

        
    }

    IEnumerator PlaceBomb()
    {
        aud.playSFX(aud.placeBomb);
        Vector3Int cell = tilemap.WorldToCell(transform.position);
        Vector3 pos = tilemap.GetCellCenterWorld(cell);
        BombTilemap.SetTile(cell, brick);
        GameObject bomb = Instantiate(bombPrefab, pos, Quaternion.identity);
        --remainBomb;
        yield return new WaitForSeconds(fuseTime);
        cell = tilemap.WorldToCell(bomb.transform.position);
        pos = tilemap.GetCellCenterWorld(cell);
        Destroy(bomb);
        explode(pos);
        ++remainBomb;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Bomb"))
        {
            collision.isTrigger = false;
        }
    }

    private Vector3 randomBrick()
    {
        List<Vector3Int> brickPositions = new List<Vector3Int>();
        BoundsInt bounds = tilemap.cellBounds;
        foreach (Vector3Int pos in bounds.allPositionsWithin)
        {
            if (tilemap.GetTile(pos) == brick)
            {
                brickPositions.Add(pos);
            }
        }
        Vector3Int randomBrickPosition = brickPositions[Random.Range(0, brickPositions.Count)];
        Vector3 worldPosition = tilemap.GetCellCenterWorld(randomBrickPosition);
        return worldPosition;
    }

    public void IncreaseScrore(int add)
    {
        score += add;
        ui.setScoretext("SCORE: " + score);
    }


    private void loadData()
    {
        score = StaticData.s_score;
        bombAmount = StaticData.s_bombamount;
        remainBomb = bombAmount;
        ItemProb = StaticData.s_lucky;
        explodeSize = StaticData.s_blast;
        GetComponent<movements_controller>().speed = StaticData.s_speed;
        lives = StaticData.s_live;
    }
}




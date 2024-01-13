using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class enemy_lv2_move : MonoBehaviour
{
    public Tilemap tilemap;
    bomb_controller bomb_ctrl;
    movements_controller movements_ctrl;
    public Tilemap bombTilemap;
    Vector3 curDir = Vector3.zero;
    Rigidbody2D rb;
    public float speed = 3f;
    Animator anim;
    
    void Start()
    {
        anim = GetComponent<Animator>();
        movements_ctrl = FindObjectOfType<movements_controller>();
        bomb_ctrl = FindObjectOfType<bomb_controller>();
        rb = GetComponent<Rigidbody2D>();
        StartCoroutine(RandomDirection());
    }

    
    void Update()
    {
        rb.MovePosition(transform.position + curDir * speed * Time.fixedDeltaTime);
    }

    IEnumerator RandomDirection()
    {
        while (true)
        {
            Vector3Int cell = tilemap.WorldToCell(transform.position);
            Vector3 pos = tilemap.GetCellCenterWorld(cell);
            List<Vector3> direction = new List<Vector3>();
            if (tilemap.GetTile<Tile>(cell + Vector3Int.up) == null && bombTilemap.GetTile<Tile>(cell + Vector3Int.up) == null)
                direction.Add(Vector3.up);
            if (tilemap.GetTile<Tile>(cell + Vector3Int.down) == null && bombTilemap.GetTile<Tile>(cell + Vector3Int.down) == null)
                direction.Add(Vector3.down);
            if (tilemap.GetTile<Tile>(cell + Vector3Int.right) == null && bombTilemap.GetTile<Tile>(cell + Vector3Int.right) == null)
                direction.Add(Vector3.right);
            if (tilemap.GetTile<Tile>(cell + Vector3Int.left) == null && bombTilemap.GetTile<Tile>(cell + Vector3Int.left) == null)
                direction.Add(Vector3.left);
            if (direction.Count == 0) curDir = Vector3.zero;
            else if (direction.Count == 1) curDir = direction[0];
            else if (direction.Count == 2)
            {
                if (direction[0] != -1 * direction[1])
                {
                    if (curDir.x == direction[0].x) curDir = direction[1];
                    else curDir = direction[0];
                }
            }
            else {
                Vector3 exceptDir = -1 * curDir;
                Vector3 random = exceptDir;
                while (random == exceptDir)
                    random = direction[Mathf.RoundToInt(UnityEngine.Random.Range(0, direction.Count))];
                curDir = random;
            }
            yield return new WaitForSeconds(1/speed * Time.timeScale);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Explosion"))
        {
            anim.SetBool("die", true);
            enabled = false;
            Destroy(gameObject, 1f);
            bomb_ctrl.IncreaseScrore(30);
            movements_ctrl.monsterCount--;
        }
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            GetComponent<Collider2D>().isTrigger = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            GetComponent<Collider2D>().isTrigger = false;
        }
    }
}

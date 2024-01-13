using System.Collections;
using UnityEngine;

public class movements_controller : MonoBehaviour
{
    UI ui;
    public GameOverScene gameOverScene;
    private Rigidbody2D rb;
    private Vector2 direction = Vector2.zero;
    public float speed = 4f;
    private const int idle = 0;
    private const int up = 1;
    private const int down = 2;
    private const int side = 3;
    private const int death = 4;
    private const int win = 5;
    [HideInInspector] public Animator anim;
    private SpriteRenderer sp;
    public float max_speed = 7f;
    private Vector2 startPos;
    private Vector3 playerScale;
    private float respawnTime = 0.5f;
    public int monsterCount = 1;
    audio_manager aud;
    [HideInInspector] public bool alive = true;
    PauseTime pt;


    void Start()
    {
        pt = FindObjectOfType<PauseTime>();
        sp = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        ui = FindObjectOfType<UI>();
        ui.setScoretext("SCORE: " + StaticData.s_score);
        ui.setBombtext((StaticData.s_bombamount - 1).ToString());
        ui.setLivetext((StaticData.s_live - 1).ToString());
        ui.setLuckytext(Mathf.Round(50 * StaticData.s_lucky - 5f).ToString());
        ui.setSpeedtext(Mathf.Round(2 * StaticData.s_speed - 8f).ToString());
        ui.setRaidustext((StaticData.s_blast - 1).ToString());
        startPos = transform.position;
        playerScale = transform.localScale;
        aud = FindObjectOfType<audio_manager>();
    }


    void Update()
    {
        float xDir = Input.GetAxisRaw("Horizontal");
        float yDir = Input.GetAxisRaw("Vertical");
        if (xDir != 0 || yDir != 0) aud.footStep.enabled = true;
        else aud.footStep.enabled = false;
        if (xDir < 0)
        {
            anim.SetInteger("state", side);
            sp.flipX = false;
            setDirection(Vector2.left);
        }
        else if (xDir > 0)
        {
            sp.flipX = true;
            anim.SetInteger("state", side);
            setDirection(Vector2.right);
        }
        else if (yDir < 0)
        {
            anim.SetInteger("state", down);
            setDirection(Vector2.down);
        }
        else if (yDir > 0)
        {
            anim.SetInteger("state", up);
            setDirection(Vector2.up);
        }
        if (Input.GetButtonDown("Cancel") && !pt.isPause)
        {
            pt.PauseTimer();
        }
    }

    private void FixedUpdate()
    {
        Vector2 curPos = rb.position;
        Vector2 step = direction * speed * Time.fixedDeltaTime;
        rb.MovePosition(curPos + step);
        setDirection(Vector2.zero);
        anim.SetInteger("state", idle);
    }

    private void setDirection(Vector2 newDir) 
    {
        direction = newDir;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Explosion") || collision.CompareTag("Enemy"))
        {
            StartCoroutine(Die());
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            StartCoroutine(Die());
        }
    }

    IEnumerator Die()
    {
        if (GetComponent<bomb_controller>().lives <= 0 || !alive) yield break;
        GetComponent<Collider2D>().isTrigger = true;
        aud.footStep.enabled = false;
        alive = false;
        anim.SetInteger("state", death);
        aud.playSFX(aud.playerDeath);
        GetComponent<bomb_controller>().lives--;
        if (GetComponent<bomb_controller>().lives > 0)
            ui.setLivetext((GetComponent<bomb_controller>().lives - 1).ToString());
        enabled = false;
        GetComponent<bomb_controller>().enabled = false;
        yield return new WaitForSeconds(2.25f);
        enabled = true;
        GetComponent<bomb_controller>().enabled = true;
        if (GetComponent<bomb_controller>().lives >= 1)
        {
            StartCoroutine(Respawn(respawnTime));
        }
        else
        {
            gameOverScene.Setup(GetComponent<bomb_controller>().score);
            FindObjectOfType<PauseTime>().enabled = false;
            GetComponent<bomb_controller>().enabled = false;
            enabled = false;
            yield break;
        }
    }

    IEnumerator Respawn(float duration)
    {
        aud.playSFX(aud.respawn);
        rb.simulated = false;
        transform.localScale = new Vector3(0, 0, 0);
        yield return new WaitForSeconds(duration);
        anim.SetInteger("state", idle);
        transform.position = startPos;
        rb.simulated = true;
        transform.localScale = playerScale;
        alive = true;
        GetComponent<Collider2D>().isTrigger = false;
    }

}

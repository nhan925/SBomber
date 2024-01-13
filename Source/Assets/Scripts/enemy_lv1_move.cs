using UnityEngine;

public class enemy_lv1_move : MonoBehaviour
{
    private Rigidbody2D Enemy;
    bomb_controller bomb_ctrl;
    movements_controller movements_ctrl;
    [SerializeField] float speed = 5f;
    private Animator anim;
    private SpriteRenderer sp;
    public bool isReturn;
    public int Rotate;
    void Start()
    {
        bomb_ctrl = FindObjectOfType<bomb_controller>();
        movements_ctrl = FindObjectOfType<movements_controller>();
        sp = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        Enemy = GetComponent<Rigidbody2D>();

        isReturn = false;
    }

    void Update()
    {
        //1: up 2:down 3:left 4:right
        if (Rotate == 1)
        {
            sp.flipX = false;
            Enemy.MovePosition(transform.position + Vector3.up * speed * Time.fixedDeltaTime);
            if (isReturn)
            {
                Rotate = 2;
                isReturn = false;
            }

        }
        else if (Rotate == 2)
        {
            sp.flipX = true;
            Enemy.MovePosition(transform.position + Vector3.down * speed * Time.fixedDeltaTime);
            if (isReturn)
            {
                Rotate = 1;
                isReturn = false;
            }
        }
        else if (Rotate == 3)
        {
            sp.flipX = true;
            Enemy.MovePosition(transform.position + Vector3.left * speed * Time.fixedDeltaTime);
            if (isReturn)
            {
                Rotate = 4;
                isReturn = false;
            }
        }
        else if (Rotate == 4)
        {
            sp.flipX = false;
            Enemy.MovePosition(transform.position + Vector3.right * speed * Time.fixedDeltaTime);
            if (isReturn)
            {
                Rotate = 3;
                isReturn = false;
            }
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Object") || collision.gameObject.CompareTag("Bomb"))
        {
            isReturn = true;
        }
        else if (collision.gameObject.CompareTag("Enemy"))
        {
            GetComponent<Collider2D>().isTrigger = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Explosion"))
        {
            anim.SetBool("die", true);
            enabled = false;
            GetComponent<Collider2D>().enabled = false;
            Destroy(gameObject, 1f);
            bomb_ctrl.IncreaseScrore(10);
            movements_ctrl.monsterCount--;
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

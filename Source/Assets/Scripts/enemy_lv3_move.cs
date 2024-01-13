using UnityEngine;
using UnityEngine.AI;

public class enemy_lv3_move : MonoBehaviour
{
    public GameObject player;
    bomb_controller bomb_ctrl;
    movements_controller movements_ctrl;
    NavMeshAgent agent;
    Animator anim;
    private SpriteRenderer sp;
    const int idle = 0;
    const int walk = 1;
    const int die = 2;
    const int attack = 3;
    
    void Start()
    {
        anim = GetComponent<Animator>();
        sp = GetComponent<SpriteRenderer>();
        bomb_ctrl = FindObjectOfType<bomb_controller>();
        movements_ctrl = FindObjectOfType<movements_controller>();
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
    }

    
    void Update()
    {
        Vector3 curPos = transform.position;
        if (Vector3.Distance(curPos, player.transform.position) < 10 && player.GetComponent<movements_controller>().alive)
        {
            anim.SetInteger("state", walk);
            Vector3 playerPos = player.transform.position;
            agent.SetDestination(playerPos);
            if (playerPos.x < transform.position.x) sp.flipX = true;
            else sp.flipX = false;
        }
        else if (player.GetComponent<movements_controller>().alive)
        {
            curPos.x = Mathf.Round(curPos.x);
            curPos.y = Mathf.Round(curPos.y);
            agent.SetDestination(curPos);
            anim.SetInteger("state", idle);
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Explosion"))
        {
            anim.SetInteger("state", die);
            enabled = false;
            Destroy(gameObject, 0.75f);
            bomb_ctrl.IncreaseScrore(50);
            movements_ctrl.monsterCount--;
        }
        else if (collision.CompareTag("Player"))
        {
            anim.SetInteger("state", attack);
        }
    }
}


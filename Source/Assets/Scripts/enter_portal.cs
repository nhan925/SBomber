using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class enter_portal : MonoBehaviour
{
    movements_controller movements_ctrl;
    bomb_controller bomb_ctrl;
    audio_manager aud;
    
    void Start()
    {
        aud = FindObjectOfType<audio_manager>();
        bomb_ctrl = FindObjectOfType<bomb_controller>();
        movements_ctrl = FindObjectOfType<movements_controller>();
    }

    void EnterPortal()
    {
        if (movements_ctrl.monsterCount <= 0)
        {
            saveData();
            StartCoroutine(WinEffect());
        }
    }

    IEnumerator WinEffect()
    {
        if (SceneManager.GetActiveScene().buildIndex == 3) aud.playSFX(aud.winGame);
        else aud.playSFX(aud.winLevel);
        movements_ctrl.anim.SetInteger("state", 5);
        movements_ctrl.enabled = false;
        bomb_ctrl.enabled = false;
        yield return new WaitForSeconds(1.75f);
        movements_ctrl.enabled = true;
        bomb_ctrl.enabled = true;
        SceneController.instance.Nextlevel();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            EnterPortal();
        }
    }

    private void saveData()
    {
        StaticData.s_score = bomb_ctrl.score;
        StaticData.s_bombamount = bomb_ctrl.bombAmount;
        StaticData.s_lucky = bomb_ctrl.ItemProb;
        StaticData.s_blast = bomb_ctrl.explodeSize;
        StaticData.s_speed = movements_ctrl.speed;
        StaticData.s_live = bomb_ctrl.lives;
        StaticData.s_time += Mathf.Round(Time.realtimeSinceStartup - FindObjectOfType<PauseTime>().startTime);
    }
}

using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseTime : MonoBehaviour
{
    public bool isPause;
    public GameObject pausePannel;
    public Text timeText;
    public int levelTime = 300;
    public float startTime;
    private float pausedTime;
    public GameObject player;
    public Animator TransitionAnim;
    [SerializeField] GameObject transitionObj;
    public GameOverScene gameOverScene;
    audio_manager aud;
    public bool escKey = false;
    
    void Start()
    {
        transitionObj.SetActive(true);
        startTime = Time.realtimeSinceStartup;
        aud = FindObjectOfType<audio_manager>();
    }

    
    void Update()
    {
        if (!isPause)
        {
            float elapsedRealTime = Time.realtimeSinceStartup - startTime;
            if (elapsedRealTime > levelTime)
            {
                aud.playSFX(aud.playerDeath);
                gameOverScene.Setup(player.GetComponent<bomb_controller>().score);
                player.GetComponent<bomb_controller>().enabled = false;
                player.GetComponent<movements_controller>().enabled = false;
                enabled = false;
            }
            timeText.text = "TIME: " + (levelTime - Mathf.Round(elapsedRealTime));
        }
    }

    
    public void PauseTimer()
    {
        isPause = true;
        aud.footStep.enabled = false;
        pausedTime = Time.realtimeSinceStartup - startTime;
        timeText.text = "TIME: " + (levelTime - Mathf.Round(pausedTime));
        pausePannel.SetActive(true);
        Time.timeScale = 0f;
        player.GetComponent<bomb_controller>().enabled = false;
        player.GetComponent<movements_controller>().enabled = false;
    }

    
    public void ResumeTimer()
    {
        if (!escKey) aud.playSFX(aud.buttonClick);
        else escKey = false;
        isPause = false;
        startTime = Time.realtimeSinceStartup - pausedTime;
        pausePannel.SetActive(false);
        Time.timeScale = 1f;
        player.GetComponent<bomb_controller>().enabled = true;
        player.GetComponent<movements_controller>().enabled = true;
    }

    public void ExitButton()
    {
        aud.playSFX(aud.buttonClick);
        Time.timeScale = 1f;
        StartCoroutine(EndTransition());
    }

    IEnumerator EndTransition()
    {
        TransitionAnim.SetTrigger("end");
        yield return new WaitForSeconds(1);
        aud.changeBackgroundMusic(aud.menu);
        SceneManager.LoadScene(0);
    }

}
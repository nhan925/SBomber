using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOverScene : MonoBehaviour
{
    public Text scoreText;
    [SerializeField] public Animator TransitionAnim;
    audio_manager aud;
    public void Setup(int score)
    {
        aud = FindObjectOfType<audio_manager>();
        gameObject.SetActive(true);
        scoreText.text = "Score: " + score.ToString();
    }

    public void RestartButton()
    {
        aud.playSFX(aud.buttonClick);
        SceneManager.LoadScene("Level_01");
    }

    public void ExitButton()
    {
        aud.playSFX(aud.buttonClick);
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
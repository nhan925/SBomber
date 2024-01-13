using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuScript : MonoBehaviour
{
    static bool firstTime = true;
    [SerializeField] GameObject transitionObj;
    audio_manager aud;
    private string playerName;

    void Start()
    {
        aud = FindObjectOfType<audio_manager>();
        if (!firstTime) transitionObj.SetActive(true);
        playerName = string.Empty;
    }


    public void PlayGame()
    {
        if (playerName == string.Empty) return;
        if (firstTime)
        {
            firstTime = false;
        }
        StartCoroutine(playGameTransition());
    }

    private IEnumerator playGameTransition()
    {
        transitionObj.SetActive(true);
        transitionObj.GetComponent<SceneController>().TransitionAnim.SetTrigger("end");
        yield return new WaitForSeconds(1);
        aud.changeBackgroundMusic(aud.background);
        SceneManager.LoadSceneAsync(1);
    }

    public void StopGame()
    {
        Application.Quit();
    }

    public void GetName(string s)
    {
        playerName = s;
    }

    public void assignPlayerName()
    {
        StaticData.s_name = playerName;
    }
}
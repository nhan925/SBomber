using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{

    public static SceneController instance;
    [SerializeField] public Animator TransitionAnim;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }


    public void Nextlevel()
    {
        StartCoroutine(LoadLevel());
    }
    IEnumerator LoadLevel()
    {
        TransitionAnim.SetTrigger("end");
        yield return new WaitForSeconds(1);
        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex + 1);
        TransitionAnim.SetTrigger("start");
    }

}

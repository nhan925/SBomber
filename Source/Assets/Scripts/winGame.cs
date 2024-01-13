using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.IO;

public class WinGame : MonoBehaviour
{
    public Text scoreText;
    public Text timeText;
    public Text liveText;
    public Text bombText;
    public Text luckyText;
    public Text speedText;
    public Text blastText;
    public float score = 100;
    public float time;
    public int bomb;
    public float lucky;
    public int blast;
    public float speed;
    public int star;
    public int live;
    public GameObject one_star;
    public GameObject two_star;
    public GameObject three_star;
    [SerializeField] GameObject transitionObj;
    public string playerName;
    private string filePath;
    audio_manager aud;

    void Start()
    {
        filePath = Application.persistentDataPath + "/players_data.dat";
        transitionObj.SetActive(true);
        Calculate();
        WinInfor();
        StarPannel();
        SavePlayerData(playerName, score, time);
        aud = FindObjectOfType<audio_manager>();
    }


    public void WinInfor()
    {
        scoreText.text = (score.ToString());
        timeText.text = (time.ToString());
        liveText.text = (live.ToString());
        bombText.text = (bomb.ToString());
        luckyText.text = (lucky.ToString());
        speedText.text = (speed.ToString());
        blastText.text = (blast.ToString());
    }
    //Bomb: x10 Blast: x10 Speed: x10 Lucky: x10 Heart: x15
    //Bonus 5 each item collected
    public void Calculate()
    {
        score = StaticData.s_score;
        playerName = StaticData.s_name;
        time = StaticData.s_time;
        bomb = StaticData.s_bombamount - 1;
        lucky = Mathf.Round(50f * StaticData.s_lucky - 5f);
        blast = StaticData.s_blast - 1;
        speed = Mathf.Round(2 * StaticData.s_speed - 8);
        live = StaticData.s_live - 1;
        score = score + bomb * 10 + lucky * 10 + blast * 10 + speed * 10 + live * 15 + 5 * StaticData.s_itemCount;
        if (score*10 - time > 8000) star = 3;
        else if (score*10 - time > 6000) star = 2;
        else star = 1;
    }
    public void StarPannel()
    {
        if (star == 1)
        {
            one_star.gameObject.SetActive(true);

        }
        else if (star == 2)
        {
            two_star.gameObject.SetActive(true);
        }
        else if (star == 3)
        {
            three_star.gameObject.SetActive(true);
        }
    }

    public void restartGame()
    {
        aud.playSFX(aud.buttonClick);
        StartCoroutine(transitionEffect(1));
    }


    public void mainMenu() 
    {
        aud.playSFX(aud.buttonClick);
        StartCoroutine(transitionEffect(0));
    }
   
    private IEnumerator transitionEffect(int scene)
    {
        transitionObj.GetComponent<SceneController>().TransitionAnim.SetTrigger("end");
        yield return new WaitForSeconds(1);
        SceneManager.LoadSceneAsync(scene);
    }

    public void SavePlayerData(string playerName, float score, float time)
    {
        if (!File.Exists(filePath))
        {
            File.Create(filePath).Close();
        }
        using (StreamWriter writer = new StreamWriter(filePath, true))
        {
            writer.WriteLine($"{playerName},{score},{time}");
        }
    }
}
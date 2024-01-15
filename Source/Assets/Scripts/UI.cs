using UnityEngine.UI;
using UnityEngine;


public class UI : MonoBehaviour
{
    public Text scoreText;
    public Text liveText;
    public Text bombText;
    public Text speedText;
    public Text luckyText;
    public Text radiusText;


    public void setScoretext(string text)
    {
        if (scoreText != null) scoreText.text = text;
    }

    public void setLivetext(string text)
    {
        liveText.text = text;
    }
    public void setBombtext(string text)
    {
        bombText.text = text;
    }

    public void setSpeedtext(string text)
    {
        speedText.text = text;
    }

    public void setLuckytext(string text)
    {
        luckyText.text = text;
    }

    public void setRaidustext(string text)
    {
        radiusText.text = text;
    }
}

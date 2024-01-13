using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;
using System.Linq;

public class SetRanking : MonoBehaviour
{
    public Text p1_name_text, p2_name_text, p3_name_text;
    public Text p1_score_text, p2_score_text, p3_score_text;
    public Text p1_time_text, p2_time_text, p3_time_text;
    private string p1_name = "Player 01", p2_name = "Player 02", p3_name = "Player 03";
    private int p1_score = 0, p2_score = 0, p3_score = 0;
    private float p1_time = 0, p2_time = 0, p3_time = 0;

    private string filePath;
    void Start()
    {
        filePath = Application.persistentDataPath + "/players_data.dat";
        List<string> topThreePlayers = GetTopThreePlayers();
        ParsePlayerData(topThreePlayers);
        SetText();
    }

    public void SetText()
    {
        p1_name_text.text = p1_name;
        p2_name_text.text = p2_name;
        p3_name_text.text = p3_name;
        p1_score_text.text = p1_score.ToString();
        p2_score_text.text = p2_score.ToString();
        p3_score_text.text = p3_score.ToString();
        p1_time_text.text = p1_time.ToString();
        p2_time_text.text = p2_time.ToString();
        p3_time_text.text = p3_time.ToString();
    }

    public List<string> GetTopThreePlayers()
    {
        if (File.Exists(filePath))
        {
            string[] lines = File.ReadAllLines(filePath);
            if (lines.Length > 0)
            {
                List<string> playerDataList = new List<string>(lines);
                playerDataList.Sort((a, b) =>
                {
                    int scoreA = int.Parse(a.Split(',')[1]);
                    int scoreB = int.Parse(b.Split(',')[1]);
                    if (scoreA != scoreB) return scoreB.CompareTo(scoreA);
                    else
                    {
                        float timeA = float.Parse(a.Split(",")[2]);
                        float timeB = float.Parse(b.Split(",")[2]);
                        return timeA.CompareTo(timeB);
                    }
                });
                List<string> topThreePlayers = playerDataList.Take(3).ToList();
                return topThreePlayers;
            }
            else
            {
                return new List<string> {};
            }
        }
        else
        {
            return new List<string> {};
        }
    }

    private void ParsePlayerData(List<string> topThreePlayers)
    {
        if (topThreePlayers.Count >= 1)
        {
            string[] data1 = topThreePlayers[0].Split(',');
            p1_name = data1[0];
            p1_score = int.Parse(data1[1]);
            p1_time = float.Parse(data1[2]);
        }

        if (topThreePlayers.Count >= 2)
        {
            string[] data2 = topThreePlayers[1].Split(',');
            p2_name = data2[0];
            p2_score = int.Parse(data2[1]);
            p2_time = float.Parse(data2[2]);
        }

        if (topThreePlayers.Count >= 3)
        {
            string[] data3 = topThreePlayers[2].Split(',');
            p3_name = data3[0];
            p3_score = int.Parse(data3[1]);
            p3_time = float.Parse(data3[2]);
        }
    }
}
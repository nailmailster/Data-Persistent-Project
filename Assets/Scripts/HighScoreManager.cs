using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.IO;

public class HighScoreManager : MonoBehaviour
{
    public static HighScoreManager Instance;

    private string championName = "";
    private int championScore = 0;

    private string playerName = "";

    public string ChampionName
    {
        get { return championName; }
        set { championName = value; }
    }

    public int ChampionScore
    {
        get { return championScore; }
        set { championScore = value; }
    }

    public string PlayerName
    {
        get { return playerName; }
        set { playerName = value; }
    }

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        LoadHighScoreAndPlayer();
    }

    [System.Serializable]
    class SaveData
    {
        // private string championName;
        // private int highscore;
        // private string playerName;
        //  Unity cannot serialize properties?
        //  So I made values public
        public string championName;
        public int highscore;
        public string playerName;

        public string ChampionName
        {
            get { return championName; }
            set { championName = value; }
        }

        public int Highscore
        {
            get { return highscore; }
            set { highscore = value; }
        }

        public string PlayerName
        {
            get { return playerName; }
            set { playerName = value; }
        }
    }

    public void SaveHighScoreAndPlayer(int points)
    {
        SaveData data = new SaveData();

        data.PlayerName = PlayerName;
        if (points > ChampionScore)
        {
            data.ChampionName = PlayerName;
            data.Highscore = points;
        }
        else
        {
            data.ChampionName = championName;
            data.Highscore = championScore;
        }

        string json = JsonUtility.ToJson(data);

        File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
    }

    public void LoadHighScoreAndPlayer()
    {
        string path = Application.persistentDataPath + "/savefile.json";

        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveData data = JsonUtility.FromJson<SaveData>(json);
            championName = data.ChampionName;
            championScore = data.Highscore;
            playerName = data.PlayerName;
        }
        else
        {
            championName = "";
            championScore = 0;
            playerName = "";
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.IO;
using System;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public string playerName;
    public int highScore;
    public Player[] bestTenPlayers = new Player[10];


    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    [System.Serializable]
    class SavedData
    {
        public int points;
        public string playerName;
        public Player[] bestTenPlayers;
    }

    public void SaveData()
    {

        SavedData data = new SavedData
        {
            points = highScore,
            playerName = playerName,
            bestTenPlayers = bestTenPlayers
        };

        string json = JsonUtility.ToJson(data);

        File.WriteAllText(Application.persistentDataPath + "/SaveData.json", json);
    }

    public void LoadData()
    {
        string path = Application.persistentDataPath + "/SaveData.json";

        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SavedData data = JsonUtility.FromJson<SavedData>(json);
            Instance.playerName = data.playerName;
            Instance.highScore = data.points;
            Instance.bestTenPlayers = data.bestTenPlayers;
        } else
        {
            for (int i = 0; i < 10; i++)
            {
                Instance.bestTenPlayers[i] = new Player("-", 0);
            }
        }

    }

    [System.Serializable]
    public class Player : IComparable<Player>
    {
        public int score;
        public string playerName;

        public Player(string playerName, int score)
        {
            this.playerName = playerName;
            this.score = score;
        }

        public int CompareTo(Player obj)
        {
            return obj.score.CompareTo(this.score);
        }
    }
}
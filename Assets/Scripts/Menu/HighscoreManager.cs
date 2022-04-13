using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HighscoreManager : MonoBehaviour
{
    public TextMeshProUGUI[] players = new TextMeshProUGUI[9];
    public TextMeshProUGUI[] scores = new TextMeshProUGUI[9];

    void Awake()
    {
        for (int i = 0; i < players.Length; i++)
        {
            players[i].text = GameManager.Instance.bestTenPlayers[i].playerName;
            scores[i].text = "" + GameManager.Instance.bestTenPlayers[i].score;
        }
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System;

public class MainManager : MonoBehaviour
{
    public Brick BrickPrefab;
    public int LineCount = 6;
    public Rigidbody Ball;

    public Text ScoreText;
    public Text HighscoreText;
    public GameObject GameOverText;
    public Button BackToMenu;

    private bool m_Started = false;
    private int m_Points;
    
    private bool m_GameOver = false;


    
    // Start is called before the first frame update
    void Start()
    {
        HighscoreText.text = "Best Score: " + GameManager.Instance.bestTenPlayers[0].playerName + " - " + GameManager.Instance.bestTenPlayers[0].score;

        const float step = 0.6f;
        int perLine = Mathf.FloorToInt(4.0f / step);
        
        int[] pointCountArray = new [] {1,1,2,2,5,5};
        for (int i = 0; i < LineCount; ++i)
        {
            for (int x = 0; x < perLine; ++x)
            {
                Vector3 position = new Vector3(-1.5f + step * x, 2.5f + i * 0.3f, 0);
                var brick = Instantiate(BrickPrefab, position, Quaternion.identity);
                brick.PointValue = pointCountArray[i];
                brick.onDestroyed.AddListener(AddPoint);
            }
        }
    }

    private void Update()
    {
        if (!m_Started)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                m_Started = true;
                float randomDirection = UnityEngine.Random.Range(-1.0f, 1.0f);
                Vector3 forceDir = new Vector3(randomDirection, 1, 0);
                forceDir.Normalize();

                Ball.transform.SetParent(null);
                Ball.AddForce(forceDir * 2.0f, ForceMode.VelocityChange);
            }
        }
        else if (m_GameOver)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
        }
    }

    

    void AddPoint(int point)
    {
        m_Points += point;
        ScoreText.text = $"Score : {m_Points}";
    }

    public void GameOver()
    {
        m_GameOver = true;
        Debug.Log("Dosaženo: " + m_Points + ", poslední v tabulce byl " + GameManager.Instance.bestTenPlayers[0].playerName + " se " + GameManager.Instance.bestTenPlayers[0].score + "body");

        if (m_Points > GameManager.Instance.bestTenPlayers[9].score)
        {
            Debug.Log("Podmínka splnìna");
            GameManager.Instance.bestTenPlayers[9] = new GameManager.Player(GameManager.Instance.playerName, m_Points);
            Array.Sort(GameManager.Instance.bestTenPlayers);
            for (int i = 0; i < 10; i++)
            {
                Debug.Log(GameManager.Instance.bestTenPlayers[i].playerName + " - " + GameManager.Instance.bestTenPlayers[i].score);
            }
            HighscoreText.text = "Best Score: " + GameManager.Instance.bestTenPlayers[0].playerName + " - " + GameManager.Instance.bestTenPlayers[0].score;
            GameManager.Instance.SaveData();
        }
        GameOverText.SetActive(true);
        BackToMenu.gameObject.SetActive(true);
    }

    public void GoBackToMenu()
    {
        SceneManager.LoadScene(0);
    }

    


}

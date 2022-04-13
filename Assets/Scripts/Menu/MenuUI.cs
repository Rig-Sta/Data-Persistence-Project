using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;


#if UNITY_EDITOR
using UnityEditor;
#endif

public class MenuUI : MonoBehaviour
{
    public TMP_InputField inputField;

    private void Awake()
    {
        GameManager.Instance.LoadData();

        if (inputField != null)
        {
            inputField.text = GameManager.Instance.playerName;
        }
        
    }

    public void StartGame()
    {
        GameManager.Instance.playerName = inputField.text;
        GameManager.Instance.SaveData();
        SceneManager.LoadScene(1);
    }

    public void EnterHighscore()
    {
        GameManager.Instance.playerName = inputField.text;
        GameManager.Instance.SaveData();
        SceneManager.LoadScene(2);
    }

    public void ExitHighscore()
    {
        SceneManager.LoadScene(0);
    }

    public void ExitGame()
    {
        GameManager.Instance.playerName = inputField.text;
        GameManager.Instance.SaveData();

#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
        Application.Quit();
#endif
    }
}
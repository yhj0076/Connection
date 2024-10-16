using System.Collections;
using System.Collections.Generic;
using TMPro;
using TMPro.Examples;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    private void Awake()
    {
        Application.targetFrameRate = 60;
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Debug.LogError("씬에 2개 이상의 게임 매니저가 존재합니다.");
            Destroy(gameObject);
            if(SecurityPlayerPrefs.GetInt("Clear",-1) == -1)
            {
                SecurityPlayerPrefs.SetInt("Clear", 0);
            }
        }
    }

    public void GameStart()
    {
        SceneManager.LoadScene(3);
    }

    public void Clear()
    {
        SceneManager.LoadScene(4);
        int before = SecurityPlayerPrefs.GetInt("difficulty", 0);
        if (before < 5)
        {
            before++;
            SecurityPlayerPrefs.SetInt("difficulty", before);
        }
        else
        {
            SecurityPlayerPrefs.SetInt("Clear", 1);
        }
    }

    public void Story()
    {
        SceneManager.LoadScene(6);
    }

    public void Option()
    {
        SceneManager.LoadScene(1);
    }

    public void GoHome()
    {
        SceneManager.LoadScene(0);
    }

    public void NowStage()
    {
        SceneManager.LoadScene(2);
    }

    public void Die()
    {
        SceneManager.LoadScene(6);
    }

    public void ExitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
      Application.Quit(); // 어플리케이션 종료
#endif
    }
}

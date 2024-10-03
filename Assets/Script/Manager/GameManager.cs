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
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Debug.LogError("씬에 2개 이상의 게임 매니저가 존재합니다.");
            Destroy(gameObject);
        }
    }

    public void GameStart()
    {
        SceneManager.LoadScene(0);
    }

    public void Clear()
    {
        SceneManager.LoadScene(8);
    }

    public void Story()
    {
        SceneManager.LoadScene(6);
    }
}

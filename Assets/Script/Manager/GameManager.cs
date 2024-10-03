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
            Debug.LogError("���� 2�� �̻��� ���� �Ŵ����� �����մϴ�.");
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

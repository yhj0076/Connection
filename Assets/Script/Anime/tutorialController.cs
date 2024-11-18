using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tutorialController : MonoBehaviour
{
    private void Awake()
    {
        if (SecurityPlayerPrefs.GetInt("difficulty", 0) > 0)
        {
            gameObject.SetActive(false);
        }
    }
    private void Start()
    {
        Time.timeScale = 0;
    }

    private void OnDisable()
    {
        Time.timeScale = 1;
    }
}

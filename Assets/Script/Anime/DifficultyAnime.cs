using System.Collections;
using System.Collections.Generic;
using System.IO.IsolatedStorage;
using UnityEngine;

public class DifficultyAnime : MonoBehaviour
{
    public GameObject[] Enemies;
    private void Awake()
    {
        int difficulty = SecurityPlayerPrefs.GetInt("difficulty", 0);
        GameObject Enemy = GameObject.Instantiate(Enemies[difficulty]);
        Enemy.transform.parent = transform;
        Enemy.transform.localPosition = Vector3.zero;
    }
}

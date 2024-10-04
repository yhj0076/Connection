using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthManager : MonoBehaviour
{
    public GameObject PlayerHP;
    public GameObject EnemyHP;

    public float PlayerHealth;
    public float EnemyHealth;

    private float PlayerFullHP;
    private float EnemyFullHP;

    private Vector2 PlayerFullGage;
    private Vector2 EnemyFullGage;
    private void Start()
    {
        PlayerFullHP = PlayerHealth;
        EnemyFullHP = EnemyHealth;
        PlayerFullGage = new Vector2(PlayerHP.GetComponent<RectTransform>().sizeDelta.x, PlayerHP.GetComponent<RectTransform>().sizeDelta.y);
        EnemyFullGage = new Vector2(EnemyHP.GetComponent<RectTransform>().sizeDelta.x, EnemyHP.GetComponent<RectTransform>().sizeDelta.y);
    }

    public void Attack(float dmg)
    {
        if(dmg > 0)
        {
            EnemyHealth -= dmg;
            Vector2 DMG = new Vector2(EnemyHealth/EnemyFullHP, 1);
            EnemyHP.GetComponent<RectTransform>().sizeDelta = EnemyFullGage * DMG;
        }
        else if(dmg < 0)
        {
            PlayerHealth -= dmg;
            Vector2 DMG = new Vector2(PlayerHealth / PlayerFullHP, 1);
            PlayerHP.GetComponent<RectTransform>().sizeDelta = PlayerFullGage * DMG;
        }
    }
}

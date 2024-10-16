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
    GameObject StageManager;
    GameObject Stage;
    private void Start()
    {
        PlayerFullHP = PlayerHealth;
        EnemyFullHP = EnemyHealth;
        PlayerFullGage = new Vector2(PlayerHP.GetComponent<RectTransform>().sizeDelta.x, PlayerHP.GetComponent<RectTransform>().sizeDelta.y);
        EnemyFullGage = new Vector2(EnemyHP.GetComponent<RectTransform>().sizeDelta.x, EnemyHP.GetComponent<RectTransform>().sizeDelta.y);

        StageManager = GameObject.Find("StageManager");
        Stage = GameObject.Find("Stage");
    }

    public void Attack(float dmg)
    {
        if(dmg > 0)
        {
            EnemyHealth -= dmg;
            if (EnemyHealth > 0)
            {
                Vector2 DMG = new Vector2(EnemyHealth / EnemyFullHP, 1);
                EnemyHP.GetComponent<RectTransform>().sizeDelta = EnemyFullGage * DMG;
            }
            else
            {
                RefreshClearStat();
                Vector2 DMG = new Vector2(0, 1);
                EnemyHP.GetComponent<RectTransform>().sizeDelta = EnemyFullGage * DMG;
                GameObject gameManager = FindObjectOfType<GameManager>().gameObject;
                gameManager.GetComponent<GameManager>().Clear();
            }
        }
        else if(dmg < 0)
        {
            PlayerHealth += dmg;
            if (PlayerHealth > 0)
            {
                Vector2 DMG = new Vector2(PlayerHealth / PlayerFullHP, 1);
                PlayerHP.GetComponent<RectTransform>().sizeDelta = PlayerFullGage * DMG;
            }
            else
            {
                RefreshClearStat();
                Vector2 DMG = new Vector2(0, 1);
                PlayerHP.GetComponent<RectTransform>().sizeDelta = PlayerFullGage * DMG;
                GameObject gameManager = FindObjectOfType<GameManager>().gameObject;
                gameManager.GetComponent<GameManager>().Die();
            }
        }
    }

    void RefreshClearStat()
    {
        SecurityPlayerPrefs.SetFloat("ABT", StageManager.GetComponent<StageManager>().ABT);
        SecurityPlayerPrefs.SetInt("ABP", StageManager.GetComponent<StageManager>().GetABP());
        SecurityPlayerPrefs.SetInt("LL", StageManager.GetComponent<StageManager>().GetLL());
        SecurityPlayerPrefs.SetInt("UCP", StageManager.GetComponent<StageManager>().GetUCP());
        SecurityPlayerPrefs.SetFloat("DPR", StageManager.GetComponent<StageManager>().GetDPR());
    }
}

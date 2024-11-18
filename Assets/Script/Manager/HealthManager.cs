using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthManager : MonoBehaviour
{
    public GameObject PlayerHP;
    public GameObject EnemyHP;
    public GameObject Fade;

    public float PlayerHealth;
    public float EnemyHealth;

    private float PlayerFullHP;
    private float EnemyFullHP;

    private Vector2 PlayerFullGage;
    private Vector2 EnemyFullGage;
    GameObject StageManager;
    GameObject Stage;

    GameObject Player;
    GameObject Enemy;

    private void Start()
    {
        Fade.SetActive(false);
        PlayerFullHP = PlayerHealth;
        EnemyFullHP = EnemyHealth;
        PlayerFullGage = new Vector2(PlayerHP.GetComponent<RectTransform>().sizeDelta.x, PlayerHP.GetComponent<RectTransform>().sizeDelta.y);
        EnemyFullGage = new Vector2(EnemyHP.GetComponent<RectTransform>().sizeDelta.x, EnemyHP.GetComponent<RectTransform>().sizeDelta.y);

        StageManager = GameObject.Find("StageManager");
        Stage = GameObject.Find("Stage");
        Player = GameObject.Find("Player");
        Enemy = GameObject.Find("Enemies").transform.GetChild(0).gameObject;
    }

    public void Attack(float dmg)
    {
        if(dmg > 0)
        {
            EnemyHealth -= dmg;
            Player.GetComponent<PlayerAnimation>().toAttack();  // 공격 애니메이션
            if (EnemyHealth > 0)
            {
                Enemy.GetComponent<EnemyAnimationSelector>().toDamage();
                Vector2 DMG = new Vector2(EnemyHealth / EnemyFullHP, 1);
                EnemyHP.GetComponent<RectTransform>().sizeDelta = EnemyFullGage * DMG;
            }
            else
            {
                Fade.SetActive(true);
                Enemy.GetComponent<EnemyAnimationSelector>().toDeath();
                RefreshClearStat();
                Vector2 DMG = new Vector2(0, 1);
                EnemyHP.GetComponent<RectTransform>().sizeDelta = EnemyFullGage * DMG;
            }
        }
        else if(dmg < 0)
        {
            PlayerHealth += dmg;
            Enemy.GetComponent<EnemyAnimationSelector>().toAttack();
            if (PlayerHealth > 0)
            {
                Player.GetComponent<PlayerAnimation>().toDamage();  // 피격 애니메이션
                Vector2 DMG = new Vector2(PlayerHealth / PlayerFullHP, 1);
                PlayerHP.GetComponent<RectTransform>().sizeDelta = PlayerFullGage * DMG;
            }
            else
            {
                Fade.SetActive(true);
                Player.GetComponent<PlayerAnimation>().toDeath();  // 사망 애니메이션
                RefreshClearStat();
                Vector2 DMG = new Vector2(0, 1);
                PlayerHP.GetComponent<RectTransform>().sizeDelta = PlayerFullGage * DMG;
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

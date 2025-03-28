using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MultiStageManager : MonoBehaviourPun
{
    public GameObject HealthManager;
    public GameObject timeText;
    public GameObject Stage;
    public GameObject PlayerRatio;
    public GameObject EnemyRatio;
    public GameObject PlayerDMG;
    public GameObject EnemyDMG;
    public GameObject GoBackWindow; // 뒤로가기 창
    public GameObject EnemyController;

    public float PlayerPower;
    public float EnemyPower;

    public float LeftRatio;
    public float RightRatio;

    public float LeftTime;

    protected State state;

    protected float tmpTime;
    protected Vector2 firstSizePlayer;
    protected Vector2 firstSizeEnemy;
    // 비율 : Player / Player + Enemy
    // Start is called before the first frame update

    public float ABT;
    public int ABP;
    public int LL;
    public int UCP;
    public float DPR;

    protected int CountRound;
    protected int AllDMG;
    public void UpdateData()
    {
        Stage stageComponent = Stage.GetComponent<Stage>();
        Stage practiceComponent = Stage.GetComponent<PracticeStage>();
        if (stageComponent != null)
        {
            PlayerPower = Stage.GetComponent<Stage>().gainedDamage;
        }
        else if (practiceComponent != null)
        {
            PlayerPower = Stage.GetComponent<PracticeStage>().gainedDamage;
        }

        if (PlayerPower == 0)
        {
            LeftRatio = 0.5f;
        }
        if (EnemyPower == 0)
        {
            RightRatio = 0.5f;
        }
        if (PlayerPower > 0 || EnemyPower > 0)
        {
            if (PlayerPower > 0)
            {
                LeftRatio = PlayerPower / (PlayerPower + EnemyPower);
                if (LeftRatio > 0.9f)
                {
                    LeftRatio = 0.9f;
                }
            }
            else
            {
                LeftRatio = 0.1f;
            }

            if (EnemyPower > 0)
            {
                RightRatio = EnemyPower / (PlayerPower + EnemyPower);
                if (RightRatio > 0.9f)
                {
                    RightRatio = 0.9f;
                }
            }
            else
            {
                RightRatio = 0.1f;
            }
        }
        PlayerRatio.GetComponent<RectTransform>().sizeDelta = firstSizePlayer * new Vector2(LeftRatio, 1f);
        PlayerDMG.GetComponent<TextMeshProUGUI>().text = PlayerPower.ToString();
        EnemyRatio.GetComponent<RectTransform>().sizeDelta = firstSizeEnemy * new Vector2(RightRatio, 1f);
        EnemyDMG.GetComponent<TextMeshProUGUI>().text = EnemyPower.ToString();
    }

    public int GetABP()
    {
        if (Stage.GetComponent<Stage>() != null)
        {
            return Stage.GetComponent<Stage>().ABP;
        }
        else
        {
            return Stage.GetComponent<PracticeStage>().ABP;
        }
    }

    public int GetLL()
    {
        if (Stage.GetComponent<Stage>() != null)
        {
            return Stage.GetComponent<Stage>().LL;
        }
        else
        {
            return Stage.GetComponent<PracticeStage>().LL;
        }
    }

    public int GetUCP()
    {
        if (Stage.GetComponent<Stage>() != null)
        {
            return Stage.GetComponent<Stage>().UCP;
        }
        else
        {
            return Stage.GetComponent<PracticeStage>().UCP;
        }
    }

    protected void GetAllDmg()
    {
        int gained;
        if (Stage.GetComponent<Stage>() != null)
        {
            gained = Stage.GetComponent<Stage>().gainedDamage;
        }
        else
        {
            gained = Stage.GetComponent<PracticeStage>().gainedDamage;
        }

        AllDMG += gained;
    }

    public float GetDPR()
    {
        return AllDMG / CountRound;
    }

    protected void Timer()
    {
        if (LeftTime > 0)
        {
            LeftTime -= Time.deltaTime;
            int intTime = (int)LeftTime;
            timeText.GetComponent<TextMeshProUGUI>().text = intTime.ToString();
        }
        else
        {
            GetAllDmg();
            EnemyPower = EnemyController.GetComponent<EnemyManager>().EnemyPower;
            // Attack
            float dmg = PlayerPower - EnemyPower;
            HealthManager.GetComponent<HostHealthManager>().MultiAttack(dmg);
            LeftTime = tmpTime;
            Stage.GetComponent<Stage>().gainedDamage = 0;
            PlayerPower = 0;
            EnemyPower = 0;
            EnemyController.GetComponent<EnemyManager>().clearPower();
            CountRound++;
            UpdateData();
        }

        ABT += Time.deltaTime;
    }

    public void pause()
    {
        if (!GoBackWindow.activeSelf)
        {
            GoBackWindow.SetActive(true);
        }
    }

    protected enum State
    {
        InGame,
        Stop
    }
}

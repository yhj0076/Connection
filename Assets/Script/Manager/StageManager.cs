using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class StageManager : MonoBehaviour
{
    public GameObject HealthManager;
    public GameObject timeText;
    public GameObject Stage;
    public GameObject PlayerRatio;
    public GameObject EnemyRatio;
    public GameObject PlayerDMG;
    public GameObject EnemyDMG;
    public GameObject GoBackWindow; // �ڷΰ��� â
    public GameObject EnemyController;

    public float PlayerPower;
    public float EnemyPower;

    public float LeftRatio = 0.5f;
    public float RightRatio = 0.5f;

    public float LeftTime;

    State state;

    float tmpTime;
    Vector2 firstSizePlayer;
    Vector2 firstSizeEnemy;
    // ���� : Player / Player + Enemy
    // Start is called before the first frame update

    public float ABT;
    public int ABP;
    public int LL;
    public int UCP;
    public float DPR;

    int CountRound;
    int AllDMG;
    void Awake()
    {
        state = State.InGame;
        tmpTime = LeftTime;
        firstSizePlayer = PlayerRatio.GetComponent<RectTransform>().sizeDelta;
        firstSizeEnemy = EnemyRatio.GetComponent<RectTransform>().sizeDelta;
        Time.timeScale = 1;

        ABT = 0;
        ABP = 0;
        LL = 0;
        UCP = 0;
        DPR = 0;
        CountRound = 0;
    }

    private void Update()
    {
        if (state == State.InGame)
        {
            Timer();
        }
        UpdateData();
    }

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
            PlayerPower = Stage.GetComponent <PracticeStage>().gainedDamage;
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
                if(LeftRatio > 0.9f)
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
        return Stage.GetComponent<Stage>().ABP;
    }

    public int GetLL()
    {
        return Stage.GetComponent<Stage>().LL;
    }

    public int GetUCP()
    {
        return Stage.GetComponent<Stage>().UCP;
    }

    private void GetAllDmg()
    {
        AllDMG += Stage.GetComponent<Stage>().gainedDamage;
    }

    public float GetDPR()
    {
        return AllDMG / CountRound;
    }

    private void Timer()
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
            HealthManager.GetComponent<HealthManager>().Attack(dmg);
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

    enum State
    {
        InGame,
        Stop
    }
}

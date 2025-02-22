using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TutorialStageManager : StageManager
{
    public GameObject ResultWindow;
    public float ReadyTime; // 준비 시간

    private void Awake()
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

    // Update is called once per frame
    void Update()
    {
        if (state == State.InGame)
        {
            if (ReadyTime <= 0)
            {
                PracticeTimer();
            }
            else
            {
                TimerStart();
            }
        }
    }

    void TimerStart()
    {
        if (ReadyTime > 0) 
        {
            ReadyTime -= Time.deltaTime;
        }
    }

    void PracticeTimer()
    {
        if(LeftTime > 0)
        {
            LeftTime -= Time.deltaTime;
            int intTime = (int)LeftTime;
            timeText.GetComponent<TextMeshProUGUI>().text = intTime.ToString();
        }
        else
        {
            ABP = GetABP();
            LL = GetLL();
            UCP = GetUCP();
            DPR = Stage.GetComponent<Stage>().gainedDamage;
            ResultWindow.gameObject.SetActive(true);
        }
    }
}

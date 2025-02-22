using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainStageManager : StageManager
{
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
            Timer();
        }
        UpdateData();
    }
}

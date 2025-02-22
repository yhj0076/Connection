using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResultWindow : ClearStatManager
{
    public GameObject tutorialStageManager;
    public int ABP;
    public int LL;
    public int UCP;
    public float DPR;

    private void OnEnable()
    {
        GetResultInPractice();
    }

    private void OnDisable()
    {
        RefreshData();
    }

    void GetResultInPractice()
    {
        TutorialStageManager tutorial = tutorialStageManager.GetComponent<TutorialStageManager>();
        ABP = tutorial.GetABP();
        LL = tutorial.GetLL();
        UCP = tutorial.GetUCP();
        DPR = tutorial.GetDPR();

        PrintABP();
        PrintLL();
        PrintUCP();
        PrintDPR();
    }

    void RefreshData()
    {
        ABP = 0;
        LL = 0;
        UCP = 0;
    }
}

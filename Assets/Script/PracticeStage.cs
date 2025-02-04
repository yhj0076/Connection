using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PracticeStage : Stage
{
    private void Awake()
    {
        Sensitivity = SecurityPlayerPrefs.GetFloat("sensitive", 5);
    }

    // Start is called before the first frame update
    void Start()
    {
        StageManager.GetComponent<TutorialStageManager>().UpdateData();
        BulletMaker.GetComponent<BulletMaker>().MakeBullet(MaxBullet);
    }

    // Update is called once per frame
    void Update()
    {
        //TouchUpdate();
        MouseUpdate();
        if (Input.GetMouseButtonUp(0))
        {
            FastAddDmg();
        }
    }

    void FastAddDmg()
    {
        Console.WriteLine();
        StageManager.GetComponent<TutorialStageManager>().PlayerPower += gainedDamage;
        gainedDamage = 0;
    }
}

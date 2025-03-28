using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PracticeStage : Stage
{
    public GameObject FastAdd;

    private void Awake()
    {
        Sensitivity = SecurityPlayerPrefs.GetFloat("sensitive", 5);
    }

    // Start is called before the first frame update
    void Start()
    {
        //MultiStageManager.GetComponent<TutorialStageManager>().UpdateData();
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
        Debug.Log("damage Add");
        FastAdd.GetComponent<TextMeshProUGUI>().text = gainedDamage.ToString();
        //MultiStageManager.GetComponent<TutorialStageManager>().UpdateData();
    }
}

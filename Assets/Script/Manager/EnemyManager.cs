using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public int difficulty;

    public float thinkTime;
    public int maxLink;
    public int minLink;
    public bool canUseChance;

    public float EnemyPower;

    public float movingTime;

    public GameObject healthManager;
    public GameObject StageManager;
    // Start is called before the first frame update
    void Start()
    {
        if (SecurityPlayerPrefs.GetInt("difficulty", 0) == 0)
        {
            difficulty = 0;
        }
        else
        {
            difficulty = SecurityPlayerPrefs.GetInt("difficulty", 0);
        }

        difficulty++;

        switch (difficulty)
        {
            case 1:
                thinkTime = 5;
                maxLink = 4;
                minLink = 2;
                canUseChance = false;
                break;

            case 2:
                thinkTime = 4;
                maxLink = 5;
                minLink = 3;
                canUseChance = false; 
                break;

            case 3:
                thinkTime = 3.5f;
                maxLink = 7;
                minLink = 2;
                canUseChance = true;
                break;

            case 4:
                thinkTime = 2.5f;
                maxLink = 10;
                minLink = 2;
                canUseChance = false;
                break;

            case 5:
                thinkTime = 2;
                maxLink = 10;
                minLink = 4;
                canUseChance = true;
                break;

            default:
                thinkTime = 100;
                maxLink = 0;
                minLink = 0;
                canUseChance = false;
                break;
        }
        movingTime = thinkTime;
    }

    // Update is called once per frame
    void Update()
    {
        think();
    }

    void think()
    {
        if (movingTime > 0)
        {
            movingTime -= Time.deltaTime;
        }
        else
        {
            int dmg = Random.Range(minLink, maxLink+1);

            int isUseChance = Random.Range(0, 11);
            if(isUseChance == 10 && canUseChance)
            {
                useChance();
            }
            EnemyPower += dmg;
            movingTime = thinkTime;
            StageManager.GetComponent<StageManager>().EnemyPower = EnemyPower;
        }
    }

    void useChance()
    {
        int random = Random.Range(0, 3);
        if (random == 0)
        {
            EnemyPower += (int)Random.Range(5,8);
        }
        else if (random == 1)
        {
            EnemyPower += (int)Random.Range(8, 13);
        }
        else if(random == 2)
        {
            EnemyPower += 50;
        }
    }

    public void clearPower()
    {
        EnemyPower = 0;
    }
}

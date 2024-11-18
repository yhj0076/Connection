using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPointer : MonoBehaviour
{
    public float jumpingTiming;
    public float movingSpeed;
    float beforeY;
    float afterY;

    float x;
    float z;

    float movetoX;

    float ttime = 0;

    public GameObject[] stages;

    private void Awake()
    {
        GetComponent<RectTransform>().position = new Vector3(
            stages[SecurityPlayerPrefs.GetInt("difficulty", 0)].GetComponent<RectTransform>().position.x, 
            GetComponent<RectTransform>().position.y,
            GetComponent<RectTransform>().position.z);
    }

    // Start is called before the first frame update
    void Start()
    {
        beforeY = GetComponent<RectTransform>().position.y;
        afterY = GetComponent<RectTransform>().position.y + 10;

        x = GetComponent<RectTransform>().position.x;
        z = GetComponent<RectTransform>().position.z;

        int difficulty = SecurityPlayerPrefs.GetInt("difficulty", 0);

        if (difficulty > 0)
        {
            x = stages[difficulty-1].GetComponent<RectTransform>().position.x;
            movetoX = stages[difficulty].GetComponent<RectTransform>().position.x;
        }
        else
        {
            x = stages[difficulty].GetComponent<RectTransform>().position.x;
        }
    }

    // Update is called once per frame
    void Update()
    {
        Timer();
    }

    void Timer()
    {
        if (x<movetoX)
        {
            x += Time.deltaTime * movingSpeed;
        }
        if(ttime < jumpingTiming)
        {
            ttime += Time.deltaTime;
        }
        else
        {
            GetComponent<RectTransform>().position = new Vector3(x, afterY, z);

            float temp = afterY;
            afterY = beforeY;
            beforeY = temp;

            ttime = 0;
        }
    }
}

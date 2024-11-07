using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TypingController : MonoBehaviour
{
    TextMeshProUGUI story;

    string StoryLine;

    float timeSlow = 0.1f;
    float timeFast = 0.02f;

    float nowTime = 0;
    float fullTime = 0;

    int index = 0;

    public bool StoryEnd = false;
    public GameObject nextButton;
    private void Awake()
    {
        story = GetComponent<TextMeshProUGUI>();
        StoryLine = story.text;
        fullTime = timeSlow;
        story.text = "";
    }

    // Update is called once per frame
    private void Update()
    {
        if(Input.touchCount > 0)
        {
            fullTime = timeFast;
        }
        else
        {
            fullTime = timeSlow;
        }
    }

    void FixedUpdate()
    {
        Timer();
    }

    void Timer()
    {
        if (nowTime < fullTime) 
        {
            nowTime += Time.deltaTime;
        }
        else if(index < StoryLine.Length)
        {
            story.text += StoryLine[index];
            index++;
            nowTime = 0;
        }
        else if(index == StoryLine.Length)
        {
            nextButton.SetActive(true);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BgmSelecter : MonoBehaviour
{
    public AudioClip start;
    public AudioClip front_stage;
    public AudioClip end_stage;
    public AudioClip clear;
    public AudioClip fail;
    public AudioClip story;
    public AudioClip storyEnd;

    AudioSource audioControl;

    private void Awake()
    {
        audioControl = GetComponent<AudioSource>();
    }

    public void muting(bool mute)
    {
        audioControl.mute = mute;
    }

    public void PlayBGM(int index)
    {
        switch (index)
        {
            case 0:
                {
                    audioControl.clip = start;
                    break;
                }
            case 7:
                {
                    audioControl.clip = fail;
                    break;
                }
            case 3:
                {
                    if(SecurityPlayerPrefs.GetInt("difficulty", 0) < 2)
                    {
                        audioControl.clip = front_stage;
                    }
                    else
                    {
                        audioControl.clip = end_stage;
                    }
                    break;
                }
            case 6:
                {
                    audioControl.clip = story;
                    break;
                }
            case 5:
                {
                    audioControl.clip = storyEnd;
                    break;
                }
        }
        audioControl.Play();
    }
}

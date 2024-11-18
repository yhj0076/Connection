using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;
    public AudioMixer masterMixer;
    public Slider BgmSlider;
    public Slider SfxSlider;
    public bool play;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Debug.LogError("씬에 2개 이상의 사운드 매니저가 존재합니다.");
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        play = false;
        if (SecurityPlayerPrefs.GetFloat("volumeControl_BGM", -1) == -1)
        {
            SecurityPlayerPrefs.SetFloat("volumeControl_BGM", BgmSlider.value);
        }
        else
        {
            if (BgmSlider != null)
            {
                BgmSlider.value = SecurityPlayerPrefs.GetFloat("volumeControl_BGM", -1);
            }
        }

        if (SecurityPlayerPrefs.GetFloat("volumeControl_SFX", -1) == -1)
        {
            SecurityPlayerPrefs.SetFloat("volumeControl_SFX", SfxSlider.value);
        }
        else
        {
            if (SfxSlider != null)
            {
                SfxSlider.value = SecurityPlayerPrefs.GetFloat("volumeControl_SFX", -1);
            }
        }
    }

    public void AudioControlBGM()
    {
        SecurityPlayerPrefs.SetFloat("volumeControl_BGM", BgmSlider.value);
        if (SecurityPlayerPrefs.GetFloat("volumeControl_BGM", 0) == -40f)
        {
            masterMixer.SetFloat("BGM", -80);
        }
        else
        {
            masterMixer.SetFloat("BGM", SecurityPlayerPrefs.GetFloat("volumeControl_BGM", 0));
        }
    }

    public void AudioControlSFX()
    {
        SecurityPlayerPrefs.SetFloat("volumeControl_SFX", SfxSlider.value);
        if (SecurityPlayerPrefs.GetFloat("volumeControl_SFX", 0) == -40f)
        {
            masterMixer.SetFloat("SFX", -80);
        }
        else
        {
            masterMixer.SetFloat("SFX", SecurityPlayerPrefs.GetFloat("volumeControl_SFX", 0));
        }
        play = true;
    }
}

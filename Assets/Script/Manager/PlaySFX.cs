using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class PlaySFX : MonoBehaviour
{
    Slider slider;
    float beforeValue;
    private void Awake()
    {
        slider = GetComponent<Slider>();
        beforeValue = slider.value;
    }

    public void playSFX()
    {
        if (beforeValue != slider.value)
        {
            GetComponent<AudioSource>().Play();
            beforeValue = slider.value;
        }
    }
}

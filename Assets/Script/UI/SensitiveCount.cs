using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SensitiveCount : MonoBehaviour
{
    TextMeshProUGUI text;
    public Slider Slider;
    private void Awake()
    {
        text = GetComponent<TextMeshProUGUI>();
        ChangeSensitive();
    }

    public void ChangeSensitive()
    {
        text.text = Slider.value.ToString();
    }
}

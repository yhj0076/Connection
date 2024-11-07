using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShakeDetect : MonoBehaviour
{
    Vector3 accelerationDir;    // 가속도 계산을 위한 좌표
    public float Sensitivity;   // 흔들기 민감도
    TextMeshProUGUI tmpro;
    public Slider Slider;

    private void Awake()
    {
        tmpro = GetComponent<TextMeshProUGUI>();
        tmpro.color = Color.clear;
    }

    // Update is called once per frame
    void Update()
    {
        isShaked();
    }

    void isShaked()
    {
        accelerationDir = Input.acceleration;
        if (accelerationDir.sqrMagnitude >= Sensitivity)
        {
            tmpro.color = Color.white;
        }
        else
        {
            returnClear();
        }
    }

    void returnClear()
    {
        tmpro.color = Color.Lerp(tmpro.color,Color.clear,Time.deltaTime);
    }


    public void SensitiveChange()
    {
        float value = Slider.value;
        SecurityPlayerPrefs.SetFloat("sensitive",value);
        Sensitivity = value;
    }
}

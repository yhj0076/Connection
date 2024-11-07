using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShakeDetect : MonoBehaviour
{
    Vector3 accelerationDir;    // ���ӵ� ����� ���� ��ǥ
    public float Sensitivity;   // ���� �ΰ���
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

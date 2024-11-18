using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChainingAnimation : MonoBehaviour
{
    public GameObject potions;
    public float chainTime;

    float ttime = 0;

    RectTransform rect;

    float x;
    float y;

    private void Awake()
    {
        rect = GetComponent<RectTransform>();
        y = rect.sizeDelta.y;
        rect.sizeDelta = new Vector2(0, y);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (potions.activeSelf)
        {
            chaining();
        }
        else
        {
            timing();
        }
    }

    void timing()
    {
        if (ttime < 1f)
        {
            ttime += Time.deltaTime;
        }
        else 
        {
            rect.sizeDelta = new Vector2(0, y);
            GetComponent<Image>().color = Color.white;
            potions.SetActive(true);
            x = 0;
            ttime = 0;
        }
    }

    void chaining()
    {
        potions.SetActive(false);
        GetComponent<Image>().color = Color.clear;
    }

    void timing2()
    {
        rect.sizeDelta = new Vector2(0, y);
        GetComponent<Image>().color = Color.white;
        potions.SetActive(true);
        x = 0;
        ttime = 0;
    }
}

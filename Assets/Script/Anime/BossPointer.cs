using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossPointer : MonoBehaviour
{
    public float jumpingTiming;
    float ttime = 0;

    public Sprite anime1;
    public Sprite anime2;

    Image image;

    private void Awake()
    {
        image = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        if(ttime < jumpingTiming)
        {
            ttime += Time.deltaTime;
        }
        else
        {
            image.sprite = anime1;

            Sprite temp = anime1;
            anime1 = anime2;
            anime2 = temp;

            ttime = 0;
        }
    }
}

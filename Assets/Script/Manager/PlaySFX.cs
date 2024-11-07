using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class PlaySFX : MonoBehaviour
{
    bool touched;

    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);    // 가장 먼저 누른 손가락
            Ray ray = Camera.main.ScreenPointToRay(touch.position);
            RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction);

            if (touch.phase == TouchPhase.Began)
            {
                if(hit.collider == this.gameObject)
                {
                    touched = true;
                }
            }

            if (touch.phase == TouchPhase.Ended && touched)
            {
                GetComponent<AudioSource>().Play();
                touched = false;
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseOption : MonoBehaviour
{
    public GameObject up;
    public GameObject down;
    public GameObject left;
    public GameObject right;
    public GameObject cross;
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
                if (hit.collider == up.gameObject||
                    hit.collider == down.gameObject||
                    hit.collider == left.gameObject||
                    hit.collider == right.gameObject||
                    hit.collider == cross.gameObject)
                {
                    this.gameObject.SetActive(false);
                }
            }
        }
    }
}

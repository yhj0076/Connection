using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoBackWindowScript : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        if (gameObject.activeSelf)
        {
            Time.timeScale = 0;
        }
        else
        {
            Time.timeScale = 1;
        }
    }
}

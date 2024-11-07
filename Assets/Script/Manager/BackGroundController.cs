using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGroundController : MonoBehaviour
{
    public GameObject Grass;
    public GameObject Castle;
    // Start is called before the first frame update
    void Start()
    {
        int difficulty = SecurityPlayerPrefs.GetInt("difficulty", 0);

        if(difficulty < 3)
        {
            Grass.SetActive(true);
            Castle.SetActive(false);
        }
        else
        {
            Grass.SetActive(false);
            Castle.SetActive(true);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class NextStageManager : MonoBehaviour
{
    public GameObject diff;
    // Start is called before the first frame update
    void Awake()
    {
        int before = SecurityPlayerPrefs.GetInt("difficulty", 0);
        before++;
        diff.GetComponent<TextMeshProUGUI>().text = "Now Stage\n"+before.ToString();
    }
}

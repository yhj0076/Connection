using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecretCode : MonoBehaviour
{
    public int touched = 0;

    public void clearCode()
    {
        if (touched < 10)
        {
            touched++;
        }
        else
        {
            SecurityPlayerPrefs.DeleteAll();
            touched = 0;
        }
    }
    
}

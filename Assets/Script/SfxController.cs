using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SfxController : MonoBehaviour
{
    /*
     0. 포션 연결하는 소리
     1. 폭탄 터지는 소리
     2. 왕폭탄 터지는 소리
     3. 같은 색 포션 터지는 소리
     */

    public AudioClip[] sounds;

    AudioSource AudioSource;
    // Start is called before the first frame update
    void Start()
    {
        AudioSource = GetComponent<AudioSource>();
    }

    public void PlaySFX(int index)
    {
        AudioSource.clip = sounds[index];
        AudioSource.Play();
    }
}

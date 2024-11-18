using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SfxController : MonoBehaviour
{
    /*
     0. ���� �����ϴ� �Ҹ�
     1. ��ź ������ �Ҹ�
     2. ����ź ������ �Ҹ�
     3. ���� �� ���� ������ �Ҹ�
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

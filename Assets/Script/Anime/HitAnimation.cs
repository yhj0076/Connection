using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitAnimation : MonoBehaviour
{
    Animator animator;
    AudioSource audioSource;
    private void Awake()
    {
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        GetComponent<SpriteRenderer>().color = Color.clear;
    }

    public void hit()
    {
        GetComponent<SpriteRenderer>().color = Color.white;
        animator.SetBool("hit", true);
        audioSource.Play();
    }

    public void hitFalse()
    {
        animator.SetBool("hit", false);
        GetComponent<SpriteRenderer>().color = Color.clear;
    }
}

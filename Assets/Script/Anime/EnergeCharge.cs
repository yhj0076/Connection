using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergeCharge : MonoBehaviour
{
    Animator animator;
    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void charge()
    {
        animator.SetBool("charge", true);
    }

    public void charged()
    {
        animator.SetBool("charge", false);
    }
}

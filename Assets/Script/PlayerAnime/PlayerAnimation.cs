using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    public animeState state;
    Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void toIdle()
    {
        animator.SetInteger("state",0);
        state = animeState.Idle;
    }

    public void toAttack()
    {
        animator.SetInteger("state", 1);
        state = animeState.Attack;
        Invoke("toIdle", 0.1f);
    }

    public void toDamage()
    {
        animator.SetInteger("state", 2);
        state = animeState.Damage;
        Invoke("toIdle", 0.1f);
    }
    
    public void toDeath()
    {
        animator.SetInteger("state", 3);
        state = animeState.Death;
    }

    public enum animeState
    {
        Idle,
        Attack,
        Damage,
        Death
    }
}

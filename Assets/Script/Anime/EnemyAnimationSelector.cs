using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimationSelector : MonoBehaviour
{
    public animeState state;
    Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void toIdle()
    {
        animator.SetInteger("state", 0);
        state = animeState.Idle;
    }

    public void toAttack()
    {
        animator.SetInteger("state", 1);
        state = animeState.Attack;
    }

    public void toDamage()
    {
        animator.SetInteger("state", 2);
        state = animeState.Damage;
        transform.GetChild(0).GetComponent<HitAnimation>().hit();
    }

    public void toDeath()
    {
        animator.SetInteger("state", 3);
        state = animeState.Death;
    }

    public void slow()
    {
        animator.speed = 0.5f;
        animator.updateMode = AnimatorUpdateMode.Normal;
    }

    public void clear()
    {
        GameObject gameManager = FindObjectOfType<GameManager>().gameObject;
        gameManager.GetComponent<GameManager>().Clear();
    }

    public enum animeState
    {
        Idle,
        Attack,
        Damage,
        Death
    }
}

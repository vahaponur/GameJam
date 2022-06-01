using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyHealthHandler : MonoBehaviour
{
    public float Health = 100f;
    private Animator _animator;
    private EnemyMover MOVER;
    [SerializeField] bool isBoss;
    private void Awake()
    {
        if (isBoss)
        {
            Health = 150f;
        }
        _animator =GetComponent<Animator>();
        MOVER = GetComponent<EnemyMover>();
    }

    public void GetDamage(float damage)
    {
        Health -= damage;
        if (!isBoss)
        {
            Health = Mathf.Clamp(Health, 0, 100);
        }
        else
        {
            Health = Mathf.Clamp(Health, 0, 150);

        }

    }

    private void Update()
    {
        if (Health <= 0)
        {
            GetComponent<NavMeshAgent>().height = 0;
            _animator.SetTrigger("Death");
            MOVER.EnemyState = ENEMYSTATE.DEATH;
        }
    }
}

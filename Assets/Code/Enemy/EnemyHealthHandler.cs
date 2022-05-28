using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyHealthHandler : MonoBehaviour
{
    private float Health = 100f;
    private Animator _animator;
    private EnemyMover MOVER;
    private void Awake()
    {
        _animator =GetComponent<Animator>();
        MOVER = GetComponent<EnemyMover>();
    }

    public void GetDamage(float damage)
    {
        Health -= damage;

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

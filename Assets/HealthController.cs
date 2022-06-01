using System;
using System.Collections;
using System.Collections.Generic;
using GameJam;
using UnityEngine;
using Random = UnityEngine.Random;

[RequireComponent(typeof(InputController),typeof(Animator))]
public class HealthController : MonoBehaviour
{
    public float health = 100f;
    private Animator _animator;
    private bool dead = false;
    bool hitHandled = false;
    private InputController inputC;
    private void Start()
    {
        inputC = GetComponent<InputController>();
        _animator = GetComponent<Animator>();
        
    }

    private void Update()
    {
        if (inputC.Attack)
        {
            StopCoroutine(ReturnFromHit());
                      
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("EnemySword") )
        {
            var swo = other.GetComponent<EnemySword>();
            EnemyMover sw = null;
            if (swo)
            {
                sw = swo._mover;
                if (sw.EnemyState == ENEMYSTATE.ATTACK && !hitHandled &&  PlayerDataSingleton.Instance.PlayerState != PLAYERSTATE.ATTACK )
                {
                    health -= Random.Range(2, 10f);
                    _animator.SetTrigger("HitTrigger");

                    hitHandled = true;
                    StartCoroutine(ReturnFromHit());
                   
                }
            }
       
        }

        if (health <= 0 && !dead)
        {
            _animator.SetTrigger("death");
            dead = true;
            GetComponent<MovementManager>().enabled = false;
        }
    }
    IEnumerator ReturnFromHit()
    {
     
        yield return new WaitForSeconds(1.2f);
        _animator.SetTrigger("HitReturnTrigger");

        hitHandled = false;
    }
}

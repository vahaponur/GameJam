using System;
using System.Collections;
using System.Collections.Generic;
using GameJam;
using UnityEngine;
using UnityEngine.AI;

public enum ENEMYSTATE
{
    IDLE,
    CHASE,
    ATTACK,
    HITTED,
    DEATH
}
public class EnemyMover : MonoBehaviour
{
    #region Serialized Fields

    [SerializeField,Range(1, 10)] private float chaseRange;
    [SerializeField,Range(0, 2)] private float attackRange;
    
    #endregion

    #region Private Fields

    private bool hitHandled;
    private bool secondAttackTriggered;
    private Animator _animator;
    private NavMeshAgent _navMeshAgent;
    private Vector3 worldDeltaPos;
    private Vector2 groundDeltaPos;
    Vector2 enemyVelocity = Vector2.zero;
    private Transform player;
    [HideInInspector]public ENEMYSTATE EnemyState;
    [SerializeField] private ParticleSystem _particleSystem;
    #endregion

    #region Public Properties
    #endregion

    #region MonoBehaveMethods

    void Awake()
    {
        _animator = GetComponent<Animator>();
        _navMeshAgent = GetComponent<NavMeshAgent>();
        _navMeshAgent.updatePosition = false;
    }

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        _navMeshAgent.updatePosition = false;
    }
    

   
    void Update()
    {
        _navMeshAgent.updatePosition = false;
        DecideState();
        if (EnemyState ==ENEMYSTATE.IDLE)
        {
            _navMeshAgent.ResetPath();
        }
      
        HandleChase();
        if (EnemyState == ENEMYSTATE.ATTACK)
        {
            _navMeshAgent.updatePosition = false;
            transform.LookAt(player);
            _animator.SetBool("isAttacking",true);
            if (!secondAttackTriggered)
            {
                StartCoroutine(StartSecondAttack());
                secondAttackTriggered = true;
            }
            
        }
        
       
    }

    void HandleChase()
    {
        if (EnemyState == ENEMYSTATE.CHASE)
        {
            _navMeshAgent.SetDestination(player.position);
        }
        worldDeltaPos = EnemyState == ENEMYSTATE.CHASE ? _navMeshAgent.nextPosition - transform.position:Vector3.zero;
        if (worldDeltaPos != Vector3.zero)
        {
            groundDeltaPos.x = Vector3.Dot(transform.right, worldDeltaPos);
            groundDeltaPos.y = Vector3.Dot(transform.forward, worldDeltaPos);
        }
        else groundDeltaPos = Vector2.zero;

        enemyVelocity =(Time.deltaTime > 1e-5f) ? groundDeltaPos / Time.deltaTime:Vector2.zero;
        var shouldMove = enemyVelocity.magnitude > 0.01f && _navMeshAgent.remainingDistance > _navMeshAgent.radius && EnemyState != ENEMYSTATE.ATTACK;
        if (shouldMove)
        {
            _animator.SetFloat("velX",enemyVelocity.x);
            _animator.SetFloat("velY",enemyVelocity.y);  
        }
        else
        {
            _animator.SetFloat("velX",Mathf.Lerp(_animator.GetFloat("velX"),0,Time.deltaTime*2));
            _animator.SetFloat("velY",Mathf.Lerp(_animator.GetFloat("velY"),0,Time.deltaTime*2)); 
        }
      

    }
    private void OnAnimatorMove()
    {
        transform.position = _navMeshAgent.nextPosition;
    }

    float GetMagToPlayer()
    {
        return Vector3.Distance(transform.position, player.position);
    }
    void DecideState()
    {
        if (EnemyState != ENEMYSTATE.DEATH)
        {
            var distance = GetMagToPlayer();
            EnemyState = ENEMYSTATE.IDLE;
            if (distance <= chaseRange)
                EnemyState = ENEMYSTATE.CHASE;
            if (distance<=attackRange)
                EnemyState = ENEMYSTATE.ATTACK;

        }
    
        
        
    }

    IEnumerator StartSecondAttack()
    {
        yield return new WaitForSeconds(0.9f);
        if (EnemyState == ENEMYSTATE.ATTACK)
        {
            bool onAttack = _animator.GetBool("isAttacking");
            bool onSecondAttack = _animator.GetBool("isSecondAttacking");
            if ( onAttack&&!onSecondAttack)
            {
                _animator.SetBool("isAttacking",false);
                _animator.SetBool("isSecondAttacking",true);
            }
            else if (!onAttack && onSecondAttack)
            {
                _animator.SetBool("isAttacking",true);
                _animator.SetBool("isSecondAttacking",false);
            }
            else
            {
              
            }
            
        }
        else
        {
            _animator.SetBool("isAttacking",false);
            _animator.SetBool("isSecondAttacking",false);
        }

        secondAttackTriggered = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (PlayerDataSingleton.Instance.PlayerState == PLAYERSTATE.ATTACK && EnemyState != ENEMYSTATE.DEATH)
        {
            if (other.CompareTag("PlayerSword") && !hitHandled)
            {
                EnemyState = ENEMYSTATE.HITTED;
                hitHandled = true;
                _particleSystem.transform.position = other.ClosestPointOnBounds(transform.position);
                _particleSystem.PlayWithLogic();
                _animator.SetTrigger("HitTrigger");
                StartCoroutine(ReturnFromHit());
            }
        }
    }

    #endregion

    IEnumerator ReturnFromHit()
    {
        yield return new WaitForSeconds(1.1f);
        _animator.SetTrigger("HitReturnTrigger");
        hitHandled = false;
    }
    
    #region PublicMethods
    #endregion
    
    #region PrivateMethods
    #endregion
}

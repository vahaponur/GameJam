using System;
using System.Collections;
using System.Collections.Generic;
using GameJam;
using UnityEngine;

namespace GameJam
{
    public enum ATTACKANIMTYPE
    {
        FIRST,
        SECOND
        
    }
    [RequireComponent(typeof(Animator),typeof(MovementManager))]
    public class AnimValueController : MonoBehaviour
    {
        private Animator animator;
        private MovementManager movementManager;
        private InputController inputC;
        private long currentAttackClicks;
        private float animDeltaTime;
        public bool OnAttack;
        private bool lastAnimFinished = true;
        private ATTACKANIMTYPE currentAttackType;
        private bool thereIsNextAttack;
        
        private void Start()
        {
            animator = GetComponent<Animator>();
            movementManager = GetComponent<MovementManager>();
            inputC = GetComponent<InputController>();
        }
    
        private void Update()
        {
            SetAnimRootMotion(movementManager.PlayerIsGrounded);
            var pState = PlayerDataSingleton.Instance.PlayerState;
            bool idle = pState == PLAYERSTATE.IDLE;
            bool walk = pState == PLAYERSTATE.WALK;
            bool run = pState == PLAYERSTATE.RUN;
            bool jump = pState == PLAYERSTATE.JUMP;
            bool attack = pState == PLAYERSTATE.ATTACK;
            if (idle || walk || run )
            {
                SetWalkSpeed(movementManager.InputVel);
            }
            
            SetJumpAnim(jump);
            if (jump)
            {
                SetJumpVelocity(movementManager.InputVel);
            }

           SetAttackAnims();
            
            
        }
        //TODO..
        void SetAttackAnims()
        {
            
            if (OnAttack && inputC.Attack && currentAttackClicks ==0)
            {
                thereIsNextAttack = true;
                currentAttackClicks++;
           
            }
            SetAttackStates();
            if (OnAttack && !lastAnimFinished)
            {
                animDeltaTime += Time.deltaTime;
            }

            if (OnAttack && currentAttackType == ATTACKANIMTYPE.FIRST)
            {
                animator.SetBool("isAttacking",true );
                animator.SetBool("isSecondAttacking",false);
            }

            if (OnAttack && currentAttackType == ATTACKANIMTYPE.SECOND)
            {
                animator.SetBool("isAttacking",false );
                animator.SetBool("isSecondAttacking",true);
            }

            if (currentAttackType==ATTACKANIMTYPE.FIRST && animDeltaTime>0.974615f )
            {
                lastAnimFinished = true;
                animDeltaTime = 0f;
                currentAttackClicks = 0;

            }

            if (currentAttackType == ATTACKANIMTYPE.SECOND && animDeltaTime > 1.397623f )
            {
                lastAnimFinished = true;
                animDeltaTime = 0f;
                currentAttackClicks = 0;
     
            }
          

            if (thereIsNextAttack && lastAnimFinished )
            {
                currentAttackType = currentAttackType == ATTACKANIMTYPE.FIRST
                    ? ATTACKANIMTYPE.SECOND
                    : ATTACKANIMTYPE.FIRST;
                lastAnimFinished = false;
                thereIsNextAttack = false;
    

            }

            if (thereIsNextAttack && !lastAnimFinished)
            {
           
            }
      
      
         

          
            
        }

        void SetAttackStates()
        {
            if (!OnAttack&&inputC.Attack && lastAnimFinished)
            {
                OnAttack = true;
                currentAttackType = ATTACKANIMTYPE.FIRST;
                lastAnimFinished = false;
      
                animator.SetBool("isAttacking",currentAttackType == ATTACKANIMTYPE.FIRST);

            }
            else if (lastAnimFinished && !thereIsNextAttack)
            {
                animator.SetBool("isAttacking",false);
                animator.SetBool("isSecondAttacking",false);
                currentAttackType = ATTACKANIMTYPE.FIRST;
                OnAttack = false;
            }
            
        }

   
    
        void SetWalkSpeed(float inputVel)
        {
            animator.SetFloat("Speed",inputVel,0.1f,Time.deltaTime);
        }
    
        void SetJumpAnim(bool value)
        {
            animator.SetBool("isJumping",value);
        }
    
        void SetJumpVelocity(float velOnJump)
        {
            animator.SetFloat("InputMag",velOnJump);
        }
    
        void SetAnimRootMotion(bool isGrounded)
        {
            animator.applyRootMotion = isGrounded;
        }

        
        void SetAttackAnim(bool val)
        {
            animator.SetBool("isAttacking",val);
        }
    }

}

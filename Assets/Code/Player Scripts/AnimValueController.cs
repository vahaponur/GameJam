using System;
using System.Collections;
using System.Collections.Generic;
using GameJam;
using UnityEngine;

namespace GameJam
{
    [RequireComponent(typeof(Animator),typeof(MovementManager))]
    public class AnimValueController : MonoBehaviour
    {
        private Animator animator;
        private MovementManager movementManager;
        private InputController inputC;
        private long currentAttackClicks;
        private float animDeltaTime;
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

            SetAttackStates();
            
        }
        //TODO..
        void SetAttackStates()
        {
            var firstAttack = animator.GetBool("isAttacking");
            var secondAttack = animator.GetBool("isSecondAttacking");
            if (inputC.Attack)
            {
                currentAttackClicks++;
                
            }

            if (firstAttack || secondAttack)
            {
                animDeltaTime += Time.deltaTime;
            }

            if (currentAttackClicks % 2 == 1 && inputC.Attack && animDeltaTime <0.97)
            {
                animator.SetBool("isAttacking",true);
                animator.SetBool("isSecondAttacking",false);
                
            }
            if (currentAttackClicks % 2 ==0 && inputC.Attack && animDeltaTime > 0.97)
            {
                animator.SetBool("isAttacking",false);
                animator.SetBool("isSecondAttacking",true);
            }

            

            if (animDeltaTime>2.367f)
            {
                animator.SetBool("isAttacking",false);
                animator.SetBool("isSecondAttacking",false);
                animDeltaTime = 0f;
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

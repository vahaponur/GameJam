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
    
        private void Start()
        {
            animator = GetComponent<Animator>();
            movementManager = GetComponent<MovementManager>();
        }
    
        private void Update()
        {
            SetAnimRootMotion(movementManager.PlayerIsGrounded);
            var pState = PlayerDataSingleton.Instance.PlayerState;
            bool idle = pState == PLAYERSTATE.IDLE;
            bool walk = pState == PLAYERSTATE.WALK;
            bool run = pState == PLAYERSTATE.RUN;
            bool jump = pState == PLAYERSTATE.JUMP;
            if (idle || walk || run )
            {
                SetWalkSpeed(movementManager.InputVel);
            }
            
            SetJumpAnim(jump);
            if (jump)
            {
                SetJumpVelocity(movementManager.InputVel);
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
    }

}

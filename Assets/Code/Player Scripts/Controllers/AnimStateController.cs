using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameJam
{
 
    public class AnimStateController : MonoBehaviour
    {
        #region Private Fields

        
       [SerializeField]private Animator _animator;
        

        #endregion

        #region Private Methods

        private void Awake()
        {
            GetInternalDependencies();
        }

        void GetInternalDependencies()
        {
            if (_animator is null)
            {
                _animator = GetComponent<Animator>();

            }
        }

        public void UpdateAnimator()
        {
            SetIdle();
            SetWalk();
            
        }
        void SetWalk()
        {
            if (PlayerDataSingleton.Instance.PlayerState == PLAYERSTATE.WALK)
            {
                _animator.SetTrigger("Walking");
                
            }
        }

        void SetIdle()
        {
            if (PlayerDataSingleton.Instance.PlayerState == PLAYERSTATE.IDLE)
            {
                _animator.SetTrigger("Idle");
            }
        }

        #endregion


    }
}


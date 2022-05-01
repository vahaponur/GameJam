using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameJam
{
    
 
    public class AnimStater : MonoBehaviour
    
    {
        enum AnimState
        {
            Idle,
            Walk
        }
        #region Private Fields

        private int _state;
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
            
            _state = (int)PlayerDataSingleton.Instance.PlayerState;
            _animator.SetInteger("state",_state);

        }
  
        
        #endregion


    }
}


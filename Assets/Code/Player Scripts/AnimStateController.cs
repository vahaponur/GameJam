using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameJam
{
    public class AnimStateController : MonoBehaviour
    {
        #region Private Fields

        
        private Animator _animator;
        

        #endregion

        #region Private Methods

        private void Awake()
        {
            GetInternalDependencies();
        }

        void GetInternalDependencies()
        {
            _animator = GetComponent<Animator>();
        }

        void SetWalk()
        {
            if (PlayerDataSingleton.Instance.PlayerState == PLAYERSTATE.WALK)
            {
                _animator.SetTrigger("Walking");
            }
        }

        #endregion


    }
}


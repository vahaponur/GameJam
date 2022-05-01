using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameJam
{
    /// <summary>
    /// Controls all the inputs from player, scripts that need input should have a reference to it.
    /// </summary>
    public class InputController : MonoBehaviour
    {
        
        #region Private Fields

        private float horizontalInput, verticalInput, attackInput;

        #endregion

        #region Public Properties
        public float HorizontalInput => horizontalInput;

        public float VerticalInput => verticalInput;

        public float AttackInput => attackInput;

        #endregion

        #region MonoBehaveMethods
    
        void Update()
        {
            SetXY();
            SetFightInput();
        }
        #endregion
        
        #region PrivateMethods
        /// <summary>
        /// Sets vertical and horizontal input
        /// </summary>
        void SetXY()
        {
            horizontalInput = Input.GetAxis("Horizontal");
            verticalInput = Input.GetAxis("Vertical");
        }

        void SetFightInput()
        {
            attackInput = Input.GetAxis("Fire1");
        }

        public bool OnlyMoving()
        {
            bool b1 = horizontalInput != 0 || verticalInput != 0;
            bool b2 = attackInput == 0;
            return b1 & b2;
        }
        #endregion
    }
}


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
        #endregion
    }
}


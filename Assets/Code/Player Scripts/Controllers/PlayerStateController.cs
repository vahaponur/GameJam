using System;
using UnityEngine;

namespace GameJam
{
    [RequireComponent(typeof(MovementManager),typeof(InputController))]
    public class PlayerStateController : MonoBehaviour
    {
        private MovementManager _movementManager;
        private InputController inputController;
        private AnimValueController _animValueController;

        private void Start()
        {
            _movementManager = GetComponent<MovementManager>();
            inputController = GetComponent<InputController>();
            _animValueController = GetComponent<AnimValueController>();
        }

        private void Update()
        {
            PlayerDataSingleton.Instance.PlayerState = PLAYERSTATE.IDLE;

            #region Needed Variables

            float walkInput = _movementManager.InputVel;
            bool grounded = _movementManager.PlayerIsGrounded;
            float isJumpInput = inputController.JumpAxis;

            #endregion

            if (_animValueController.OnAttack)
                PlayerDataSingleton.Instance.PlayerState = PLAYERSTATE.ATTACK;
            
            else if (isJumpInput>0 && !grounded)
                PlayerDataSingleton.Instance.PlayerState = PLAYERSTATE.JUMP;
            
            else if (walkInput > 0 && walkInput<1.8f)
                PlayerDataSingleton.Instance.PlayerState = PLAYERSTATE.WALK;
            
            
        }
    }
}
using System;
using UnityEngine;

namespace GameJam
{
    public class PlayerStateController : MonoBehaviour
    {
        private CharacterController _characterController;

        private void Awake()
        {
            _characterController = GetComponent<CharacterController>();
        }

        public void UpdatePlayerState(InputController inputController)
        {
            SetStateAccordingInputAndCC(inputController);
        }

        public void SetStateAccordingInputAndCC(InputController inputController)
        {
            Vector2 movementVector = new Vector2(inputController.HorizontalInput, inputController.VerticalInput);
            if (movementVector.sqrMagnitude < 0.05 && inputController.AttackInput == 0)
            {
                PlayerDataSingleton.Instance.PlayerState = PLAYERSTATE.IDLE;
            }


            if (movementVector.sqrMagnitude >= 0.05f && movementVector.sqrMagnitude < 0.1 && inputController.OnlyMoving())
            {
                PlayerDataSingleton.Instance.PlayerState = PLAYERSTATE.TURN;
            }

            if (movementVector.sqrMagnitude >= 0.1 && inputController.AttackInput ==0  && inputController.OnlyMoving())
            {
                PlayerDataSingleton.Instance.PlayerState = PLAYERSTATE.WALK;
            }

            if (inputController.AttackInput>0)
            {
                PlayerDataSingleton.Instance.PlayerState = PLAYERSTATE.ATTACK;
            }
        }
    }
}
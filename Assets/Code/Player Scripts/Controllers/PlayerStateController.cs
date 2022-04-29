using UnityEngine;

namespace GameJam
{
    public class PlayerStateController:MonoBehaviour
    {
        private MovementController _movementController;
        private InputController _inputController;

        public void UpdatePlayerState()
        {
          
            SetStateAccordingRB();
        }
        public void SetStateAccordingRB()
        {
            if ( _movementController.Rb.velocity.magnitude > 0.001)
            {
                PlayerDataSingleton.Instance.PlayerState = PLAYERSTATE.WALK;
                return;                
            }

            PlayerDataSingleton.Instance.PlayerState = PLAYERSTATE.IDLE;

        }

        public void GetDependencies(MovementController movementController, InputController inputController)
        {
            _inputController = inputController;
            _movementController = movementController;
        }
    }
}
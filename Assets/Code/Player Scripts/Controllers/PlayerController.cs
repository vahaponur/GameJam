using System;
using UnityEngine;

namespace GameJam
{
    [RequireComponent(typeof(MovementController),typeof( AnimStater),typeof(InputController))]
    [RequireComponent(typeof(PlayerStateController))]
    public class PlayerController:MonoBehaviour
    {
        private MovementController _movementController;
        private AnimStater _animStater;
        private InputController _inputController;
        private PlayerStateController _playerStateController;
        private void Awake()
        {
            var colliders = GameObject.FindGameObjectsWithTag("PlayerCollider");
            GetInternalDependencies();
            colliders.DisableAll();
        }

        private void Start()
        {
            
        }

        void GetInternalDependencies()
        {
            _movementController = GetComponent<MovementController>();
            _animStater = GetComponent<AnimStater>();
            _inputController = GetComponent<InputController>();
            _playerStateController = GetComponent<PlayerStateController>();
        }

      

        private void Update()
        {
            _animStater.UpdateAnimator();
            _playerStateController.UpdatePlayerState(_inputController);
            _movementController.UpdateTransformMovement(_inputController);
        }
    }
}
using System;
using UnityEngine;

namespace GameJam
{
    [RequireComponent(typeof(MovementController),typeof( AnimStateController),typeof(InputController))]
    public class PlayerController:MonoBehaviour
    {
        private MovementController _movementController;
        private AnimStateController _animStateController;
        private InputController _inputController;

        private void Awake()
        {
            var colliders = GameObject.FindGameObjectsWithTag("PlayerCollider");
            GetInternalDependencies();
            colliders.DisableAll();
        }

        void GetInternalDependencies()
        {
            _movementController = GetComponent<MovementController>();
            _animStateController = GetComponent<AnimStateController>();
            _inputController = GetComponent<InputController>();
        }

        private void FixedUpdate()
        {
            _movementController.UpdateMovement(_inputController);
            
        }

        private void Update()
        {
            _animStateController.UpdateAnimator();
        }
    }
}
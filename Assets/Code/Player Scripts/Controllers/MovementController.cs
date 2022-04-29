using System;
using UnityEngine;

namespace GameJam
{
    public class MovementController : BaseRBController
    {
        [SerializeField,Range(300,700)] private float speed= 500f;
        [SerializeField,Range(1,5)] private float rotSpeed= 2f;
        [SerializeField,Range(1,25)] private float maxVelocity = 10f;
        public void UpdateMovement(InputController inputController)
        {
            SetStateAccordingRB();
            SetChangeConstraints();
            float vel = rb.velocity.magnitude;
            if (vel < maxVelocity)
            {
                float clamper = (maxVelocity - vel) / maxVelocity;
                rb.AddForce(transform.forward*inputController.VerticalInput*speed*clamper);
                

            }

            if (rb.angularVelocity.magnitude < 1)
            {
                float maxRot = 3;
                float clamper = (maxRot - rb.angularVelocity.magnitude) / maxRot;
                rb.AddTorque(inputController.HorizontalInput*transform.up *rotSpeed* clamper,ForceMode.VelocityChange);
                rb.angularDrag = clamper;
                SetDefaultConstraints();
            }
            
            
        }

        private void OnCollisionEnter(Collision other)
        {
            rb.angularVelocity = Vector3.zero;
        }

        void SetStateAccordingRB()
        {
            if ( rb.velocity.magnitude > 0.05)
            {
                PlayerDataSingleton.Instance.PlayerState = PLAYERSTATE.WALK;
                return;                
            }

            PlayerDataSingleton.Instance.PlayerState = PLAYERSTATE.IDLE;

        }
        void SetDefaultConstraints()
        {
            rb.constraints = RigidbodyConstraints.FreezeRotation;
        }

        void SetChangeConstraints()
        {
            rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
        }
        protected override void Reset()
        {
            base.Reset();
            speed = 500f;
            maxVelocity = 10f;
            rotSpeed = 2f;
        }
    }
}
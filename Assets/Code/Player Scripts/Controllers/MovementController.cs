using System;
using UnityEngine;

namespace GameJam
{
    public class MovementController : BaseRBController
    {
        #region Serialized Fields
        [Header("Movement Properties")]
        [SerializeField,Range(300,700),Tooltip("Force rate to add to rb on input")] private float speed= 500f;
        [SerializeField,Range(1,10),Tooltip("Torque rate to add to rb on input")] private float torqueSpeed= 2f;
        [SerializeField,Range(1,25),Tooltip("Maximum rigidbody velocity magnitude")] private float maxVelocity = 10f;
        [SerializeField,Range(1,10),Tooltip("Maximum angular velocity magnitude")] private float maxTorque =3f;
        

        #endregion

        #region Private Fields
        private float currentVel;
        private float currentTorque;
        
        #endregion

        #region Public Methods
        public void UpdateMovement(InputController inputController)
        {
            SetStateAccordingRB();
            SetChangeConstraints();
            AddClampedForce(inputController);
            AddRotationTorque(inputController);
            SetDefaultConstraints();
            
        }

        #endregion

        #region MonoBehave Methods

        protected override void Reset()
        {
            base.Reset();
            speed = 500f;
            maxVelocity = 10f;
            torqueSpeed = 2f;
            maxTorque = 3f;
        }

        #endregion

        #region PrivateMethods
        /// <summary>
        /// Sets the rotation of player, clamping unnecessary acceleration
        /// </summary>
        /// <param name="inputController">Input Controller</param>
        void AddRotationTorque(InputController inputController)
        {
            float currentTorque = rb.angularVelocity.magnitude;
            if (currentTorque < maxTorque)
            {
                float clamper = (maxTorque - currentTorque) / maxTorque;
                rb.AddTorque(inputController.HorizontalInput*transform.up *torqueSpeed* clamper,ForceMode.VelocityChange);
                
                //might verbose: just increasing angular drag while torque speed reaches the maxRot speed
                rb.angularDrag = clamper;
            }
        }
        /// <summary>
        /// Sets the position change of player, clamping unnecessary acceleration
        /// </summary>
        /// <param name="inputController"></param>
        void AddClampedForce(InputController inputController)
        {
            currentVel = rb.velocity.magnitude;
            if (currentVel < maxVelocity)
            {
                float clamper = (maxVelocity - currentVel) / maxVelocity;
                rb.AddForce(transform.forward*inputController.VerticalInput*speed*clamper);
                
                //might verbose: increasing drag while velocity reaches maxVelocity
                rb.drag = clamper;

            }
        }
        

        void SetStateAccordingRB()
        {
            if ( rb.velocity.magnitude > 0.1)
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
        

        #endregion

    
    
    }
}
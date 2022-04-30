using System;
using Cinemachine;
using UnityEngine;

namespace GameJam
{
    public class MovementController : BaseRBController
    {
        #region Serialized Fields

        [Header("Movement Properties")] [SerializeField, Range(300, 700), Tooltip("Force rate to add to rb on input")]
        private float speed = 500f;

        [SerializeField, Range(1, 10), Tooltip("Torque rate to add to rb on input")]
        private float torqueSpeed = 2f;

        [SerializeField, Range(1, 25), Tooltip("Maximum rigidbody velocity magnitude")]
        private float maxVelocity = 10f;

        [SerializeField, Range(1, 10), Tooltip("Maximum angular velocity magnitude")]
        private float maxTorque = 3f;

        #endregion

        #region Private Fields

        private float currentVel;
        private float currentTorque;
        private Camera cam;
        private CharacterController controller;
        #endregion

        #region Public Properties

        public Rigidbody Rb => rb;

        #endregion

        protected void Start()
        {
            cam = FindObjectOfType<Camera>();
            controller = GetComponent<CharacterController>();
        }

        #region Public Methods

      

        public void UpdateTransformMovement(InputController inputController)
        {
            MoveWithCC(inputController);
            RotateToCam(inputController);
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
        /// Movement with character controller
        /// </summary>
        /// <param name="inputController"></param>
        void MoveWithCC(InputController inputController)
        {
           
            var totalInput = new Vector3(inputController.HorizontalInput, 0, inputController.VerticalInput);
            var magnitude = Mathf.Clamp(totalInput.magnitude, -1, 1); ;
            

            if (magnitude != 0)
            {
                PlayerDataSingleton.Instance.PlayerState = PLAYERSTATE.WALK;
                //Appliying gravity on input may need to change
                controller.Move(-transform.up * 9.81f * Time.deltaTime);
                
                controller.Move(Mathf.Abs(magnitude) * transform.forward * 3 * Time.deltaTime);
            }
            else
            {
                PlayerDataSingleton.Instance.PlayerState = PLAYERSTATE.IDLE;
            }
        }
        /// <summary>
        /// Rotates to cam right or forward according to input (horizontal- right turn,vertical forward turn)
        /// </summary>
        /// <param name="inputController">Input Controller</param>
        void RotateToCam(InputController inputController)
        {
            
            Vector3 neededForward = new Vector3(cam.transform.right.x, 0, cam.transform.right.z);
            Quaternion lerpVector = Quaternion.LookRotation(neededForward * inputController.HorizontalInput);
            if (Input.GetAxis("Horizontal") > 0.1f || Input.GetAxis("Horizontal") < -0.1f)
            {
                transform.rotation = Quaternion.Slerp(transform.rotation, lerpVector, 5 * Time.deltaTime);
            }

            Vector3 neededForward2 = new Vector3(cam.transform.forward.x, 0, cam.transform.forward.z);
            Quaternion lerpVector2 = Quaternion.LookRotation(neededForward2 * inputController.VerticalInput);

            if (Input.GetAxis("Vertical") > 0.1f || Input.GetAxis("Vertical") < -0.1f)
            {
                transform.rotation = Quaternion.Slerp(transform.rotation, lerpVector2, 5 * Time.deltaTime);
            }
        }


        #region Obsolute
        public void UpdaterRbMovement(InputController inputController)
        {
            //SetChangeConstraints();
            //AddClampedForce(inputController);
            //AddRotationTorque(inputController);
            //SetDefaultConstraints();
        }
        /// <summary>
        /// Sets the position change of player, clamping unnecessary acceleration
        /// </summary>
        /// <param name="inputController"></param>
        [Obsolete]
        void AddClampedForce(InputController inputController)
        {
            currentVel = rb.velocity.magnitude;
            if (currentVel < maxVelocity)
            {
                float clamper = (maxVelocity - currentVel) / maxVelocity;
                rb.AddForce(transform.forward * Mathf.Abs(inputController.VerticalInput) * speed * clamper);

                //might verbose: increasing drag while velocity reaches maxVelocity
                rb.drag = clamper;
            }
        }


        [Obsolete]
        void SetDefaultConstraints()
        {
            rb.constraints = RigidbodyConstraints.FreezeRotation;
        }

        [Obsolete]
        void SetChangeConstraints()
        {
            rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
        }

        /// <summary>
        /// Sets the rotation of player, clamping unnecessary acceleration
        /// </summary>
        /// <param name="inputController">Input Controller</param>
        [Obsolete]
        void AddRotationTorque(InputController inputController)
        {
            float currentTorque = rb.angularVelocity.magnitude;
            if (currentTorque < maxTorque)
            {
                float clamper = (maxTorque - currentTorque) / maxTorque;
                rb.AddTorque(inputController.HorizontalInput * transform.up * torqueSpeed * clamper,
                    ForceMode.VelocityChange);

                //might verbose: just increasing angular drag while torque speed reaches the maxRot speed
                rb.angularDrag = clamper;
            }
        }

        #endregion

        #endregion
    }
}
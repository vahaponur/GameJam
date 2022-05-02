using System;
using System.Collections;
using Cinemachine;
using UnityEngine;

namespace GameJam
{
    public class MovementController : BaseRBController
    {
        #region Serialized Fields

        [SerializeField,Range(1,5)] float jumpHeight = 1.0f;
        [SerializeField] private Animator skinAnim;
        #endregion

        #region Private Fields

     
        private Camera cam;
        private CharacterController controller;
        private Vector3 playerVelocity;
        private bool groundedPlayer;
        private float gravityValue = -9.81f;
        private GameObject groundChecker;
        private LayerMask mask;

        #region Obsolute
        private float currentVel;
        private float currentTorque;
        public Rigidbody Rb => rb;

      
        #endregion
        #endregion

        #region Public Properties

        

        #endregion

        protected void Start()
        {
            groundChecker = transform.Find("GroundChecker1").gameObject;

            cam = FindObjectOfType<Camera>();
            controller = GetComponent<CharacterController>();
        }

        #region Public Methods

      

        public void UpdateTransformMovement(InputController inputController)
        {
            AttackStateHandler();
            MoveWithCC(inputController);
            RotateToCam(inputController);
            Sprint();
        }

        #endregion

        #region MonoBehave Methods

        protected override void Reset()
        {
            base.Reset();
    
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
            groundedPlayer = isGrounded();
            if (groundedPlayer && playerVelocity.y < 0)
            {
                playerVelocity.y = 0f;
            }

            if (magnitude != 0)
            {
            
                //Appliying gravity on input may need to change
                
                
                controller.Move(Mathf.Abs(magnitude) * transform.forward * 3 * Time.deltaTime);
            }
        
            // Changes the height position of the player..
            if (Input.GetButtonDown("Jump") && groundedPlayer)
            {
                playerVelocity.y += Mathf.Sqrt(jumpHeight * -gravityValue);
            }

            playerVelocity.y += gravityValue*1.5f * Time.deltaTime;
            controller.Move(playerVelocity * Time.deltaTime);
        
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
        /// <summary>
        /// Ture if player grounded
        /// </summary>
        /// <returns></returns>
        bool isGrounded()
        {

             mask =~ LayerMask.GetMask("Player");
            var cols =Physics.OverlapSphere(groundChecker.transform.position, 0.2f,mask);
            return cols.Length>0 ;
        }

        void Sprint()
        {
            if (Input.GetKeyDown(KeyCode.LeftShift))
            {
                StartCoroutine("SprintOverTime");
            }
        }

        IEnumerator SprintOverTime()
        {
     
            float time = 0;
            while (time < 0.5f) 
            {
                controller.Move(transform.forward*5* Time.deltaTime );
            
                time += Time.deltaTime;
                yield return null;
            }
            
        }
       
        void AttackStateHandler(){
            if (PlayerDataSingleton.Instance.PlayerState == PLAYERSTATE.ATTACK)
            {
           
            
            }

         


        }
    
        #endregion
    }
}
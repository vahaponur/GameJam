using System;
using UnityEngine;

namespace GameJam
{
    [RequireComponent(typeof(InputController),typeof(CharacterController),typeof(Animator))]
    public class MovementManager : MonoBehaviour
    {
        [Header("Movement Dependencies")]
        [SerializeField,Tooltip("Main Cam, NOT Cinemachine")] Camera cam;
        [SerializeField]private GameObject groundChecker;
        [Header("Movement Properties")]
        [SerializeField,Range(1,5)]private float jumpHeight;
 
        private InputController inputController;
        private CharacterController _controller;
        
        [SerializeField] LayerMask mask;
        private Vector3 playerVelocity;
        
        private float gravityValue = Physics.gravity.magnitude;
        private bool playerIsGrounded;
        private float inputVel;

        public float InputVel => inputVel;

        public bool PlayerIsGrounded => playerIsGrounded;


        private void Awake()
        {
            _controller = GetComponent<CharacterController>();
            inputController = GetComponent<InputController>();
            
        }

        private void Start()
        {
          
         
            cam = Camera.main;
            
        }

        private void Update()
        {
            RotateToCam();
            if (PlayerDataSingleton.Instance.PlayerState != PLAYERSTATE.ATTACK)
            {
                Walk();
                Jump();
            }
    
        }

        void Walk()
        {
             inputVel = GetXZVelocity();

        }

        void Jump()
        {

            var velOnJump = GetXZVelocity();
          
             playerIsGrounded = IsGrounded();

            if (playerIsGrounded && playerVelocity.y < 0)
            {
                playerVelocity.y = 0f;
            }
           
            if (velOnJump != 0 && !playerIsGrounded)
                _controller.Move(Mathf.Abs(velOnJump) * transform.forward * 2 * Time.deltaTime);
            
            if (inputController.Jump && playerIsGrounded)
                playerVelocity.y += Mathf.Sqrt(jumpHeight* gravityValue);
          
            playerVelocity.y += -gravityValue * Time.deltaTime;

            
            _controller.Move(playerVelocity * Time.deltaTime);

        }

     

        float GetXZVelocity()
        {
            Vector3 movementVec = GetMovementVector();
            float velocity = Mathf.Clamp01(movementVec.magnitude);
            return inputController.Run <= 0 ? velocity : velocity*2;  
        }

        Vector3 GetMovementVector()=> new Vector3(inputController.Turn, 0, inputController.Walk);
        
        bool IsGrounded()
        {

            Collider[] colsArr = new Collider[1];
            var cols =Physics.OverlapSphereNonAlloc(groundChecker.transform.position, 0.1f,colsArr,mask);
            return cols>0 ;
        }
        
        void RotateToCam()
        {
      
            Vector3 neededForward = new Vector3(cam.transform.right.x, 0, cam.transform.right.z);
            Quaternion lerpVector = Quaternion.LookRotation(neededForward * inputController.Turn);
           if(inputController.Turn != 0)
               transform.rotation = Quaternion.Slerp(transform.rotation, lerpVector, 5 * Time.deltaTime);
            

            Vector3 neededForward2 = new Vector3(cam.transform.forward.x, 0, cam.transform.forward.z);
            Quaternion lerpVector2 = Quaternion.LookRotation(neededForward2 * inputController.Walk);
            if(inputController.Walk != 0)
                transform.rotation = Quaternion.Slerp(transform.rotation, lerpVector2, 5 * Time.deltaTime);
            
        }
    }
}
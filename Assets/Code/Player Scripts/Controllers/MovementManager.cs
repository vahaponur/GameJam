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
        private Animator _animator;
        private InputController inputController;
        private CharacterController _controller;
        
        private LayerMask mask;
        private Vector3 playerVelocity;
        
        private float gravityValue = Physics.gravity.magnitude;
        private bool groundedPlayer;

        private void Awake()
        {
            _controller = GetComponent<CharacterController>();
            _animator = GetComponent<Animator>();
            inputController = GetComponent<InputController>();
        }

        private void Start()
        {
          
            mask =~ LayerMask.GetMask("Player");
        }

        private void Update()
        {
            RotateToCam();
            Walk();
            Jump();
        }

        void Walk()
        {
            float inputVel = GetXZVelocity();
            _animator.SetFloat("Speed",inputVel,0.1f,Time.deltaTime);
            

        }

        void Jump()
        {
            var totalInput = GetMovementVector();
            var velOnJump = Mathf.Clamp01(totalInput.magnitude);
          
            _animator.applyRootMotion = groundedPlayer = isGrounded();
            Debug.Log(groundedPlayer);
            ArrangeJumpAnim();
            if (groundedPlayer && playerVelocity.y < 0)
            {
                playerVelocity.y = 0f;
            }
            _animator.SetFloat("InputMag",velOnJump);
            if (velOnJump != 0 && !groundedPlayer)
                _controller.Move(Mathf.Abs(velOnJump) * transform.forward * 2 * Time.deltaTime);
            if (inputController.Jump && groundedPlayer)
                playerVelocity.y += Mathf.Sqrt(jumpHeight* gravityValue);
           
            playerVelocity.y += -gravityValue*1.5f * Time.deltaTime;
            _controller.Move(playerVelocity * Time.deltaTime);

        }

        void ArrangeJumpAnim()
        {
        

            if (Input.GetAxis("KeyboardJump")>0 || Input.GetAxis("XboxJump")>0)
            {
                _animator.SetBool("isJumping",true);

            }
            else if (groundedPlayer)
            {
                _animator.SetBool("isJumping",false);
                
            }

            
            
        }

        float GetXZVelocity()
        {
            Vector3 movementVec = GetMovementVector();
            float velocity = Mathf.Clamp01(movementVec.magnitude);
            return inputController.Run <= 0 ? velocity : velocity*2;  
        }

        Vector3 GetMovementVector()=> new Vector3(inputController.Turn, 0, inputController.Walk);
        
        bool isGrounded()
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
using System;
using System.Collections;
using System.Collections.Generic;
using GameJam;
using UnityEngine;

public class AnimValueController : MonoBehaviour
{
   private Animator _animator;
   public Camera cam;
   public InputController inputController;
   private LayerMask mask;
   [SerializeField]private GameObject groundChecker;
   private CharacterController _controller;
   private Vector3 playerVelocity;
   private float gravityValue = Physics.gravity.magnitude;
   private bool groundedPlayer;
   private float jumpHeight;
   private bool onCol;
   private void Start()
   {
      _controller = GetComponent<CharacterController>();
      _animator = GetComponent<Animator>();
   }

   private void Update()
   {
      RotateToCam();
      Vector3 movementVector = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
      float maxSpeed=5;
      float magnitude = Mathf.Clamp01(movementVector.magnitude) ;
      if (Input.GetKey(KeyCode.LeftShift))
      {
         magnitude *= 2;
         
      }
      _animator.SetFloat("InputMag",magnitude);
      _animator.SetFloat("Speed",magnitude,0.1f,Time.deltaTime);

     if ( Input.GetAxis("Jump")>0)
     {
        _animator.SetBool("isJumping",true);
        
     }else if (isGrounded())
     {
        _animator.SetBool("isJumping",false);
     }
     var totalInput = new Vector3(inputController.HorizontalInput, 0, inputController.VerticalInput);
     
     var magn2 = Mathf.Clamp(totalInput.magnitude, -1, 1); ;
     groundedPlayer = isGrounded();

     _animator.applyRootMotion = groundedPlayer;
     
     if (groundedPlayer && playerVelocity.y < 0)
     {
        playerVelocity.y = 0f;
     }

     if (magn2 != 0)
     {
            
        //Appliying gravity on input may need to change

        if (!_animator.applyRootMotion)
        {
           _controller.Move(Mathf.Abs(magn2) * transform.forward * 2 * Time.deltaTime);

        }      
     }
        
     // Changes the height position of the player..
     if (Input.GetButtonDown("Jump") && groundedPlayer)
     {
         jumpHeight = Mathf.Clamp(magn2 * 3, 1, 3);
         jumpHeight = onCol ? jumpHeight * 5 : jumpHeight;
        playerVelocity.y += Mathf.Sqrt(jumpHeight* gravityValue);
     }

     playerVelocity.y += -gravityValue*1.5f * Time.deltaTime;
     _controller.Move(playerVelocity * Time.deltaTime);
   }
   void RotateToCam()
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
   bool isGrounded()
   {

      mask =~ LayerMask.GetMask("Player");
      var cols =Physics.OverlapSphere(groundChecker.transform.position, 0.2f,mask);
      return cols.Length>0 ;
   }

   private void OnCollisionEnter(Collision other)
   {
      if (other.gameObject.name.Contains("Cube"))
      {
         onCol = true; 
      }
      
   }

   private void OnCollisionExit(Collision other)
   {
       if (other.gameObject.name.Contains("Cube"))
      {
         onCol = false; 
      }
   }
}

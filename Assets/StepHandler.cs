using System;
using System.Collections;
using System.Collections.Generic;
using GameJam;
using UnityEngine;

public class StepHandler : MonoBehaviour
{
    #region Serialized Fields

  
    
    #endregion

    #region Private Fields

    private Vector3 targetPos;
    private Rigidbody rb;
    [SerializeField] GameObject origin;
    #endregion

    #region Public Properties
    #endregion

    #region MonoBehaveMethods
    void Awake()
    {
     
    }

    void Start()
    {
        
    }


    private void Update()
    {
        ClimbStair();
    }

    private void ClimbStair()
    {
        targetPos = transform.position;
        bool isGrounded= Physics.Raycast(origin.transform.position,  -transform.up, out RaycastHit hit);
        targetPos.y = hit.point.y;
        if (PlayerDataSingleton.Instance.PlayerState != PLAYERSTATE.IDLE)
        {
            transform.position = Vector3.Lerp(transform.position, targetPos,Time.deltaTime*10);
 
        }
    }



 
    #endregion
    
    #region PublicMethods
    #endregion
    
    #region PrivateMethods
    #endregion
}

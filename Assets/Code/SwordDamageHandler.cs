using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordDamageHandler : MonoBehaviour
{
    #region Serialized Fields
    #endregion

    #region Private Fields
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

   
    void Update()
    {
        
    }
    #endregion
    
    #region PublicMethods
    #endregion
    
    #region PrivateMethods
    #endregion

    private void OnTriggerEnter(Collider other)
    {
	    Debug.Log("sf");
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputController : MonoBehaviour
{
    #region Serialized Fields
    #endregion

    #region Private Fields

    private float horizontalInput, verticalInput, attackInput;

    public float HorizontalInput => horizontalInput;

    public float VerticalInput => verticalInput;

    public float AttackInput => attackInput;
    

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
        SetXY();
    }
    #endregion
    
    #region PublicMethods
    #endregion
    
    #region PrivateMethods

    void SetXY()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");
    }
    #endregion
}

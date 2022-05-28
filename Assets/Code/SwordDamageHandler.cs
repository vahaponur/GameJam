using System;
using System.Collections;
using System.Collections.Generic;
using GameJam;
using UnityEngine;
using Random = UnityEngine.Random;

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
	    if (other.CompareTag("Enemy") && PlayerDataSingleton.Instance.PlayerState == PLAYERSTATE.ATTACK)
	    {
		    var handler =other.GetComponent<EnemyHealthHandler>();
		    if (handler)
		    {
			    handler.GetDamage(GetRandomDamage());
		
		    }
	    }
    }

    float GetRandomDamage()
    {
	    return Random.Range(8, 20f);
	    
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameJam
{
    public class PlayerToCameraController : MonoBehaviour
    {
        #region Serialized Fields
        #endregion

        #region Private Fields

        private GameObject player;
        #endregion

        #region Public Properties
        #endregion

        #region MonoBehaveMethods
        void Awake()
        {
            player = GameObject.FindGameObjectWithTag("Player");
        }

        void Start()
        {
        
        }

   
        void Update()
        {
            
                transform.position = player.transform.position;
                transform.rotation = Quaternion.Lerp(transform.rotation, player.transform.rotation,3*Time.deltaTime);
            
        }
        #endregion
    
        #region PublicMethods
        #endregion
    
        #region PrivateMethods
        #endregion
    }
}


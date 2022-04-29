using System;
using UnityEngine;

namespace GameJam
{
    /// <summary>
    /// Main rigidbody controller, all scripts that mainly related rb should inherit.
    /// </summary>
    [RequireComponent(typeof(Rigidbody))]
    public abstract class BaseRBController : MonoBehaviour
    {
        protected Rigidbody rb;
     
        [SerializeField]
        protected float weightInNewton;

        protected virtual void Reset()
        {
            weightInNewton = 500;
        }

        private void Awake()
        {
      
            rb = GetComponent<Rigidbody>();
            rb.mass = weightInNewton * (1 / Physics.gravity.magnitude);
        }
    }
}
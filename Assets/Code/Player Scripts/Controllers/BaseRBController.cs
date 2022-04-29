using System;
using UnityEngine;

namespace GameJam
{
    [RequireComponent(typeof(Rigidbody))]
    public class BaseRBController : MonoBehaviour
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
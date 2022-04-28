using System;
using  UnityEngine;
namespace GameJam
{
    public sealed class PlayerDataSingleton:MonoBehaviour
    {
        public PLAYERSTATE PlayerState { get; set; }
        public static PlayerDataSingleton Instance { get; private set; }

        private void Awake()
        {
            if (Instance != null && Instance != this) 
            { 
                Destroy(this); 
            } 
            else 
            { 
                Instance = this; 
            } 
        }
    }
}
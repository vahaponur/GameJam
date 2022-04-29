using System;
using  UnityEngine;
namespace GameJam
{
    /// <summary>
    /// Includes data belonging the player (State, Curren Points etc.)
    /// </summary>
    public sealed class PlayerDataSingleton:MonoBehaviour
    {
        /// <summary>
        /// Current Player State
        /// </summary>
        public PLAYERSTATE PlayerState { get; set; }
        /// <summary>
        /// Singleton all along the game
        /// </summary>
        public static PlayerDataSingleton Instance { get; private set; }

        private void Awake()
        {
            ProduceSingleInstance();
        }

        void ProduceSingleInstance()
        {
            if (Instance != null && Instance != this) 
            { 
                Destroy(this); 
            } 
            else 
            { 
                Instance = this; 
            } 
            DontDestroyOnLoad(gameObject);  
        }
    }
}
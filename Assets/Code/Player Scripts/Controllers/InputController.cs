using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameJam
{
    /// <summary>
    /// Controls all the inputs from player, scripts that need input should have a reference to it.
    /// </summary>
    public class InputController : MonoBehaviour
    {
        private float dTime;
        #region Axes
        private float turn, walk,   dodge, mouseX, mouseY,run,jumpAxis;
        private bool jump,attack;
        private float attackSens;
        private bool onAttack;
        private int numberOfClick;

        public float Turn => turn;

        public float Walk => walk;

        public bool Attack => attack;

        public bool Jump => jump ;

        public float Dodge => dodge;

        public float MouseX => mouseX;

        public float MouseY => mouseY;

        public float Run => run;

        public float JumpAxis => jumpAxis;

        public float AttackSens => attackSens;

        public bool ONAttack => onAttack;

        #endregion
        
        private void Update()
        {
            SetInputAxes();

        }

        void SetInputAxes()
        {
            string head = DeviceManager.GamePadActive() ? "Xbox" : "Keyboard";
            turn = Input.GetAxis(head+"Turn");
            walk = Input.GetAxis(head+"Walk");
            attack = Input.GetButtonDown(head+"Attack");
            jump = Input.GetButtonDown(head + "Jump");
            dodge = Input.GetAxis(head + "Dodge");
            mouseX = Input.GetAxis(head + "MouseX");
            mouseY = Input.GetAxis(head + "MouseY");
            run = Input.GetAxis(head + "Run");
            jumpAxis = Input.GetAxis(head + "Jump");
            Debug.Log(head);

        }

        void SetAttack()
        {
            
        }
        
    }
}


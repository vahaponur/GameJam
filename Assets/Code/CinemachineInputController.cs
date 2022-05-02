using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Cinemachine;
using UnityEngine;

namespace GameJam
{
    public class CinemachineInputController : MonoBehaviour
    {
        private CinemachineFreeLook _cinemachineFreeLook;

        private void Awake()
        {
            _cinemachineFreeLook = GetComponent<CinemachineFreeLook>();
        }

        private void Update()
        {
            if (DeviceManager.GamePadActive())
            {
                _cinemachineFreeLook.m_YAxis.m_InputAxisName = "XboxMouseY";
                _cinemachineFreeLook.m_XAxis.m_InputAxisName = "XboxMouseX";
            }
            else 
            {
                _cinemachineFreeLook.m_YAxis.m_InputAxisName = "KeyboardMouseY";
                _cinemachineFreeLook.m_XAxis.m_InputAxisName = "KeyboardMouseX"; 
            }
            
    
        }

  
    }
}



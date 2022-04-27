using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using InputScripts;
using TMPro.EditorUtilities;
using UnityEngine.InputSystem.Interactions;

namespace Movement
{
    public class MoveCar : MonoBehaviour
    {
        private PlayerInputActions _pIA;
        private float forceMult = 1200000;
        private Rigidbody _rb;

        private bool Throttle { get; set; } = false;
    
        private void Awake()
        {
           _pIA = new PlayerInputActions();
           _pIA.Enable();
           _rb = GetComponent<Rigidbody>();
    
           _pIA.Player.Movement.performed += ThrottleUp;
           _pIA.Player.Movement.canceled += ThrottleDown;
        }
    
        private void ThrottleUp(InputAction.CallbackContext ctx)
        {
            Debug.Log("ThrottleUp");
            Throttle = true;
        }
        
        private void ThrottleDown(InputAction.CallbackContext ctx)
        {
            Debug.Log("ThrottleDown");
            Throttle = false;
        }

        public void FixedUpdate()
        {
            if (Throttle == true)
            {
                if (_rb.drag != 0) _rb.drag /= 100f;
                _rb.AddForce(transform.forward * (forceMult * Time.deltaTime));
            }

            if (Throttle == false)
            {
                _rb.drag = 3;
            }
        }
    }
}

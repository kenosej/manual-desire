using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using InputScripts;
using TMPro.EditorUtilities;
using UnityEngine.InputSystem.Interactions;
using UnityEngine.UIElements;

namespace Movement
{
    public class MoveCar : MonoBehaviour
    {
        private Rigidbody _rb;
        private PlayerInputActions _pIA;

        public GameObject _frontLeftWheel; 
        public GameObject _frontRightWheel; 
        public GameObject _rearLeftWheel; 
        public GameObject _rearRightWheel; 
        
        [SerializeField]
        public float forceMult = 30000;

        private bool Throttle { get; set; } = false;
        private bool Brake { get; set; } = false;
        private bool Left { get; set; } = false;
        private bool Right { get; set; } = false;
    
        private void Awake()
        {
           _pIA = new PlayerInputActions();
           _pIA.Enable();
           _rb = GetComponent<Rigidbody>();

           RegisterInputs();
        }

        private void RegisterInputs()
        {
           _pIA.Player.ThrottleAct.performed += ThrottleUp;
           _pIA.Player.ThrottleAct.canceled += ThrottleDown;
           
           _pIA.Player.BrakeAct.performed += BrakeUp;
           _pIA.Player.BrakeAct.canceled += BrakeDown;
           
           _pIA.Player.LeftAct.performed += TurnLeft;
           _pIA.Player.LeftAct.canceled += UndoTurnLeft;
           
           _pIA.Player.RightAct.performed += TurnRight;
           _pIA.Player.RightAct.canceled += UndoTurnRight;
        }

        private void TurnLeft(InputAction.CallbackContext obj)
        {
            Debug.Log("TurnLeft");
            Left = true;
        }
        
        private void UndoTurnLeft(InputAction.CallbackContext obj)
        {
            Debug.Log("UndoTurnLeft");
            Left = false;
        }

        private void TurnRight(InputAction.CallbackContext obj)
        {
            Debug.Log("TurnRight");
            Right = true;
        }

        private void UndoTurnRight(InputAction.CallbackContext obj)
        {
            Debug.Log("UndoTurnRight");
            Right = false;
        }
        
        private void BrakeUp(InputAction.CallbackContext obj)
        {
            Debug.Log("BrakeUp");
            Brake = true;
        }

        private void BrakeDown(InputAction.CallbackContext obj)
        {
            Debug.Log("BrakeDown");
            Brake = false;
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
            // _rb.AddForce(transform.forward * forceMult); // move rigid body
            // _frontLeftWheel.transform.Rotate(new Vector3(3.5f, 0f, 0f)); // rotate wheels
            // _rb.MoveRotation(_rb.rotation * Quaternion.Euler(0f, -1, 0f)); // turn car left
            
        }
    }
}

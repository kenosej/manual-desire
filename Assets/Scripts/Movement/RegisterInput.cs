using InputScripts;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Movement
{
    public class RegisterInput : MonoBehaviour
    {
        private ParentControl _pC;
        private PlayerInputActions _pIA;

        private void Awake()
        {
            _pC = GetComponent<ParentControl>();
            
            _pIA = new PlayerInputActions();
            _pIA.Enable();
            RegisterInputs();
        }

        private void RegisterInputs()
        {
            _pIA.Player.ThrottleAct.performed += ThrottleUp;
            _pIA.Player.ThrottleAct.canceled +=  ThrottleDown;

            _pIA.Player.BrakeAct.performed += BrakeUp;
            _pIA.Player.BrakeAct.canceled += BrakeDown;

            _pIA.Player.LeftAct.performed += TurnLeft;
            _pIA.Player.LeftAct.canceled += UndoTurnLeft;

            _pIA.Player.RightAct.performed += TurnRight;
            _pIA.Player.RightAct.canceled += UndoTurnRight;

            _pIA.Player.Gear1Act.performed += ShiftInto1;
            _pIA.Player.Gear1Act.canceled += UndoShiftInto1;
            
            _pIA.Player.Gear2Act.performed += ShiftInto2;
            _pIA.Player.Gear2Act.canceled += UndoShiftInto2;
            
            _pIA.Player.Gear3Act.performed += ShiftInto3;
            _pIA.Player.Gear3Act.canceled += UndoShiftInto3;
            
            _pIA.Player.Gear4Act.performed += ShiftInto4;
            _pIA.Player.Gear4Act.canceled += UndoShiftInto4;
            
            _pIA.Player.Gear5Act.performed += ShiftInto5;
            _pIA.Player.Gear5Act.canceled += UndoShiftInto5;
            
            _pIA.Player.Gear6Act.performed += ShiftInto6;
            _pIA.Player.Gear6Act.canceled += UndoShiftInto6;
            
            _pIA.Player.Gear7Act.performed += ShiftInto7;
            _pIA.Player.Gear7Act.canceled += UndoShiftInto7;
            
            _pIA.Player.Gear8Act.performed += ShiftInto8;
            _pIA.Player.Gear8Act.canceled += UndoShiftInto8;
            
            _pIA.Player.GearReverseAct.performed += ShiftIntoReverse;
            _pIA.Player.GearReverseAct.canceled += UndoShiftIntoReverse;
            
            _pIA.Player.GearNeutralAct.performed += ShiftIntoNeutral;
            _pIA.Player.GearNeutralAct.canceled += UndoShiftIntoNeutral;
            

            _pIA.Player.ClutchAct.started += ClutchUp;
            _pIA.Player.ClutchAct.performed += ClutchedFull;
            _pIA.Player.ClutchAct.canceled += ClutchDown;
        }

        private void ClutchUp(InputAction.CallbackContext obj)
        {
            _pC.Clutch = true;
        }

        private void ClutchedFull(InputAction.CallbackContext obj)
        {
            _pC.ShiftingReady = true;
        }
        
        private void ClutchDown(InputAction.CallbackContext obj)
        {
            _pC.Clutch = false;
            _pC.ShiftingReady = false;
        }

        private void ShiftInto1(InputAction.CallbackContext obj)
        {
            _pC.Gear1 = true;
        }

        private void UndoShiftInto1(InputAction.CallbackContext obj)
        {
            _pC.Gear1 = false;
        }
        
        private void ShiftInto2(InputAction.CallbackContext obj)
        {
            _pC.Gear2 = true;
        }

        private void UndoShiftInto2(InputAction.CallbackContext obj)
        {
            _pC.Gear2 = false;
        }
        
        private void ShiftInto3(InputAction.CallbackContext obj)
        {
            _pC.Gear3 = true;
        }
        
        private void UndoShiftInto3(InputAction.CallbackContext obj)
        {
            _pC.Gear3 = false;
        }

        private void ShiftInto4(InputAction.CallbackContext obj)
        {
            _pC.Gear4 = true;
        }

        private void UndoShiftInto4(InputAction.CallbackContext obj)
        {
            _pC.Gear4 = false;
        }
        
        private void ShiftInto5(InputAction.CallbackContext obj)
        {
            _pC.Gear5 = true;
        }
        
        private void UndoShiftInto5(InputAction.CallbackContext obj)
        {
            _pC.Gear5 = false;
        }
        
        private void ShiftInto6(InputAction.CallbackContext obj)
        {
            _pC.Gear6 = true;
        }
        
        private void UndoShiftInto6(InputAction.CallbackContext obj)
        {
            _pC.Gear6 = false;
        }
        
        private void ShiftInto7(InputAction.CallbackContext obj)
        {
            _pC.Gear7 = true;
        }
        
        private void UndoShiftInto7(InputAction.CallbackContext obj)
        {
            _pC.Gear7 = false;
        }
        
        private void ShiftInto8(InputAction.CallbackContext obj)
        {
            _pC.Gear8 = true;
        }
        
        private void UndoShiftInto8(InputAction.CallbackContext obj)
        {
            _pC.Gear8 = false;
        }
        
        private void ShiftIntoReverse(InputAction.CallbackContext obj)
        {
            _pC.GearReverse = true;
        }
        
        private void UndoShiftIntoReverse(InputAction.CallbackContext obj)
        {
            _pC.GearReverse = false;
        }
        
        private void ShiftIntoNeutral(InputAction.CallbackContext obj)
        {
            _pC.GearNeutral = true;
        }
        
        private void UndoShiftIntoNeutral(InputAction.CallbackContext obj)
        {
            _pC.GearNeutral = false;
        }

        private void TurnLeft(InputAction.CallbackContext obj)
        {
            _pC._left = true;
        }

        private void UndoTurnLeft(InputAction.CallbackContext obj)
        {
            _pC._left = false;
        }

        private void TurnRight(InputAction.CallbackContext obj)
        {
            _pC._right = true;
        }

        private void UndoTurnRight(InputAction.CallbackContext obj)
        {
            _pC._right = false;
        }

        private void BrakeUp(InputAction.CallbackContext obj)
        {
            _pC._brake = true;
        }

        private void BrakeDown(InputAction.CallbackContext obj)
        {
            _pC._brake = false;
        }

        private void ThrottleUp(InputAction.CallbackContext ctx)
        {
            _pC._throttle = true;
        }

        private void ThrottleDown(InputAction.CallbackContext ctx)
        {
            _pC._throttle = false;
        }
        
    }
}

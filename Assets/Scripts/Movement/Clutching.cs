using UnityEngine;

namespace Movement
{
    public class Clutching : MonoBehaviour
    {
        private ParentControl _pC;
        
        private void Awake()
        {
            _pC = GetComponent<ParentControl>();
        }

        private void FixedUpdate()
        {
            if (ShouldChangeGears()) ShiftIntoNewGear();
        }
        
        private bool ShouldChangeGears()
        {
            return _pC.ShiftingReady && (
                _pC.Gear1 ||
                _pC.Gear2 ||
                _pC.Gear3 ||
                _pC.Gear4 ||
                _pC.Gear5 ||
                _pC.Gear6 ||
                _pC.Gear7 ||
                _pC.Gear8 ||
                _pC.GearReverse ||
                _pC.GearNeutral
            );
        }
        
        // 3 up
        // 60%, 40%, 10%
        // 10%, 40%, 60%
        // 3 down
        // from 1 to 8

        private void SwitchingDown(ParentControl.Gears switchingToGear)
        {
            
        }

        private void ShiftIntoNewGear()
        {
            if (_pC.Gear1)
            {
                _pC.CurrentGear = ParentControl.Gears.FIRST;
            }
            else if (_pC.Gear2)
            {
                _pC.CurrentGear = ParentControl.Gears.SECOND;
            }
            else if (_pC.Gear3)
            {
                _pC.CurrentGear = ParentControl.Gears.THIRD;
            }
            else if (_pC.Gear4)
            {
                _pC.CurrentGear = ParentControl.Gears.FOURTH;
            }
            else if (_pC.Gear5)
            {
                _pC.CurrentGear = ParentControl.Gears.FIFTH;
            }
            else if (_pC.Gear6)
            {
                _pC.CurrentGear = ParentControl.Gears.SIXTH;
            }
            else if (_pC.Gear7)
            {
                _pC.CurrentGear = ParentControl.Gears.SEVENTH;
            }
            else if (_pC.Gear8)
            {
                _pC.CurrentGear = ParentControl.Gears.EIGHTH;
            }
            else if (_pC.GearReverse)
            {
                _pC.CurrentGear = ParentControl.Gears.REVERSE;
            }
            else
            {
                _pC.CurrentGear = ParentControl.Gears.NEUTRAL;
            }
            
        }
        
    }
}

using AutoInfo;
using System.Linq;
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
            if (!_pC.ShiftingReady) return false;

            return _pC.GearsReceiver.Any(g => g);
        }
        
        private void ShiftIntoNewGear()
        {
            for (int i = 0; i < _pC.GearsReceiver.Length; i++)
            {
                if (_pC.GearsReceiver[i])
                {
                    ParentControl.Gears nextGear = (ParentControl.Gears) i;
                    
                    SmoothShifting(nextGear);
                    _pC.CurrentGear = nextGear;
                }
            }
        }
        
        private void SmoothShifting(ParentControl.Gears switchingToGear)
        {
            if (switchingToGear == ParentControl.Gears.NEUTRAL ||
                switchingToGear == ParentControl.Gears.REVERSE ||
                _pC.CurrentGear == ParentControl.Gears.NEUTRAL ||
                _pC.CurrentGear == ParentControl.Gears.REVERSE)
                return;

            if ((int)switchingToGear > _pC.Car.NumberOfGears) // this shouldn't ever happen here
                return;
            
            Gear currGearMeta = _pC.Car.Gears.Find(g => g != null && g.Level == (int)_pC.CurrentGear);
            Gear nextGearMeta = _pC.Car.Gears.Find(g => g != null && g.Level == (int)switchingToGear);
            
            if (currGearMeta == null || nextGearMeta == null)
                return;
            
            float currInvRadianScalar = currGearMeta.Denominator / currGearMeta.Numerator;
            float nextInvRadianScalar = nextGearMeta.Denominator / nextGearMeta.Numerator;

            float currMaxDeltaScalar = Mathf.PI * currInvRadianScalar;
            float nextMaxDeltaScalar = Mathf.PI * nextInvRadianScalar;

            if ((int)switchingToGear > (int)_pC.CurrentGear)
            {
                ShiftingUp(switchingToGear, in currMaxDeltaScalar, in nextMaxDeltaScalar);
            }
            else if ((int)switchingToGear < (int)_pC.CurrentGear)
            {
                ShiftingDown(switchingToGear, in currMaxDeltaScalar, in nextMaxDeltaScalar);
            }
        }

        private void ShiftingUp(ParentControl.Gears switchingToGear, in float currMaxDeltaScalar, in float nextMaxDeltaScalar)
        {
            if ((int)switchingToGear == (int)_pC.CurrentGear + 1)
            {
                float nextMaxDeltaScalarOneUpTransferMod = nextMaxDeltaScalar * 0.6f;

                _pC.DeltaRadianScalar = nextMaxDeltaScalarOneUpTransferMod * _pC.DeltaRadianScalar / currMaxDeltaScalar;
            }
            else if ((int)switchingToGear == (int)_pC.CurrentGear + 2)
            {
                float nextMaxDeltaScalarOneUpTransferMod = nextMaxDeltaScalar * 0.4f;

                _pC.DeltaRadianScalar = nextMaxDeltaScalarOneUpTransferMod * _pC.DeltaRadianScalar / currMaxDeltaScalar;
            }
            else if ((int)switchingToGear == (int)_pC.CurrentGear + 3)
            {
                float nextMaxDeltaScalarOneUpTransferMod = nextMaxDeltaScalar * 0.1f;

                _pC.DeltaRadianScalar = nextMaxDeltaScalarOneUpTransferMod * _pC.DeltaRadianScalar / currMaxDeltaScalar;
            }
            else
            {
                _pC.DeltaRadianScalar = 0f;
            }
        }

        private void ShiftingDown(ParentControl.Gears switchingToGear, in float currMaxDeltaScalar, in float nextMaxDeltaScalar)
        {
            if ((int)switchingToGear == (int)_pC.CurrentGear - 1)
            {
                float maxCheckpointForNextGear = currMaxDeltaScalar * 0.6f;
                
                if (_pC.DeltaRadianScalar >= maxCheckpointForNextGear)
                {
                    _pC.DeltaRadianScalar = nextMaxDeltaScalar;
                }
                else
                {
                    _pC.DeltaRadianScalar = nextMaxDeltaScalar * _pC.DeltaRadianScalar / maxCheckpointForNextGear;
                }
            }
            else if ((int)switchingToGear == (int)_pC.CurrentGear - 2)
            {
                float maxCheckpointForNextGear = currMaxDeltaScalar * 0.4f;
                
                if (_pC.DeltaRadianScalar >= maxCheckpointForNextGear)
                {
                    _pC.DeltaRadianScalar = nextMaxDeltaScalar;
                }
                else
                {
                    _pC.DeltaRadianScalar = nextMaxDeltaScalar * _pC.DeltaRadianScalar / maxCheckpointForNextGear;
                }
            }
            else if ((int)switchingToGear == (int)_pC.CurrentGear - 3)
            {
                float maxCheckpointForNextGear = currMaxDeltaScalar * 0.1f;
                
                if (_pC.DeltaRadianScalar >= maxCheckpointForNextGear)
                {
                    _pC.DeltaRadianScalar = nextMaxDeltaScalar;
                }
                else
                {
                    _pC.DeltaRadianScalar = nextMaxDeltaScalar * _pC.DeltaRadianScalar / maxCheckpointForNextGear;
                }
            }
            else
            {
                _pC.DeltaRadianScalar = nextMaxDeltaScalar;
            }
        }

        
    }
}

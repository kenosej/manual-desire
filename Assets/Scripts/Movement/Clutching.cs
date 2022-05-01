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
                    ParentControl.GearsEnum nextGear = (ParentControl.GearsEnum) i;

                    if (!IsNextGearValid(nextGear))
                        return;
                    
                    if (ShouldSmoothShift(nextGear))
                        SmoothShifting(nextGear);
                    
                    _pC.CurrentGear = nextGear;
                    return;
                }
            }
        }

        private bool ShouldSmoothShift(ParentControl.GearsEnum nextGear)
        {
            return _pC.CurrentGear != ParentControl.GearsEnum.NEUTRAL &&
                   _pC.CurrentGear != ParentControl.GearsEnum.REVERSE &&
                   nextGear != ParentControl.GearsEnum.NEUTRAL &&
                   nextGear != ParentControl.GearsEnum.REVERSE;
        }

        private bool IsNextGearValid(ParentControl.GearsEnum nextGear)
        {
            if (nextGear == ParentControl.GearsEnum.REVERSE ||
                nextGear == ParentControl.GearsEnum.NEUTRAL)
                return true;
            
            if ((int)nextGear > _pC.Car.NumberOfGears)
                return false;

            return _pC.Car.Gears.Any(g => g?.Level == (int)nextGear);
        }

        private void SmoothShifting(ParentControl.GearsEnum switchingToGear)
        {
            Gear currGearMeta = _pC.Car.Gears.Find(g => g.Level == (int)_pC.CurrentGear);
            Gear nextGearMeta = _pC.Car.Gears.Find(g => g.Level == (int)switchingToGear);
            
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

        private void ShiftingUp(ParentControl.GearsEnum switchingToGear, in float currMaxDeltaScalar, in float nextMaxDeltaScalar)
        {
            if ((int)switchingToGear == (int)_pC.CurrentGear + 1)
            {
                float nextMaxDeltaScalarOneUpTransferMod = nextMaxDeltaScalar * 0.6f;

                _pC.Radian = nextMaxDeltaScalarOneUpTransferMod * _pC.Radian / currMaxDeltaScalar;
            }
            else if ((int)switchingToGear == (int)_pC.CurrentGear + 2)
            {
                float nextMaxDeltaScalarOneUpTransferMod = nextMaxDeltaScalar * 0.4f;

                _pC.Radian = nextMaxDeltaScalarOneUpTransferMod * _pC.Radian / currMaxDeltaScalar;
            }
            else if ((int)switchingToGear == (int)_pC.CurrentGear + 3)
            {
                float nextMaxDeltaScalarOneUpTransferMod = nextMaxDeltaScalar * 0.1f;

                _pC.Radian = nextMaxDeltaScalarOneUpTransferMod * _pC.Radian / currMaxDeltaScalar;
            }
            else
            {
                _pC.Radian = 0f;
            }
        }

        private void ShiftingDown(ParentControl.GearsEnum switchingToGear, in float currMaxDeltaScalar, in float nextMaxDeltaScalar)
        {
            if ((int)switchingToGear == (int)_pC.CurrentGear - 1)
            {
                float maxCheckpointForNextGear = currMaxDeltaScalar * 0.6f;
                
                if (_pC.Radian >= maxCheckpointForNextGear)
                {
                    _pC.Radian = nextMaxDeltaScalar;
                }
                else
                {
                    _pC.Radian = nextMaxDeltaScalar * _pC.Radian / maxCheckpointForNextGear;
                }
            }
            else if ((int)switchingToGear == (int)_pC.CurrentGear - 2)
            {
                float maxCheckpointForNextGear = currMaxDeltaScalar * 0.4f;
                
                if (_pC.Radian >= maxCheckpointForNextGear)
                {
                    _pC.Radian = nextMaxDeltaScalar;
                }
                else
                {
                    _pC.Radian = nextMaxDeltaScalar * _pC.Radian / maxCheckpointForNextGear;
                }
            }
            else if ((int)switchingToGear == (int)_pC.CurrentGear - 3)
            {
                float maxCheckpointForNextGear = currMaxDeltaScalar * 0.1f;
                
                if (_pC.Radian >= maxCheckpointForNextGear)
                {
                    _pC.Radian = nextMaxDeltaScalar;
                }
                else
                {
                    _pC.Radian = nextMaxDeltaScalar * _pC.Radian / maxCheckpointForNextGear;
                }
            }
            else
            {
                _pC.Radian = nextMaxDeltaScalar;
            }
        }

        
    }
}

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
            if (_pC.Clutch)
            {
                ClutchUp(_pC.FindCorrectRadianEndpointToGear());
            }
            if (ShouldChangeGears()) ShiftIntoNewGear();
        }

        private void ClutchUp(in float scaledRadianEndpoint)
        {
            float dropRate = scaledRadianEndpoint * 0.0003f;
            
            if (_pC.Radian > 0)
            {
                _pC.Radian -= dropRate;
            }
            
            // don't transfer torque to wheels
            for (var i = 0; i < _pC._wheelsColliders.Length; i++)
            {
                if (i < 2)
                {
                    _pC._wheelsColliders[i].motorTorque = 0f;
                }
            }
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
            
            if ((int)switchingToGear > (int)_pC.CurrentGear)
            {
                ShiftingUp(switchingToGear, currGearMeta.ScaledRadianEndpoint, nextGearMeta.ScaledRadianEndpoint);
            }
            else if ((int)switchingToGear < (int)_pC.CurrentGear)
            {
                ShiftingDown(switchingToGear, currGearMeta.ScaledRadianEndpoint, nextGearMeta.ScaledRadianEndpoint);
            }
        }

        private void ShiftingUp(ParentControl.GearsEnum nextGear, float currGearScaledRadianEndpoint, float nextGearScaledRadianEndpoint)
        {
            float exchangePoint;
            
            if ((int)nextGear == (int)_pC.CurrentGear + 1)
            {
                exchangePoint = 0.6f;
            }
            else if ((int)nextGear == (int)_pC.CurrentGear + 2)
            {
                exchangePoint = 0.4f;
            }
            else if ((int)nextGear == (int)_pC.CurrentGear + 3)
            {
                exchangePoint = 0.1f;
            }
            else
            {
                exchangePoint = 0f;
            }

            float nextGearMaximumRadianEndpoint = nextGearScaledRadianEndpoint * exchangePoint;
            _pC.Radian = nextGearMaximumRadianEndpoint * _pC.Radian / currGearScaledRadianEndpoint;
        }

        private void ShiftingDown(ParentControl.GearsEnum nextGear, float currGearScaledRadianEndpoint, float nextGearScaledRadianEndpoint)
        {
            float exchangePoint;
            
            if ((int)nextGear == (int)_pC.CurrentGear - 1)
            {
                exchangePoint = 0.6f;
            }
            else if ((int)nextGear == (int)_pC.CurrentGear - 2)
            {
                exchangePoint = 0.4f;
            }
            else if ((int)nextGear == (int)_pC.CurrentGear - 3)
            {
                exchangePoint = 0.1f;
            }
            else
            {
                _pC.Radian = nextGearScaledRadianEndpoint;
                return;
            }
            
            float currGearPointOfMaximumExchange = currGearScaledRadianEndpoint * exchangePoint;

            if (_pC.Radian >= currGearPointOfMaximumExchange)
            {
                _pC.Radian = nextGearScaledRadianEndpoint;
                return;
            }
            
            _pC.Radian = nextGearScaledRadianEndpoint * _pC.Radian / currGearPointOfMaximumExchange;
        }

        
    }
}

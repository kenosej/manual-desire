using System.Linq;
using UnityEngine;

namespace Cars.Movement
{
    [RequireComponent(typeof(Rigidbody), typeof(ParentControl))]
    public class Clutching : MonoBehaviour
    {
        private Rigidbody _rB;
        private ParentControl _pC;
        
        private void Awake()
        {
            _rB = GetComponent<Rigidbody>();
            _pC = GetComponent<ParentControl>();
        }

        private void FixedUpdate()
        {
            ClutchUp();
            TurnCarOffIfStartingBelowMinSpeed();
            
            if (ShouldChangeGears()) ShiftIntoNewGear();
        }

        private void TurnCarOffIfStartingBelowMinSpeed()
        {
            if (!_pC.IsTurnedOn || _pC.Clutch) return;
            
            if (_pC.CurrentGear == ParentControl.GearsEnum.Neutral ||
                _pC.CurrentGear == ParentControl.GearsEnum.First ||
                _pC.CurrentGear == ParentControl.GearsEnum.Reverse) 
                return;
            
            var gear = _pC.Car.Gears.Find(g => g.Level == (int)_pC.CurrentGear);
            
            if (_pC.SpeedInKmh > gear.MinSpeedKmh) return;
            
            JerkForward();
            _pC.IsTurnedOn = false;
        }
        
        private void JerkForward()
        { 
            _rB.AddRelativeForce(Vector3.forward * 100, ForceMode.Acceleration);
        }

        private void ClutchUp()
        {
            if (_pC.throttle || !_pC.Clutch) return;

            var scaledRadianEndpoint = _pC.FindCorrectRadianEndpointToGear();
            
            var dropRate = scaledRadianEndpoint * 0.003f;
            
            _pC.Radian -= dropRate;
            
            _pC.ApplyTorqueToWheels(0f);
        }

        private bool ShouldChangeGears()
        {
            return _pC.ShiftingReady && _pC.GearsReceiver.Any(g => g);
        }
        
        private void ShiftIntoNewGear()
        {
            for (int i = 0; i < _pC.GearsReceiver.Length; i++)
            {
                if (!_pC.GearsReceiver[i]) continue;
                
                var nextGear = (ParentControl.GearsEnum) i;

                if (!IsNextGearValid(nextGear))
                    return;
                    
                if (ShouldTransferRadianLoad(nextGear))
                    TransferRadianLoad(nextGear);
                    
                _pC.CurrentGear = nextGear;
                return;
            }
        }

        private bool ShouldTransferRadianLoad(ParentControl.GearsEnum nextGear)
        {
            return _pC.CurrentGear != ParentControl.GearsEnum.Neutral &&
                   _pC.CurrentGear != ParentControl.GearsEnum.Reverse &&
                   nextGear != ParentControl.GearsEnum.Neutral &&
                   nextGear != ParentControl.GearsEnum.Reverse;
        }

        private bool IsNextGearValid(ParentControl.GearsEnum nextGear)
        {
            if (nextGear == ParentControl.GearsEnum.Reverse ||
                nextGear == ParentControl.GearsEnum.Neutral)
                return true;
            
            return (int)nextGear <= _pC.Car.NumberOfGears && _pC.Car.Gears.Any(g => g?.Level == (int)nextGear);
        }

        private void TransferRadianLoad(ParentControl.GearsEnum switchingToGear)
        {
            var currGearMeta = _pC.Car.Gears.Find(g => g.Level == (int)_pC.CurrentGear);
            var nextGearMeta = _pC.Car.Gears.Find(g => g.Level == (int)switchingToGear);
            
            if ((int)switchingToGear > (int)_pC.CurrentGear)
                ShiftingUp(switchingToGear, currGearMeta.ScaledRadianEndpoint, nextGearMeta.ScaledRadianEndpoint);
            else if ((int)switchingToGear < (int)_pC.CurrentGear)
                ShiftingDown(switchingToGear, currGearMeta.ScaledRadianEndpoint, nextGearMeta.ScaledRadianEndpoint);
        }

        private void ShiftingUp(ParentControl.GearsEnum nextGear, float currGearScaledRadianEndpoint, float nextGearScaledRadianEndpoint)
        {
            float exchangePoint;
            
            if ((int)nextGear == (int)_pC.CurrentGear + 1)
                exchangePoint = 0.6f;
            else if ((int)nextGear == (int)_pC.CurrentGear + 2)
                exchangePoint = 0.4f;
            else if ((int)nextGear == (int)_pC.CurrentGear + 3)
                exchangePoint = 0.1f;
            else
                exchangePoint = 0f;

            var nextGearMaximumRadianEndpoint = nextGearScaledRadianEndpoint * exchangePoint;
            
            _pC.Radian = nextGearMaximumRadianEndpoint * _pC.Radian / currGearScaledRadianEndpoint;
        }

        private void ShiftingDown(ParentControl.GearsEnum nextGear, float currGearScaledRadianEndpoint, float nextGearScaledRadianEndpoint)
        {
            float exchangePoint;
            
            if ((int)nextGear == (int)_pC.CurrentGear - 1)
                exchangePoint = 0.6f;
            else if ((int)nextGear == (int)_pC.CurrentGear - 2)
                exchangePoint = 0.4f;
            else if ((int)nextGear == (int)_pC.CurrentGear - 3)
                exchangePoint = 0.1f;
            else
                exchangePoint = 0f;
            
            var currGearPointOfMaximumExchange = currGearScaledRadianEndpoint * exchangePoint;
            
            if (_pC.Radian >= currGearPointOfMaximumExchange)
            {
                _pC.Radian = nextGearScaledRadianEndpoint * 0.95f; // so it doesn't accidentally get incremented over the limit
                return;
            }
            
            _pC.Radian = nextGearScaledRadianEndpoint * _pC.Radian / currGearPointOfMaximumExchange;
        }
    }
}

using System.Linq;
using Cars.Models;
using UnityEngine;

namespace Cars.Movement
{
    public class Throttling : MonoBehaviour
    {
        private ParentControl _pC;

        [field: SerializeField] private float MotorTorque { get; set; }

        private void Awake()
        {
            _pC = GetComponent<ParentControl>();
        }

        private void FixedUpdate()
        {
            ApplyLeerGas();
            PressThrottle();
            ReleaseThrottle();
        }

        private void PressThrottle()
        {
            if (!_pC.IsTurnedOn || !_pC.throttle) return;
            
            if (_pC.Clutch)
            {
                IncreaseRadianInNeutral();
                return;
            }
            
            switch (_pC.CurrentGear)
            {
                case ParentControl.GearsEnum.Neutral:
                    IncreaseRadianInNeutral();
                    return;
                case ParentControl.GearsEnum.Reverse:
                {
                    var firstGear = _pC.Car.Gears.Find(g => g.Level == 1);
                    AdjustForReverse(firstGear);
                    break;
                }
                default:
                {
                    var gear = _pC.Car.Gears.Find(g => g.Level == (int)_pC.CurrentGear);
                    AdjustThrottleToGear(gear);
                    break;
                }
            }

            _pC.ApplyTorqueToWheels(MotorTorque);
        }

        private void IncreaseRadianInNeutral()
        {
            var smallestRadianEndpointOfAlLGears = _pC.Car.Gears.Min(g => g.ScaledRadianEndpoint);

            if (_pC.Radian >= smallestRadianEndpointOfAlLGears) return;

            const float step = Mathf.PI / 800;
            _pC.Radian += step;
        }

        private void AdjustForReverse(Gear firstGear)
        {
            if (_pC.Radian < firstGear.ScaledRadianEndpoint)
            {
                const float thousandthOfRadian = Mathf.PI / 1000;

                _pC.Radian += thousandthOfRadian;
            }

            MotorTorque = (firstGear.LowestTorque + firstGear.DeltaTorque * Mathf.Sin(_pC.Radian * firstGear.RadianScalar)) * -1;
        }

        private void AdjustThrottleToGear(Gear gear)
        {
            var scaledRadian = _pC.Radian * gear.RadianScalar;

            if (Mathf.Sin(scaledRadian) > 0.9999f)
            {
                //Debug.Log($"({gear.Level}. gear) {speedInKmh} km/h | PEAK");
            }
            
            if (_pC.Radian < gear.ScaledRadianEndpoint)
            {
                const float thousandthOfRadian = Mathf.PI / 1000;

                _pC.Radian += thousandthOfRadian;
            }

            MotorTorque = gear.LowestTorque + gear.DeltaTorque * Mathf.Sin(scaledRadian);

            if (_pC.SpeedInKmh <= gear.MaxSpeedKmh) return;
            
            // the faster car goes above its gear limit, the smaller torque is applied (partly) proportionally
            var speedSubtractionStep = Mathf.Ceil(Mathf.Abs(_pC.SpeedInKmh - gear.MaxSpeedKmh)) / 100;
            
            MotorTorque *= Mathf.Clamp(0.45f - speedSubtractionStep, 0.2f, 0.70f);
        }

        private void ReleaseThrottle()
        {
            if (_pC.throttle || _pC.Clutch) return;

            var scaledRadianEndpoint = _pC.FindCorrectRadianEndpointToGear();
            
            float dropRate;

            if (_pC.CurrentGear == ParentControl.GearsEnum.Neutral)
                dropRate = scaledRadianEndpoint * 0.003f;
            else
                dropRate = scaledRadianEndpoint * 0.0005f;
            
            _pC.Radian -= dropRate;
            
            MotorTorque = 0f;
            _pC.ApplyTorqueToWheels(MotorTorque);
        }

        private void ApplyLeerGas()
        {
            if (!_pC.IsTurnedOn ||
                _pC.Clutch ||
                _pC.SpeedInKmh > 2f ||
                (_pC.CurrentGear != ParentControl.GearsEnum.First &&
                _pC.CurrentGear != ParentControl.GearsEnum.Reverse))
                return;
            
            _pC.ApplyTorqueToWheels(_pC.CurrentGear == ParentControl.GearsEnum.First ? 10f : -10f);
        }
    }
}

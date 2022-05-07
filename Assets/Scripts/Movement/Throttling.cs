using Models;
using System.Linq;
using UnityEngine;

namespace Movement
{
    public class Throttling : MonoBehaviour
    {
        private Rigidbody _rB;
        private ParentControl _pC;

        [field: SerializeField] private float MotorTorque { get; set; }

        private void Awake()
        {
            _rB = GetComponent<Rigidbody>();
            _pC = GetComponent<ParentControl>();
        }

        private void FixedUpdate()
        {
            RegulateHeat();
            
            if (!_pC.IsTurnedOn || _pC.brake)
            {
                ReleaseThrottle(_pC.FindCorrectRadianEndpointToGear());
                return;
            }

            if (!_pC.throttle && _pC.Clutch) return;
            
            if (_pC.Clutch && _pC.throttle)
            {
                IncreaseRadianInNeutral();
                return;
            }
            
            if (_pC.throttle) PressThrottle();
            else DecideWhenNoThrottle();
        }

        private void RegulateHeat()
        {
            var multiplier = _pC.Heat > 50f ? 0.75f : 1f;
            var shouldCoolDownOnModerateRPMs = _pC.Heat > 50f;

            if (_pC.Radian < _pC.FindCorrectRadianEndpointToGear() * 0.65f)
                if (shouldCoolDownOnModerateRPMs)
                    _pC.Heat -= 0.02f * multiplier;
                else
                    _pC.Heat += 0.02f * multiplier;
            else if (_pC.Radian < _pC.FindCorrectRadianEndpointToGear() * 0.75f)
                _pC.Heat += 0.05f * multiplier;
            else
                _pC.Heat += 0.1f * multiplier;
        }

        private void PressThrottle()
        {
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

            if (_pC.Radian < smallestRadianEndpointOfAlLGears)
            {
                const float step = Mathf.PI / 800;

                _pC.Radian += step;
            }
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
            float scaledRadian = _pC.Radian * gear.RadianScalar;

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

        private void DecideWhenNoThrottle()
        {
            if (_pC.Radian < 0.001f)
            {
                switch (_pC.CurrentGear)
                {
                    case ParentControl.GearsEnum.First:
                        _pC.ApplyTorqueToWheels(10f);
                        break;
                    case ParentControl.GearsEnum.Reverse:
                        _pC.ApplyTorqueToWheels(-10f);
                        break;
                    default:
                    {
                        if (_pC.CurrentGear != ParentControl.GearsEnum.Neutral)
                        {
                            JerkForward();
                            _pC.IsTurnedOn = false;
                        }

                        break;
                    }
                }
            }
            else
            {
                ReleaseThrottle(_pC.FindCorrectRadianEndpointToGear());
            }
        }

        private void JerkForward()
        { 
            _rB.AddRelativeForce(Vector3.forward * 100, ForceMode.Acceleration);
        }

        private void ReleaseThrottle(in float scaledRadianEndpoint)
        {
            var dropRate = scaledRadianEndpoint * 0.0005f;
            
            _pC.Radian -= dropRate;
            
            MotorTorque = 0f;
            _pC.ApplyTorqueToWheels(MotorTorque);
        }
    }
}

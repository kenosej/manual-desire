using AutoInfo;
using System.Linq;
using UnityEngine;

namespace Movement
{
    public class Throttling : MonoBehaviour
    {
        private ParentControl _pC;
        private Rigidbody _rB;

        [field: SerializeField] private float MotorTorque { get; set; }

        private void Awake()
        {
            _pC = GetComponent<ParentControl>();
            _rB = GetComponent<Rigidbody>();
        }

        private void FixedUpdate()
        {
            if (_pC.Clutch) return;

            if (!_pC.IsTurnedOn)
            {
                ReleaseThrottle(_pC.FindCorrectRadianEndpointToGear());
                return;
            }
            
            if (_pC._throttle) PressThrottle();
            else DecideWhenNoThrottle();
        }

        private void PressThrottle()
        {
            if (_pC.CurrentGear == ParentControl.GearsEnum.NEUTRAL)
            {
                IncreaseRadianInNeutral();
                return;
            }
            if (_pC.CurrentGear == ParentControl.GearsEnum.REVERSE)
            {
                Gear firstGear = _pC.Car.Gears.Find(g => g.Level == 1);
                AdjustForReverse(firstGear);
            }
            else
            {
                float speed = _rB.velocity.magnitude;

                Gear gear = _pC.Car.Gears.Find(g => g.Level == (int)_pC.CurrentGear);
                AdjustThrottleToGear(speed, gear);
            }
            
            for (var i = 0; i < _pC._wheelsColliders.Length; i++)
            {
                if (i < 2)
                {
                    _pC._wheelsColliders[i].motorTorque = MotorTorque;
                }
            }
        }

        private void IncreaseRadianInNeutral()
        {
            float smallestRadianEndpointOfAlLGears = _pC.Car.Gears.Min(g => g.ScaledRadianEndpoint);

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

        private void AdjustThrottleToGear(in float speed, Gear gear)
        {
            //if (speed >= gear.MaxSpeed)
            //{
            //    MotorTorque = 0f;
            //    Debug.Log($"Max speed is reached! {speed} m/s; {speed * 3.6f} km/h");
            //}

            float scaledRadian = _pC.Radian * gear.RadianScalar;

            if (scaledRadian > gear.ScaledRadianPeak && Mathf.Sin(scaledRadian) < 0.0001f)
            {
                Debug.Log($"({gear.Level}. gear) {speed} m/s; {speed * 3.6f} km/h | Wave returned to 0");
            }
            
            if (Mathf.Sin(scaledRadian) > 0.9999f)
            {
                Debug.Log($"({gear.Level}. gear) {speed} m/s; {speed * 3.6f} km/h | PEAK");
            }
            
            if (_pC.Radian < gear.ScaledRadianEndpoint)
            {
                const float thousandthOfRadian = Mathf.PI / 1000;

                _pC.Radian += thousandthOfRadian;
            }

            MotorTorque = gear.LowestTorque + gear.DeltaTorque * Mathf.Sin(scaledRadian);
        }

        private void DecideWhenNoThrottle()
        {
            if (_pC.Radian < 0.001f)
            {
                if (_pC.CurrentGear == ParentControl.GearsEnum.FIRST)
                {
                    for (var i = 0; i < _pC._wheelsColliders.Length; i++)
                    {
                        if (i < 2)
                        {
                            //_pC._wheelsColliders[i].motorTorque = _pC.Car.LeerGasTorque;
                        }
                    }
                }
                // turn off the car at the other gears
            }
            else
            {
                ReleaseThrottle(_pC.FindCorrectRadianEndpointToGear());
            }
        }

        private void ReleaseThrottle(in float scaledRadianEndpoint)
        {
            float dropRate = scaledRadianEndpoint * 0.0005f;
            
            _pC.Radian -= dropRate;
                
            for (var i = 0; i < _pC._wheelsColliders.Length; i++)
            {
                MotorTorque = 0f;

                if (i < 2)
                {
                    _pC._wheelsColliders[i].motorTorque = MotorTorque;
                }
            }
        }
    }
}

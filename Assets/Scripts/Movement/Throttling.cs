using System;
using AutoInfo;
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
            if (_pC._throttle) PressThrottle();
            if (!_pC._throttle) ReleaseThrottle();
        }

        private void PressThrottle()
        {
            float speed = _rB.velocity.magnitude;

            Gear gear = _pC.Car.Gears.Find(g => g.Level == (int)_pC.CurrentGear);
            AdjustThrottleToGearInfo(speed, gear.LowestTorque, gear.HighestTorque, gear.Numerator, gear.Denominator, gear.MaxSpeed);
            
            for (var i = 0; i < _pC._wheelsColliders.Length; i++)
            {
                if (i < 2)
                {
                    _pC._wheelsColliders[i].motorTorque = MotorTorque;
                }
            }
        }

        private void AdjustThrottleToGearInfo(
            in float speed, in float lowestTorque,
            in float highestTorque, in float numerator,
            in float denominator, in float maxSpeed)
        {
            float deltaTorque = highestTorque - lowestTorque;
            float radianScalar = numerator / denominator;
            float invRadianScalar = denominator / numerator;

            float peakDeltaRadianScaledVal = Mathf.PI * invRadianScalar * 0.5f;
            bool isBeforePeak = _pC.Radian < peakDeltaRadianScaledVal;

            if (speed >= maxSpeed)
            {
                MotorTorque = 0f;
                //Debug.Log($"Max speed is reached!");
            }
            
            if (_pC.Radian < Mathf.PI * invRadianScalar)
            {
                const float thousandthOfRadian = Mathf.PI / 1000;

                _pC.Radian += thousandthOfRadian;
            }
            
            MotorTorque = lowestTorque + deltaTorque * (float)Math.Sin(_pC.Radian * radianScalar);
        }


        private void ReleaseThrottle()
        {
            for (var i = 0; i < _pC._wheelsColliders.Length; i++)
            {
                MotorTorque = 0f;

                if (_pC.Radian > 0) // smooth scaling down the 0-1 value to which the DELTA_TORQUE is scaled
                {
                    _pC.Radian -= Mathf.PI / 15000;
                }

                if (i < 2)
                {
                    _pC._wheelsColliders[i].motorTorque = MotorTorque;
                }
            }
        }
    }
}

using UnityEngine;

namespace Cars.Movement
{
    [RequireComponent(typeof(ParentControl))]
    public class Braking : MonoBehaviour
    {
        private ParentControl _pC;
        [field: SerializeField] float BrakeTorque { get; set; } = 7000f;
        [field: SerializeField] float ParkingBrakeTorque { get; set; } = 3000f;
        
        private void Awake()
        {
            _pC = GetComponent<ParentControl>();
        }
        
        private void FixedUpdate()
        {
            if (_pC.brake) Brake();
            else ReleaseBrake();

            if (_pC.parkingBrake) ParkingBrake();
            else ReleaseParkingBrake();
        }

        private void ParkingBrake()
        {
            for (var i = 0; i < _pC.wheelsColliders.Length; i++)
                if (i > 1)
                    _pC.wheelsColliders[i].brakeTorque = ParkingBrakeTorque;
        }

        private void ReleaseParkingBrake()
        {
            for (var i = 0; i < _pC.wheelsColliders.Length; i++)
                if (i > 1)
                    _pC.wheelsColliders[i].brakeTorque = 0f;
        }

        private void ReleaseBrake()
        {
            foreach (var wheelCollider in _pC.wheelsColliders)
                wheelCollider.brakeTorque = 0f;
        }

        private void Brake()
        {
            TurnCarOffIfBrakingAtLowSpeedWithoutClutch();
            
            foreach (var wheelCollider in _pC.wheelsColliders)
                wheelCollider.brakeTorque = BrakeTorque;
        }

        private void TurnCarOffIfBrakingAtLowSpeedWithoutClutch()
        {
            var correctMinSpeedToGear = _pC.FindCorrectMinSpeedToGear();
                
            if (!_pC.Clutch &&
                _pC.CurrentGear != ParentControl.GearsEnum.Neutral &&
                _pC.SpeedInKmh < (correctMinSpeedToGear == 0f ? 1f : correctMinSpeedToGear * 1.1f))
                _pC.IsTurnedOn = false;
        }
    }
}

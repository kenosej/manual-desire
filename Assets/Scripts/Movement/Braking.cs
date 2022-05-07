using UnityEngine;

namespace Movement
{
    public class Braking : MonoBehaviour
    {
        private ParentControl _pC;
        [field: SerializeField] float BrakeTorque { get; set; } = 7000f;
        
        private void Awake()
        {
            _pC = GetComponent<ParentControl>();
        }
        
        private void FixedUpdate()
        {
            if (_pC._brake) Brake();
            else ReleaseBrake();
        }

        private void ReleaseBrake()
        {
            foreach (var wheelCollider in _pC._wheelsColliders)
            {
                wheelCollider.brakeTorque = 0f;
            }
        }

        private void Brake()
        {
            TurnCarOffIfBrakingAtLowRPMsWithoutClutch();
            
            foreach (var wheelCollider in _pC._wheelsColliders)
            {
                wheelCollider.brakeTorque = BrakeTorque;
            }
        }

        private void TurnCarOffIfBrakingAtLowRPMsWithoutClutch()
        {
            if (!_pC.Clutch &&
                _pC.CurrentGear != ParentControl.GearsEnum.NEUTRAL &&
                _pC.Radian < _pC.FindCorrectRadianEndpointToGear() * 0.1f)
            {
                _pC.IsTurnedOn = false;
            }
        }
    }
}

using UnityEngine;

namespace Cars.Movement
{
    public class Steering : MonoBehaviour
    {
        private Rigidbody _rB;
        private ParentControl _pC;
        [SerializeField] private float maxAngle = 30f;
        
        [field: SerializeField] private float SteerStep { get; } = 1.1f;
        [field: SerializeField] private float ReleaseSteerStep { get; } = 0.9f;
    
        [SerializeField] private float currSteerAngle;

        private float CurrSteerAngle
        {
            get => currSteerAngle;
            set => currSteerAngle = Mathf.Abs(value) < SteerStep ? 0f : Mathf.Clamp(value, maxAngle * -1, maxAngle);
        }
       
        private void Awake()
        {
            _rB = GetComponent<Rigidbody>();
            _pC = GetComponent<ParentControl>();
        }
    
        private void FixedUpdate()
        {
            AdjustMaxSteeringAngleToCarSpeed();
            AdjustAngle();
            Steer();
        }

        private void AdjustMaxSteeringAngleToCarSpeed()
        {
            const float lowestSteerAngleAtSpeed = 90f;
            
            maxAngle = 30f - 25 * ((_pC.SpeedInKmh > lowestSteerAngleAtSpeed ? lowestSteerAngleAtSpeed : _pC.SpeedInKmh) / lowestSteerAngleAtSpeed);
        }

        private void AdjustAngle()
        {
            if (_pC.left) TurnLeft();
            else if (_pC.right) TurnRight();
            else ReturnSteeringWheelToCenter();
        }
    
        private void Steer()
        {
            for (var i = 0; i < _pC.wheelsColliders.Length; i++)
                if (i < 2)
                    _pC.wheelsColliders[i].steerAngle = CurrSteerAngle;
        }
    
        private void TurnLeft()
        {
            CurrSteerAngle -= SteerStep;
        }
        
        private void TurnRight()
        {
            CurrSteerAngle += SteerStep;
        }
        
        private void ReturnSteeringWheelToCenter()
        {
            if (CurrSteerAngle == 0f) return;
            
            var areWheelsOnLeft = CurrSteerAngle < 0f;
    
            if (areWheelsOnLeft)
                CurrSteerAngle += ReleaseSteerStep;
            else
                CurrSteerAngle -= ReleaseSteerStep;
        }
    }
}

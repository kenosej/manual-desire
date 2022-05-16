using UnityEngine;

namespace Cars.Movement
{
    [RequireComponent(typeof(ParentControl), typeof(Rigidbody))]
    public class Steering : MonoBehaviour
    {
        private ParentControl _pC;
        [SerializeField] private float maxAngle = 30f;
        [SerializeField] private float LowestSteerAngleAtSpeed = 110f;
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
            maxAngle = 30f - 25 * ((_pC.SpeedInKmh > LowestSteerAngleAtSpeed ? LowestSteerAngleAtSpeed : _pC.SpeedInKmh) / LowestSteerAngleAtSpeed);
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

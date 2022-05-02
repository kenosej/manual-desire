using UnityEngine;

namespace Movement
{
    public class Steering : MonoBehaviour
    {
        private Rigidbody _rB;
        private ParentControl _pC;
        [SerializeField] float _maxAngle = 30f;
        
        [field: SerializeField] private float SteerStep { get; set; } = 1.1f;
        [field: SerializeField] private float ReleaseSteerStep { get; set; } = 0.9f;
    
        [SerializeField] private float _currSteerAngle;
    
        public float CurrSteerAngle
        {
            get => _currSteerAngle;
            set
            {
                float absAngle = Mathf.Abs(value);
                
                if (absAngle < SteerStep)
                    _currSteerAngle = 0f;
                else if (absAngle < _maxAngle)
                    _currSteerAngle = value;
            }
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
            float lowestSteerAngleAtSpeed = 90f;
            float speedInKmh = _rB.velocity.magnitude * 3.6f;
            
            if (speedInKmh > lowestSteerAngleAtSpeed)
                speedInKmh = lowestSteerAngleAtSpeed;
            
            _maxAngle = 30f - 25 * (speedInKmh / lowestSteerAngleAtSpeed);
        }

        private void AdjustAngle()
        {
            if (_pC._left) TurnLeft();
            else if (_pC._right) TurnRight();
            else ReturnSteeringWheelToCenter();
        }
    
        private void Steer()
        {
            for (var i = 0; i < _pC._wheelsColliders.Length; i++)
            {
                if (i < 2)
                {
                    _pC._wheelsColliders[i].steerAngle = CurrSteerAngle;
                }
            }
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
            
            bool areWheelsOnLeft = CurrSteerAngle < 0f;
    
            if (areWheelsOnLeft)
                CurrSteerAngle += ReleaseSteerStep;
            else
                CurrSteerAngle -= ReleaseSteerStep;
        }
    }
}

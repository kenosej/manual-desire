using UnityEngine;

namespace OnScreen
{
    public class MovingSpeedNeedle : MonoBehaviour
    {
        [SerializeField] private Rigidbody _rB;
        
        public float SpeedInKmh
        {
            get => _rB.velocity.magnitude * 3.6f;
        }

        private Vector3 _currRotation;

        private const float START_POS = -12.359f;
        private const float END_POS = -254.883f;

        private const float MAX_KMH = 200f;

        private void Awake()
        {
            _currRotation = transform.eulerAngles;
        }

        private void UpdateNeedlePosition(in float rot)
        {
            _currRotation.z = rot;
            transform.rotation = Quaternion.Euler(_currRotation);
        }

        private void Update()
        {
            UpdateNeedlePosition(ScaleNeedlePositionToSpeed());
        }

        private float ScaleNeedlePositionToSpeed()
        {
            float numerator = (END_POS - START_POS) * SpeedInKmh;

            return numerator / MAX_KMH + START_POS;
        }
    }
}

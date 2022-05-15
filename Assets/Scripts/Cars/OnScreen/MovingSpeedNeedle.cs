using UnityEngine;

namespace Cars.OnScreen
{
    public class MovingSpeedNeedle : MonoBehaviour
    {
        private OnScreenParent _osp;
        
        private const float START_POS = -12.359f;
        private const float END_POS = -254.883f;

        private const float MAX_KMH = 200f;

        private void Start()
        {
            _osp = transform.parent.GetComponentInParent<OnScreenParent>();
        }

        private void Update()
        {
            _osp.UpdateNeedlePosition(ScaleNeedlePositionToSpeed(), gameObject);
        }

        private float ScaleNeedlePositionToSpeed()
        {
            var numerator = (END_POS - START_POS) * _osp.SpeedInKmh;

            return numerator / MAX_KMH + START_POS;
        }
    }
}

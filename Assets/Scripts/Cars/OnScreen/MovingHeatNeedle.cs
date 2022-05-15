using UnityEngine;
using Cars.Movement;

namespace Cars.OnScreen
{
    public class MovingHeatNeedle : MonoBehaviour
    {
        private ParentControl _pC;
        private OnScreenParent _osp;
        
        private const float START_POS =  -0.239f;
        private const float END_POS = -95.062f;

        private const float HEAT_MAX = 100f;
        
        private Vector3 _currRotation;

        private void Start()
        {
            _osp = transform.parent.GetComponentInParent<OnScreenParent>();
            _pC = _osp.carObjectReference.GetComponent<ParentControl>();
        }

        private void Update()
        {
            _osp.UpdateNeedlePosition(ScaleNeedlePositionToHeat(), gameObject);
        }

        private float ScaleNeedlePositionToHeat()
        {
            var numerator = (END_POS - START_POS) * _pC.Heat;
            return numerator / HEAT_MAX + START_POS;
        }
    }
}

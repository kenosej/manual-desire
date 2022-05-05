using Movement;
using UnityEngine;

namespace OnScreen
{
    public class MovingHeatNeedle : MonoBehaviour
    {
        public GameObject _carObjectReference;
        private ParentControl _pC;
        
        private const float START_POS =  -0.239f;
        private const float END_POS = -95.062f;

        private const float HEAT_MAX = 100f;
        
        private Vector3 _currRotation;

        private void Awake()
        {
            _pC = _carObjectReference.GetComponent<ParentControl>();
            
            _currRotation = transform.eulerAngles;
        }
        
        private void UpdateNeedlePosition(in float rot) // 3rd duplicate
        {
            _currRotation.z = rot;
            transform.rotation = Quaternion.Euler(_currRotation);
        }

        private void Update()
        {
            UpdateNeedlePosition(ScaleNeedlePositionToHeat());
        }

        private float ScaleNeedlePositionToHeat()
        {
            float numerator = (END_POS - START_POS) * _pC.Heat;
            return numerator / HEAT_MAX + START_POS;
        }
    }
}

using AutoInfo;
using Movement;
using UnityEngine;

namespace OnScreen
{
    public class MovingRPMNeedle : MonoBehaviour
    {
        public GameObject _carObjectReference;
        private ParentControl _pC;
        
        private const float START_POS = -13.617f;
        private const float END_POS = -254.978f;
        
        private float _maxScaledRadian;
        private Vector3 _currRotation;

        private void Awake()
        {
            _pC = _carObjectReference.GetComponent<ParentControl>();

            _currRotation = transform.eulerAngles;
        }
        
        private void UpdateNeedlePosition(float rot)
        {
            _currRotation.z = rot;
            transform.rotation = Quaternion.Euler(_currRotation);
        }

        private void Update()
        {
            if (_pC.CurrentGear != ParentControl.GearsEnum.NEUTRAL && _pC.CurrentGear != ParentControl.GearsEnum.REVERSE)
                UpdateNeedlePosition(ScaleNeedlePositionToScaledRadian());
        }

        private float ScaleNeedlePositionToScaledRadian()
        {
            Gear gear = _pC.Car.Gears.Find(g => g.Level == (int)_pC.CurrentGear);

            float numerator = (END_POS - START_POS) * _pC.Radian;

            return numerator / gear.ScaledRadianEndpoint + START_POS;
        }

    }
}

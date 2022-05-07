using Movement;
using UnityEngine;

namespace OnScreen
{
    public class MovingRPMNeedle : MonoBehaviour
    {
        private ParentControl _pC;
        public GameObject carObjectReference;
        
        private const float START_POS = -13.617f;
        private const float LEER_GAS_POS = -47.677f;
        private const float END_POS = -254.978f;
        
        private Vector3 _currRotation;

        private void Awake()
        {
            _pC = carObjectReference.GetComponent<ParentControl>();

            _currRotation = transform.eulerAngles;
        }
        
        private void UpdateNeedlePosition(in float rotation)
        {
            _currRotation.z = rotation;
            transform.rotation = Quaternion.Euler(_currRotation);
        }

        private void Update()
        {
            UpdateNeedlePosition(ScaleNeedlePositionToScaledRadian(_pC.FindCorrectRadianEndpointToGear()));
        }

        private float ScaleNeedlePositionToScaledRadian(in float scaledRadianEndpoint)
        {
            var startPosition = _pC.IsTurnedOn ? LEER_GAS_POS : START_POS;
            
            var numerator = (END_POS - startPosition) * (_pC.shouldSmoothAlignRadian ? _pC.SmoothAligningRadian : _pC.Radian);

            return numerator / scaledRadianEndpoint + startPosition;
        }
    }
}

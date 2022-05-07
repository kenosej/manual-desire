using Movement;
using UnityEngine;

namespace OnScreen
{
    public class MovingRPMNeedle : MonoBehaviour
    {
        private ParentControl _pC;
        private OnScreenParent _osp;
        
        private const float START_POS = -13.617f;
        private const float LEER_GAS_POS = -47.677f;
        private const float END_POS = -254.978f;

        private void Awake()
        {
            _osp = transform.parent.GetComponentInParent<OnScreenParent>();
            _pC = _osp.carObjectReference.GetComponent<ParentControl>();
        }

        private void Update()
        {
            _osp.UpdateNeedlePosition(ScaleNeedlePositionToScaledRadian(_pC.FindCorrectRadianEndpointToGear()), gameObject);
        }

        private float ScaleNeedlePositionToScaledRadian(in float scaledRadianEndpoint)
        {
            var startPosition = _pC.IsTurnedOn ? LEER_GAS_POS : START_POS;
            
            var numerator = (END_POS - startPosition) * (_pC.shouldSmoothAlignRadian ? _pC.SmoothAligningRadian : _pC.Radian);

            return numerator / scaledRadianEndpoint + startPosition;
        }
    }
}

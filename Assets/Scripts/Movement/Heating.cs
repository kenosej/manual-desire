using UnityEngine;

namespace Movement
{
    public class Heating : MonoBehaviour
    {
        private ParentControl _pC;

        private void Awake()
        {
            _pC = GetComponent<ParentControl>();
        }

        private void FixedUpdate()
        {
            RegulateHeat();
        }

        private void RegulateHeat()
        {
            var multiplier = _pC.Heat > 50f ? 0.75f : 1f;
            var shouldCoolDownOnModerateRPMs = _pC.Heat > 50f;
    
            if (_pC.Radian < _pC.FindCorrectRadianEndpointToGear() * 0.65f)
                if (shouldCoolDownOnModerateRPMs)
                    _pC.Heat -= 0.02f * multiplier;
                else
                    _pC.Heat += 0.02f * multiplier;
            else if (_pC.Radian < _pC.FindCorrectRadianEndpointToGear() * 0.75f)
                _pC.Heat += 0.05f * multiplier;
            else
                _pC.Heat += 0.1f * multiplier;
        }
    }
}

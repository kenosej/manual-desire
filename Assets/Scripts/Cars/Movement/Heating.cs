using UnityEngine;

namespace Cars.Movement
{
    [RequireComponent(typeof(ParentControl))]
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

        private void RegulateHeat() // TODO: manage when car is turned off
        {
            var multiplier = _pC.Heat > 50f ? 0.75f : 1f;
            var shouldCoolDownOnModerateRPMs = _pC.Heat > 50f;

            if (_pC.RPMNeedle01Position < 0.65f)
                if (shouldCoolDownOnModerateRPMs)
                    _pC.Heat -= 0.02f * multiplier;
                else
                    _pC.Heat += 0.02f * multiplier;
            else if (_pC.RPMNeedle01Position < 0.75f)
                _pC.Heat += 0.05f * multiplier;
            else
                _pC.Heat += 0.1f * multiplier;
        }
    }
}

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
            // philosophy (if the car is turned on):
            // if RPMs going up -> the higher the temp., the slower the heating
            // if RPMs going down -> the higher the temp., the faster the cool down
            
            var heatStep = _pC.RPMNeedle01Position < 0.65f ? 0.02f : Mathf.Lerp(0.02f, 0.1f, _pC.RPMNeedle01Position);
            
            if (!_pC.IsTurnedOn || (_pC.Heat > 50f && _pC.RPMNeedle01Position < 0.65f))
                heatStep *= -1f;
            
            // if RPMs are greater than 65% -> the greater the heat, the smaller the multiplier
            // if RPMs are smaller than 65% -> the greater the heat, the bigger the multiplier
            var multiplier = !_pC.IsTurnedOn ? 3f : Mathf.Lerp(
                _pC.RPMNeedle01Position < 0.65f ? 0.5f : 2f,
                _pC.RPMNeedle01Position < 0.65f ? 2f : 0.5f,
                Mathf.InverseLerp(0f, 100f, _pC.Heat)
                );

            _pC.Heat += heatStep * multiplier;
        }
    }
}

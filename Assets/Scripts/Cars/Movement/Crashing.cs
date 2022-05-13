using UnityEngine;

namespace Cars.Movement 
{
    public class Crashing : MonoBehaviour
    {
        private ParentControl _pC;
        
        private void Awake()
        {
            _pC = GetComponent<ParentControl>();
        }
        
        private void OnCollisionEnter(Collision collision)
        {
            _pC.HealthBarFillAmount -= Mathf.InverseLerp(0f, 100f, _pC.SpeedInKmh);
        }
    }
}

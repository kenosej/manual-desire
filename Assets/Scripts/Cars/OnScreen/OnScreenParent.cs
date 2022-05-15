using System;
using UnityEngine;
using Cars.Movement;

namespace Cars.OnScreen
{
    public class OnScreenParent : MonoBehaviour
    {
        private Rigidbody _rB;
        private ParentControl _pC;
        private CanvasGroup _cG;
        public GameObject carObjectReference;
        
        public float SpeedInKmh => _rB.velocity.magnitude * 3.6f;

        private void Awake()
        {
			carObjectReference = transform.Find("/Car").GetChild(0).gameObject;
            _rB = carObjectReference.GetComponent<Rigidbody>();
            _pC = carObjectReference.GetComponent<ParentControl>();
            _cG = GetComponent<CanvasGroup>();
        }
        
        public void UpdateNeedlePosition(in float zRotation, GameObject go)
        {
            go.transform.rotation = Quaternion.Euler(new Vector3(0, 0, zRotation));
        }

        private void Update()
        {
            // hide canvas
            _cG.alpha = _pC.IsPaused ? 0f : 1f;
        }
    }
}

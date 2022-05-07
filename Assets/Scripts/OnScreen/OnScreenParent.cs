using Movement;
using UnityEngine;

namespace OnScreen
{
    public class OnScreenParent : MonoBehaviour
    {
        private Rigidbody _rB;
        private ParentControl _pC;
        public GameObject carObjectReference;
        
        public float SpeedInKmh => _rB.velocity.magnitude * 3.6f;

        public virtual void Awake()
        {
            _rB = carObjectReference.GetComponent<Rigidbody>();
            _pC = carObjectReference.GetComponent<ParentControl>();
        }
        
        public void UpdateNeedlePosition(in float zRotation, GameObject go)
        {
            go.transform.rotation = Quaternion.Euler(new Vector3(0, 0, zRotation));
        }
    }
}

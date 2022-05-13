using TMPro;
using UnityEngine;
using Cars.Movement;

namespace Cars.OnScreen
{
    [RequireComponent(typeof(TextMeshProUGUI))]
    public class TurnedOff : MonoBehaviour
    {
        private ParentControl _pC;
        private OnScreenParent _osp;
        private TextMeshProUGUI _tmp;
    
        private void Awake()
        {
            _osp = transform.parent.GetComponentInParent<OnScreenParent>();
            _pC = _osp.carObjectReference.GetComponent<ParentControl>();
            _tmp = GetComponent<TextMeshProUGUI>();
        }

        private void Update()
        {
            _tmp.enabled = !_pC.IsTurnedOn;
        }
    }
}

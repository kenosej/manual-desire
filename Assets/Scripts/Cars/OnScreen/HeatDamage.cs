using TMPro;
using UnityEngine;
using Cars.Movement;

namespace Cars.OnScreen
{
    public class HeatDamage : MonoBehaviour
    {
        private ParentControl _pC;
        private OnScreenParent _osp;
        private TextMeshProUGUI _tmp;
        
        private void Awake()
        {
            _osp = GetComponentInParent<OnScreenParent>();
            _pC = _osp.carObjectReference.GetComponent<ParentControl>();
            _tmp = GetComponent<TextMeshProUGUI>();
        }

        private void Update()
        {
            if (_pC.Heat > 95f)
                BlinkCautionText();
            else
                _tmp.enabled = false;
        }

        private void BlinkCautionText()
        {
            _tmp.enabled = Mathf.CeilToInt(Time.time) % 2 == 0;
        }
    }
}

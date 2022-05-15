using UnityEngine;
using Cars.Movement;
using UnityEngine.UI;

namespace Cars.OnScreen
{
    [RequireComponent(typeof(Image))]
    public class Health : MonoBehaviour
    {
        private Image _bar;
        private ParentControl _pC;
        private OnScreenParent _osp;
        
        private void Start()
        {
            _osp = transform.parent.GetComponentInParent<OnScreenParent>();
            _bar = GetComponent<Image>();
            _pC = _osp.carObjectReference.GetComponent<ParentControl>();
        }

        private void Update()
        {
            HeatDamage();
            UpdateBar();
        }

        private void HeatDamage()
        {
            if (_pC.Heat > 95)
                _pC.HealthBarFillAmount -= 0.05f * Time.deltaTime;
        }

        private void UpdateBar()
        {
            _bar.fillAmount = _pC.HealthBarFillAmount;
        }
    }
}

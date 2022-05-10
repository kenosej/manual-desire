using UnityEngine;
using Cars.Movement;
using UnityEngine.UI;

namespace Cars.OnScreen
{
    public class Health : MonoBehaviour
    {
        private Image _bar;
        private ParentControl _pC;
        private OnScreenParent _osp;
        
        private float _barFillAmount = 1f;

        private float BarFillAmount
        {
            get => _barFillAmount;
            set
            {
                switch (value)
                {
                    case <= 0f:
                        _pC.IsCarDead = true;
                        return;
                    case > 1f:
                        return;
                    default:
                        _barFillAmount = value;
                        return;
                }
            }
        }
        
        private void Awake()
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
                BarFillAmount -= 0.05f * Time.deltaTime;
        }

        private void UpdateBar()
        {
            _bar.fillAmount = BarFillAmount;
        }
    }
}

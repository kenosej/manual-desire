using Movement;
using UnityEngine;
using UnityEngine.UI;

namespace OnScreen
{
    public class Health : MonoBehaviour
    {
        private Image _bar;
        private ParentControl _pC;
        public GameObject carObjectReference;
        
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
                        break;
                }
            }
        }
        
        private void Awake()
        {
            _bar = GetComponent<Image>();
            _pC = carObjectReference.GetComponent<ParentControl>();
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

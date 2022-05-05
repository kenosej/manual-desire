using Movement;
using UnityEngine;
using UnityEngine.UI;

namespace OnScreen
{
    public class Health : MonoBehaviour
    {
        public GameObject _carObjectReference;
        private ParentControl _pC;
        private Image _bar;
        
        private float _barFillAmount = 1f;

        public float BarFillAmount
        {
            get => _barFillAmount;
            set
            {
                if (value <= 0f)
                {
                    _pC.IsCarDead = true;
                    return;
                }
                if (value > 1f) return;
                _barFillAmount = value;
            }
        }
        
        private void Awake()
        {
            _pC = _carObjectReference.GetComponent<ParentControl>();
            _bar = GetComponent<Image>();
        }

        private void Update()
        {
            HeatDamage();
            UpdateBar();
        }

        private void HeatDamage()
        {
            if (_pC.Heat > 95)
            {
                BarFillAmount -= 0.05f * Time.deltaTime;
            }
        }

        private void UpdateBar()
        {
            _bar.fillAmount = BarFillAmount;
        }
    }
}

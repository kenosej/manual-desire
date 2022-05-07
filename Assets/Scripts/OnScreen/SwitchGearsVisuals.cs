using Movement;
using UnityEngine;
using UnityEngine.UI;

namespace OnScreen
{
    public class SwitchGearsVisuals : MonoBehaviour
    {
        private ParentControl _pC;
        public GameObject carObjReference;
        private readonly Image[] _gears = new Image[10];

        private void Awake()
        {
            _pC = carObjReference.GetComponent<ParentControl>();

            LinkVisuals();
        }
        
        private void LinkVisuals()
        {
            _gears[0] = transform.Find("GearNeutral").gameObject.GetComponent<Image>();

            for (int i = 1; i < 9; i++)
            {
                _gears[i] = transform.Find($"Gear{i}").gameObject.GetComponent<Image>();
            }

            _gears[9] = transform.Find("GearReverse").gameObject.GetComponent<Image>();
        }

        private void Update()
        {
            ColorCurrentGear();
        }

        private void ColorCurrentGear()
        {
            foreach (var gear in _gears)
                gear.color = Color.white;
            
            _gears[(int)_pC.CurrentGear].color = Color.black;
        }
    }
}

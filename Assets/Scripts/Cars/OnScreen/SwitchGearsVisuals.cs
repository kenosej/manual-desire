using UnityEngine;
using Cars.Movement;
using UnityEngine.UI;

namespace Cars.OnScreen
{
    public class SwitchGearsVisuals : MonoBehaviour
    {
        private OnScreenParent _osp;
        private ParentControl _pC;
        private readonly Image[] _gears = new Image[10];

        private Sprite[] _unselectedGears = new Sprite[10];
        private Sprite[] _selectedGears = new Sprite[10];
        private Sprite[] _shiftingReadyGears = new Sprite[10];

        private void Awake()
        {
            _osp = GetComponentInParent<OnScreenParent>();
            _pC = _osp.carObjectReference.GetComponent<ParentControl>();

            LinkVisuals();
            DeactivateUnusedGears();
            LoadSprites();
        }

        private void LoadSprites()
        {
            _selectedGears[0] = Resources.Load<Sprite>($"SelectedGears/sn");
            _unselectedGears[0] = Resources.Load<Sprite>($"UnselectedGears/un");
            _shiftingReadyGears[0] = Resources.Load<Sprite>($"ShiftingReadyGears/rn");
            _selectedGears[9] = Resources.Load<Sprite>($"SelectedGears/sr");
            _unselectedGears[9] = Resources.Load<Sprite>($"UnselectedGears/ur");
            _shiftingReadyGears[9] = Resources.Load<Sprite>($"ShiftingReadyGears/rr");
            
            for (int i = 1; i <= _pC.Car.NumberOfGears; i++)
            {
                _selectedGears[i] = Resources.Load<Sprite>($"SelectedGears/s{i}");
                _unselectedGears[i] = Resources.Load<Sprite>($"UnselectedGears/u{i}");
                _shiftingReadyGears[i] = Resources.Load<Sprite>($"ShiftingReadyGears/r{i}");
            }
        }
        
        private void DeactivateUnusedGears()
        {
            for (int i = _pC.Car.NumberOfGears + 1; i < 9; i++)
            {
                _gears[i].enabled = false;
            }
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
            SwitchToCurrentGear();
        }

        private void SwitchToCurrentGear()
        {
            for (int i = 0; i <= _pC.Car.NumberOfGears; i++)
                _gears[i].sprite = _unselectedGears[i];
            
            // reverse
            _gears[9].sprite = _unselectedGears[9];

            _gears[(int)_pC.CurrentGear].sprite = _pC.ShiftingReady ? _shiftingReadyGears[(int)_pC.CurrentGear] : _selectedGears[(int)_pC.CurrentGear];
        }
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using Movement;
using UnityEngine;
using UnityEngine.UI;

namespace OnScreen
{
    public class SwitchGearsVisuals : MonoBehaviour
    {
        public GameObject _carObjReference;
        private ParentControl _pC;
        private Image[] Gears = new Image[10];

        private void Awake()
        {
            _pC = _carObjReference.GetComponent<ParentControl>();

            LinkVisuals();
        }
        
        private void LinkVisuals()
        {
            Gears[0] = transform.Find("GearNeutral").gameObject.GetComponent<Image>();

            for (int i = 1; i < 9; i++)
            {
                Gears[i] = transform.Find($"Gear{i}").gameObject.GetComponent<Image>();
            }

            Gears[9] = transform.Find("GearReverse").gameObject.GetComponent<Image>();
        }

        private void Update()
        {
            ColorCurrentGear();
        }

        private void ColorCurrentGear()
        {
            foreach (Image gear in Gears)
            {
                gear.color = Color.white;
            }
            
            Gears[(int)_pC.CurrentGear].color = Color.black;
        }
    }
}

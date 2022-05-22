using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace TimeManagement
{
    public class FinalTime : MonoBehaviour
    {
        private void Update()
        {
            this.GetComponent<TextMeshProUGUI>().text = TimeCounterController.FinalTime.text;
        }
    }
}

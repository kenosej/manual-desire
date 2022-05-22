using System;
using System.Collections;
using System.Collections.Generic;
using Cars.Models;
using UnityEngine;
using Cars;
using Unity.VisualScripting;

namespace TimeManagement
{
    public class StopTimeCounter : MonoBehaviour
    {

        public static bool IsTriggered;
        public static string WhoTriggered;
        public static bool GameOver;

        private void Awake()
        {
            IsTriggered = false;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.transform.CompareTag("Opponent"))
            {
                TimeCounterController.EndTimer();
                WhoTriggered = other.transform.tag;
                IsTriggered = true;
                GameOver = true;
                return;
            }
            
            var carTag = transform.Find("/Car").GetChild(0).tag;
            var enterObjectTag = other.transform.parent.tag;

            if (enterObjectTag == carTag)
            {
                TimeCounterController.EndTimer();
                WhoTriggered = carTag;
                IsTriggered = true;
                return;
            }
        }
    }
}
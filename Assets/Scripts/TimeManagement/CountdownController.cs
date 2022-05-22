using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

namespace TimeManagement
{
    public class CountdownController : MonoBehaviour
    {
        public int CountdownTime;
        public Text CountdownDisplay;

        private void Start()
        {
            StartCoroutine(CountdownToStart());
        }

        IEnumerator CountdownToStart()
        {
            while (CountdownTime > 0)
            {
                CountdownDisplay.text = CountdownTime.ToString();
                yield return new WaitForSeconds(1f);
                CountdownTime--;
            }

            CountdownDisplay.text = "GO!";
            TimeCounterController.StartGame();
            yield return new WaitForSeconds(1f);
            CountdownDisplay.gameObject.SetActive(false);
        }
    }
} 
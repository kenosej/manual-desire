using System;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace TimeManagement
{
    public class TimeCounterController : MonoBehaviour
    {
        private float ElapsedTime;
        private TimeSpan TimePlaying;
        public Text TimeCounter;
        public static Text FinalTime;
        public static bool TimerGoing;
        public static bool GameIsStarted;
        public static TimeCounterController Instance;
        
        private void Awake()
        {
            Instance = this;
        }
        
        private void Start()
        {
            TimeCounter.text = "Time: 00:00.00";
            TimerGoing = false;
        }

        private void Update()
        {

            if (GameIsStarted == true)
            {
                BeginTimer();
                GameIsStarted = false;
            }
            
            FinalTime = TimeCounter;
        }

        public void BeginTimer()
        {
            TimerGoing = true;
            ElapsedTime = 0f;

            StartCoroutine(UpdateTimer());
        }

        public static void EndTimer()
        {
            TimerGoing = false;
            GameIsStarted = false;
        }

        private  IEnumerator UpdateTimer()
        {
            while (TimerGoing)
            {
                ElapsedTime += Time.deltaTime;
                TimePlaying = TimeSpan.FromSeconds(ElapsedTime);
                string timePlayingStr = "Time: " + TimePlaying.ToString("mm':'ss':'ff");
                TimeCounter.text = timePlayingStr;
                yield return null;
            }
        }
        
        public static void StartGame()
        {
            GameIsStarted = true;
        }
    }
}


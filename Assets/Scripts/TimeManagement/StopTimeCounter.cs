using UnityEngine;

namespace TimeManagement
{
    public class StopTimeCounter : MonoBehaviour
    {

        public static bool IsTriggered;
        public static string WhoTriggered;

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
                return;
            }
            
            var carTag = transform.Find("/Car").GetChild(0).tag;
            var enterObjectTag = other.transform.parent.tag;

            if (enterObjectTag == carTag)
            {
                TimeCounterController.EndTimer();
                WhoTriggered = carTag;
                IsTriggered = true;
            }
        }
    }
}
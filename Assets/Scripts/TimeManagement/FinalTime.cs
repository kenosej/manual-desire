using TMPro;
using UnityEngine;

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

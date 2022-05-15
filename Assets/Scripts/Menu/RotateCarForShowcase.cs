using System;
using UnityEngine;

namespace Menu
{
    public class RotateCarForShowcase : MonoBehaviour
    {
        [field: SerializeField] private float RotationSpeed { get; set; } = 20f;

        private void Awake()
        {
            Time.timeScale = 1f;
        }

        private void Update()
        {
            transform.eulerAngles = new Vector3(0f, transform.eulerAngles.y + (RotationSpeed * Time.deltaTime), 0f);
        }
    }
}

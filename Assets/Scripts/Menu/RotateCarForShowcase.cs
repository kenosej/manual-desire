using UnityEngine;

namespace Menu
{
    public class RotateCarForShowcase : MonoBehaviour
    {
        [field: SerializeField] private float RotationSpeed { get; set; } = 20f;
        private Transform _car;

        private void Awake()
        {
            Time.timeScale = 1f;

            _car = transform.GetChild(0);

            DisableCarComponents();
        }

        private void DisableCarComponents()
        {
            foreach (var component in _car.GetComponents<MonoBehaviour>())
                component.enabled = false;
            
            foreach (var audioSource in _car.GetComponents<AudioSource>())
                audioSource.enabled = false;

            _car.GetComponent<Rigidbody>().isKinematic = true;
        }

        private void Update()
        {
            _car.eulerAngles = new Vector3(0f, _car.eulerAngles.y + (RotationSpeed * Time.deltaTime), 0f);
        }
    }
}

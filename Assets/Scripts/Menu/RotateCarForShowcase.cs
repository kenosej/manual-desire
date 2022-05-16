using UnityEngine;

namespace Menu
{
    public class RotateCarForShowcase : MonoBehaviour
    {
        [field: SerializeField] private float RotationSpeed { get; set; } = 20f;
        private bool _carExists;
        
        private Transform _car;

        public Transform Car
        {
            get => _car;
            set
            {
                _car = value;
                _carExists = true;
                DisableCarComponents();
            }
        }

        private void Awake()
        {
            Time.timeScale = 1f;
        }

        public void DestroyTheCar()
        {
            if (!_carExists) return;
            
            Destroy(_car.gameObject);
            _carExists = false;
        }

        private void DisableCarComponents()
        {
            foreach (var component in Car.GetComponents<MonoBehaviour>())
                component.enabled = false;
            
            foreach (var audioSource in Car.GetComponents<AudioSource>())
                audioSource.enabled = false;

            Car.GetComponent<Rigidbody>().isKinematic = true;
        }

        private void Update()
        {
            if (_carExists) RotateTheCar();
        }

        private void RotateTheCar()
        {
            Car.eulerAngles = new Vector3(0f, Car.eulerAngles.y + (RotationSpeed * Time.deltaTime), 0f);
        }
    }
}

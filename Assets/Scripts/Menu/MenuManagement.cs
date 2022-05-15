using System;
using UnityEngine;
using Cars.InputScripts;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

namespace Menu
{
    public class MenuManagement : MonoBehaviour
    {
        private MenuInputActions _mIA;
        
        private GameObject _main;
        private GameObject _cars;
        private GameObject _maps;
        private GameObject _instructions;
        
        private void Awake()
        {
            _mIA = new MenuInputActions();
            _mIA.Menu.Enable();
            _mIA.Menu.Back.performed += GoBack;
            
            LinkGameObjects();
            DisableAllMenus();
            _main.SetActive(true);
        }

        private void OnDestroy()
        {
            _mIA.Menu.Disable();
        }

        private void GoBack(InputAction.CallbackContext obj)
        {
            DisableAllMenus();
            _main.SetActive(true);
        }

        private void LinkGameObjects()
        {
            _main = transform.Find("Main").gameObject;
            _cars = transform.Find("Cars").gameObject;
            _maps = transform.Find("Maps").gameObject;
            _instructions = transform.Find("Instructions").gameObject;
        }

        private void DisableAllMenus()
        {
            _main.SetActive(false);
            _cars.SetActive(false);
            _maps.SetActive(false);
            _instructions.SetActive(false);
        }

        public void Play()
        {
            SceneManager.LoadScene("SampleScene", LoadSceneMode.Single);
        }

        public void OpenCarsMenu()
        {
            DisableAllMenus();
            
            _cars.SetActive(true);
        }
        
        public void OpenMapsMenu()
        {
            DisableAllMenus();
            
            _maps.SetActive(true);
        }

        public void OpenInstructionsMenu()
        {
            DisableAllMenus();
            
            _instructions.SetActive(true);
        }

        public void Quit()
        {
            Debug.Log("Quitting..");
            Application.Quit();
        }
    }
}

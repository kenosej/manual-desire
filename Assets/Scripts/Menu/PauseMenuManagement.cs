using UnityEngine;
using Cars.Movement;
using UnityEngine.SceneManagement;

namespace Menu
{
    public class PauseMenuManagement : MonoBehaviour
    {
        private CanvasGroup _cG;
        private GameObject _pause;
        private GameObject _lost;
        private ParentControl _pC;

        private void Awake()
        {
            _cG = GetComponent<CanvasGroup>();
            ShowInterMenu(false);
            
            _pause = transform.Find("Pause").gameObject;
            _lost = transform.Find("Lost").gameObject;
            
            _pause.SetActive(true);
            _lost.SetActive(false);
            
            _pC = transform.Find("/Car").GetChild(0).gameObject.GetComponent<ParentControl>();
        }

        private void Update()
        {
            if (_pC.IsCarDead)
            {
                _pause.SetActive(false);
                _lost.SetActive(true);
                ShowInterMenu(true);
                Time.timeScale = 0f;
                return;
            }
            
            if (_pC.IsPaused)
            {
                ShowInterMenu(true);
                Time.timeScale = 0f;
            }
            else
            {
                ShowInterMenu(false);
                Time.timeScale = 1f;
            }
        }

        private void ShowInterMenu(bool show)
        {
            _cG.alpha = show ? 1f : 0f;
        }

        public void Resume()
        {
            if (_cG.alpha == 0f) return;
            
            _pC.IsPaused = false;
        }

        public void Quit()
        {
            if (_cG.alpha == 0f) return;
            
            SceneManager.LoadScene("MenuScene", LoadSceneMode.Single);
        }
    }
}

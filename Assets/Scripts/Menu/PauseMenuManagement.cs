using UnityEngine;
using Cars.Movement;
using UnityEngine.SceneManagement;

namespace Menu
{
    public class PauseMenuManagement : MonoBehaviour
    {
        private CanvasGroup _cG;
        private ParentControl _pC;

        private void Awake()
        {
            _cG = GetComponent<CanvasGroup>();
            ShowPauseMenu(false);
            _pC = transform.Find("/Car").GetChild(0).gameObject.GetComponent<ParentControl>();
        }

        private void Update()
        {
            if (_pC.IsPaused)
            {
                ShowPauseMenu(true);
                Time.timeScale = 0f;
            }
            else
            {
                ShowPauseMenu(false);
                Time.timeScale = 1f;
            }
        }

        private void ShowPauseMenu(bool show)
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

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
            _pC = transform.Find("/Car").gameObject.GetComponent<ParentControl>();
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
                Time.timeScale = 1f;
            }
        }

        private void ShowPauseMenu(bool show)
        {
            _cG.alpha = show ? 1f : 0f;
        }

        public void Resume()
        {
            _pC.IsPaused = false;
        }

        public void Quit()
        {
            SceneManager.LoadScene("MenuScene", LoadSceneMode.Single);
        }
    }
}

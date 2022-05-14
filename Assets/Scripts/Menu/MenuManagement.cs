using UnityEngine;
using UnityEngine.SceneManagement;

namespace Menu
{
    public class MenuManagement : MonoBehaviour
    {
        public void ChangeIntoPlayScene()
        {
            SceneManager.LoadScene("SampleScene", LoadSceneMode.Single);
        }
        
        // Application.Quit()
    }
}

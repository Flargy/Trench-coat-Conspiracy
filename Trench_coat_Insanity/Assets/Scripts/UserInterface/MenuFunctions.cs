using UnityEngine;
using UnityEngine.SceneManagement;

namespace UserInterface
{
    public class MenuFunctions : MonoBehaviour
    {
        public void StartGame(int sceneIndex)
        {
            SceneManager.LoadScene(sceneIndex);
        }

        public void QuitGame()
        {
            Application.Quit();
        }
    }
}
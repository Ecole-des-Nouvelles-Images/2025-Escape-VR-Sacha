using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void StartGame()
    {
        SceneManager.LoadScene("Integration");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}

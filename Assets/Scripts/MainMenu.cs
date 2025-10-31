using System;
using System.Collections;
using Manager;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{

    public void StartGame()
    {
        SceneLoader.OnLoad.Invoke("Integration");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}

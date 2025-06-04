using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private GameObject _loadingScreen;
    [SerializeField] private Slider _loadingBarFill;

    private void Awake()
    {
        _loadingScreen.SetActive(false);
    }

    public void StartGame()
    {
        StartCoroutine(LoadSceneAsync("Integration"));
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    IEnumerator LoadSceneAsync(string sceneName)
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName);
        _loadingScreen.SetActive(true);
        while (asyncLoad != null && !asyncLoad.isDone)
        {
            Debug.Log(asyncLoad.progress);
            float progress = Mathf.Clamp01(asyncLoad.progress / .9f);
            _loadingBarFill.value = progress;
            yield return new WaitForEndOfFrame();
        }
    }
}

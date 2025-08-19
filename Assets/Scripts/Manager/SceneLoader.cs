using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Utils;

namespace Manager
{
    public class SceneLoader : MonoBehaviour
    {
        public static Action<string> OnLoad;
        
        [SerializeField] private GameObject _loadingScreen;
        [SerializeField] private Slider _loadingSlider;
        [SerializeField] private string _firstSceneName;

        private void OnEnable()
        {
            OnLoad += LoadScene;
        }

        private void OnDisable()
        {
            OnLoad -= LoadScene;
        }

        private void OnDestroy()
        {
            OnLoad -= LoadScene;
        }

        private void Start()
        {
            _loadingScreen.SetActive(false);
            OnLoad.Invoke(_firstSceneName);
        }

        private void LoadScene(string sceneName)
        {
            if(sceneName == "MainMenu")
                GameEvents.OnEnableFarInteractor.Invoke();
            else
            {
                GameEvents.OnDisableFarInteractor.Invoke();
            }
            StartCoroutine(LoadSceneAsync(sceneName));
        }

        IEnumerator LoadSceneAsync(string sceneName)
        {
            AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName);
            _loadingScreen.SetActive(true);
            while (asyncLoad != null && !asyncLoad.isDone)
            {
                //Debug.Log(asyncLoad.progress);
                float progress = Mathf.Clamp01(asyncLoad.progress / .9f);
                _loadingSlider.value = progress;
                yield return new WaitForEndOfFrame();
            }
            _loadingScreen.SetActive(false);
            _loadingSlider.value = 0;
            yield return null;
            
        }
    }
}
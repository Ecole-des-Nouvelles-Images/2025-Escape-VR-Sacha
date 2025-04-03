using System;
using Manager;
using UnityEngine;

namespace Props
{
    public class DecorPlayerDetector : MonoBehaviour
    {
        private SceneFader _sceneFader;

        private void Awake()
        {
            _sceneFader = FindFirstObjectByType<SceneFader>();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("MainCamera"))
            {
                _sceneFader.OnFadeIn.Invoke(_sceneFader.CurrentFadeType);
                Debug.Log("On Fade Out");
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("MainCamera"))
            {
                _sceneFader.OnFadeOut.Invoke(_sceneFader.CurrentFadeType);
                Debug.Log("On Fade In");
            }
        }
    }
}
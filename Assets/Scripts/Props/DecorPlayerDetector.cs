using System;
using Manager;
using UnityEngine;

namespace Props
{
    public class DecorPlayerDetector : MonoBehaviour
    {
        [SerializeField] private bool _isTriggerByEnter;
        private SceneFader _sceneFader;

        private void Awake()
        {
            _sceneFader = FindFirstObjectByType<SceneFader>();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("MainCamera"))
            {
                if (_isTriggerByEnter)
                {
                    _sceneFader.OnFadeOut.Invoke(_sceneFader.CurrentFadeType);
                    Debug.Log("On Fade In");
                }
                else
                {
                    _sceneFader.OnFadeIn.Invoke(_sceneFader.CurrentFadeType);
                    Debug.Log("On Fade Out");
                }
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("MainCamera"))
            {
                if (_isTriggerByEnter)
                {
                    _sceneFader.OnFadeIn.Invoke(_sceneFader.CurrentFadeType);
                    Debug.Log("On Fade Out");
                }
                else
                {
                    _sceneFader.OnFadeOut.Invoke(_sceneFader.CurrentFadeType);
                    Debug.Log("On Fade In");
                }
            }
        }
    }
}
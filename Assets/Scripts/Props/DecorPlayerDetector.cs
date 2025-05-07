using System;
using Manager;
using UnityEngine;
using Utils;

namespace Props
{
    public class DecorPlayerDetector : MonoBehaviour
    {
        [SerializeField] private bool _isTriggerByEnter;
        private SceneFader _sceneFader;
        private bool _isEnable;
        private float _currentFadeDisableTime;

        private void OnEnable()
        {
            GameEvents.OnTeleport += DisableFadeSystem;
        }

        private void OnDisable()
        {
            GameEvents.OnTeleport -= DisableFadeSystem;
        }
        private void Awake()
        {
            _sceneFader = FindFirstObjectByType<SceneFader>();
        }

        private void Update()
        {
            if (_currentFadeDisableTime > 0 && _isEnable == false)
            {
                _currentFadeDisableTime -= Time.deltaTime;
            }
            else
            {
                _isEnable = true;
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("MainCamera") && _isEnable)
            {
                if (_isTriggerByEnter)
                {
                    _sceneFader.OnFadeOut.Invoke(_sceneFader.CurrentFadeType);
                }
                else
                {
                    _sceneFader.OnFadeIn.Invoke(_sceneFader.CurrentFadeType);
                }
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("MainCamera") && _isEnable)
            {
                if (_isTriggerByEnter)
                {
                    _sceneFader.OnFadeIn.Invoke(_sceneFader.CurrentFadeType);
                }
                else
                {
                    _sceneFader.OnFadeOut.Invoke(_sceneFader.CurrentFadeType);
                }
            }
        }
        private void DisableFadeSystem()
        {
            _isEnable = false;
            _currentFadeDisableTime = .5f;
        }
    }
}
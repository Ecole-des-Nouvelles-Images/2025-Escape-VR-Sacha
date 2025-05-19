using System;
using Manager;
using UnityEngine;
using Utils;

namespace Props
{
    public class DecorPlayerDetector : MonoBehaviour
    {
        [SerializeField] private bool _isTriggerByEnter;
        [SerializeField] private string _sceneFaderID;
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
                    GameEvents.OnFadeScreen(_sceneFaderID, false);
                }
                else
                {
                    GameEvents.OnFadeScreen(_sceneFaderID, true);
                }
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("MainCamera") && _isEnable)
            {
                if (_isTriggerByEnter)
                {
                    GameEvents.OnFadeScreen(_sceneFaderID, true);
                }
                else
                {
                    GameEvents.OnFadeScreen(_sceneFaderID, false);
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
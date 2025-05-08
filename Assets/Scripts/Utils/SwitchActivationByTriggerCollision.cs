using System;
using UnityEngine;

namespace Utils
{
    public class SwitchActivationByTriggerCollision : MonoBehaviour
    {
        [SerializeField] private GameObject _activeObjectIn;
        [SerializeField] private GameObject _activeObjectOut;
        [SerializeField] private ObjectDetectorByTag _detector;

        private void Update()
        {
            
            if (_detector.ObjectDetected)
            {
                _activeObjectIn.SetActive(true);
                _activeObjectOut.SetActive(false);
            }
            else
            {
                _activeObjectIn.SetActive(false);
                _activeObjectOut.SetActive(true);
            }
        }
    }
}

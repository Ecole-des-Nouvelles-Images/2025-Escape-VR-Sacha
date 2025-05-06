using System;
using UnityEngine;

namespace Utils
{
    public class SwitchActivationByTriggerCollision : MonoBehaviour
    {
        [SerializeField] private GameObject _activeObjectIn;
        [SerializeField] private GameObject _activeObjectOut;

        private void OnTriggerEnter(Collider other)
        {
            _activeObjectIn.SetActive(true);
            _activeObjectOut.SetActive(false);
        }
        private void OnTriggerExit(Collider other)
        {
            _activeObjectIn.SetActive(false);
            _activeObjectOut.SetActive(true);
        }
    }
}

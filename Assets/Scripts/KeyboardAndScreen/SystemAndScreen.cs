using System;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace KeyboardAndScreen
{
    public class SystemAndScreen : MonoBehaviour
    {
        public bool IsUnlock;
        
        [SerializeField] private TMP_Text _display;
        [SerializeField] private Image _displayBackground;
        [SerializeField] private string _keyInputs;
        
        private string _listInput = "" ;
        private float _timerError = 0.5f;
        private bool _isTimer;

        private void Start()
        {
            Display();
            _displayBackground.color = Color.white;
        }

        private void Update()
        {
            if (_isTimer)
                Timer();
        }

        private void Validation()
        {
            if (_listInput == _keyInputs)
            {
                IsUnlock = true;
                _displayBackground.color = Color.green;
            }
            else
            {
                IsUnlock = false;
                _listInput = "";
                Display();
                _isTimer = true;
            }
        }

        private void Timer()
        {
            _timerError -= Time.deltaTime;
            if (_timerError <= 0)
            {
                _displayBackground.color = Color.white;
            }
            else
            {
                _displayBackground.color = Color.red;
                _isTimer = false;
                _timerError = 0.5f;
            }
        }

        private void Display()
        {
            _display.text = _listInput;
        }
        
        public void AddInputs(string value)
        {
            if (_listInput.Length < _keyInputs.Length)
            {
                _listInput += value;
                Display();
            }
            if (_listInput.Length == _keyInputs.Length || _listInput.Length > _keyInputs.Length)
            {
                Validation();
            }
        }
    }
}

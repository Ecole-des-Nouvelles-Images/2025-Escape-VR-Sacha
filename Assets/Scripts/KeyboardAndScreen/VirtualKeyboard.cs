using System;
using System.Collections.Generic;
using Manager;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;
using Utils;

namespace KeyboardAndScreen
{
    public class VirtualKeyboard : MonoBehaviour
    {
        [SerializeField] private TMP_Text _display;
        [SerializeField] private Image _displayBackground;
        [SerializeField] private string _keyboardUnlockID;
        [SerializeField] private string _keyInputs;
        
        private string _listInput = "" ;
        private List<int> _values = new List<int>();
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

        public void Validation()
        {
            if (_listInput == _keyInputs)
            {
                GameEvents.OnKeyboardUnlock.Invoke(_keyboardUnlockID);
                _displayBackground.color = Color.green;
            }
            else
            {
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
        
        public void AddInputs(int value)
        {
            if (_listInput.Length < _keyInputs.Length)
            {
                _values.Add(value);
                _listInput += value;
                Display();
            }
            if (_listInput.Length == _keyInputs.Length || _listInput.Length > _keyInputs.Length)
            {
                Validation();
            }
        }

        public void RemoveInput()
        {
            if (_listInput != "" && _values.Count > 0)
            {
                _values.Remove(_values[_values.Count-1]);
                _listInput="";
                foreach (int value in _values)
                {
                    _listInput+=value;
                }
                Debug.Log(_listInput);
                Display();
            }
        }
    }
}

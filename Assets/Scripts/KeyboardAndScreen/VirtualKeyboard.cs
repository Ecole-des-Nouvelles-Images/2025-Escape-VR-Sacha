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
        [SerializeField] private AudioSource _successAudioSource;
        [SerializeField] private AudioClip _successSound;
        [SerializeField] private AudioClip _failSound;
        
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

        private void OnTriggerEnter(Collider other)
        {
            
            Debug.Log("Collision détecté!");
            if (other.CompareTag("KeyObject"))
            {
                
                Debug.Log("touche entré");
                other.GetComponent<VNumpadInput>().EnterInput();
            }
        }

        public void Validation()
        {
            if (_listInput == _keyInputs)
            {
                GameEvents.OnKeyboardUnlock.Invoke(_keyboardUnlockID);
                _displayBackground.color = Color.green;
                _successAudioSource.clip = _successSound;
                _successAudioSource.Play();
            }
            else
            {
                _listInput = "";
                Display();
                _successAudioSource.clip = _failSound;
                _successAudioSource.Play();
                _isTimer = true;
                _timerError = 0.5f;
            }
        }

        private void Timer()
        {
            _timerError -= Time.deltaTime;
            if (_timerError <= 0)
            {
                _displayBackground.color = Color.white;
                _isTimer = false;
            }
            else
            {
                _displayBackground.color = Color.red;
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

using System;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace KeyboardAndScreen
{
    public class VNumpadInput : MonoBehaviour
    {
        [SerializeField] private VirtualKeyboard _myVirtualKeyboardSystem;
        [SerializeField] private int _myValue;
        
        private AudioSource _audioSource;

        private void Awake()
        {
            if (TryGetComponent(out _audioSource))
                _audioSource = GetComponent<AudioSource>();
            _audioSource.playOnAwake = false;
        }

        public void EnterInput()
        {
            if(_myValue == 999)
                _myVirtualKeyboardSystem.RemoveInput();
            else if(_myValue == 666)
                _myVirtualKeyboardSystem.Validation();
            else
                _myVirtualKeyboardSystem.AddInputs(_myValue);
            if (_audioSource)
            {
                _audioSource.Play();
            }
        }
    }
}

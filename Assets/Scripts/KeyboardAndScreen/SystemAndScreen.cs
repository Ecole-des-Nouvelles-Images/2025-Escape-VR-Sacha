using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

namespace KeyboardAndScreen
{
    public class SystemAndScreen : MonoBehaviour
    {
        public bool IsUnlock;
        
        [SerializeField] private TMP_Text _display;
        [SerializeField] private string _keyInputs;
        
        private string _listInput ;

        private void Validation()
        {
            if (_listInput == _keyInputs)
            {
                IsUnlock = true;
            }
            else
            {
                IsUnlock = false;
                _listInput = "";
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

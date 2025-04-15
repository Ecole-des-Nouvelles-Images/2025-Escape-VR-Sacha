using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace KeyboardAndScreen
{
    public class VNumpadInput : MonoBehaviour
    {
        [FormerlySerializedAs("_myKeyboardSystem")] [FormerlySerializedAs("_mySystem")] [SerializeField] private VirtualKeyboard _myVirtualKeyboardSystem;
        [SerializeField] private string _myValue;

        public void EnterInput()
        {
            _myVirtualKeyboardSystem.AddInputs(_myValue);
        }
    }
}

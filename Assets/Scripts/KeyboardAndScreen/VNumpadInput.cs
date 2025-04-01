using UnityEngine;
using UnityEngine.UI;

namespace KeyboardAndScreen
{
    public class VNumpadInput : MonoBehaviour
    {
        [SerializeField] private SystemAndScreen _mySystem;
        [SerializeField] private string _myValue;

        public void EnterInput()
        {
            _mySystem.AddInputs(_myValue);
        }
    }
}

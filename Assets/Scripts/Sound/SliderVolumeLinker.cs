using UnityEngine;
using UnityEngine.UIElements;
using Utils;

namespace Sound
{
    public class SliderVolumeLinker : MonoBehaviour
    {
        [SerializeField] private string _mixerName;
        [SerializeField] private Slider _mySlider;

        private float _updatedValue;
        
        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            _mySlider.lowValue = 0;
            _mySlider.highValue = 20;
            _mySlider.value = 10;
            _updatedValue = _mySlider.value;
            GameEvents.OnSliderModified.Invoke(_mixerName, _mySlider.value);
        }

        // Update is called once per frame
        void Update()
        {
            if (!Mathf.Approximately(_mySlider.value, _updatedValue))
            {
                GameEvents.OnSliderModified.Invoke(_mixerName, _mySlider.value);
                _updatedValue = _mySlider.value;
            }
        }
    }
}

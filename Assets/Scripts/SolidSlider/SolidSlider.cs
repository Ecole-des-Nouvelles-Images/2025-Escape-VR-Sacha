using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace SolidSlider
{
    public class SolidSlider : MonoBehaviour
    {
        [SerializeField] private Transform _handleTransform;
        [SerializeField] private float _minimumLimitCursorPosition;
        [SerializeField] private float _maximumLimitCursorPosition;
        [SerializeField] private bool _isHorizontalSlider;
        [SerializeField] private Slider _sliderScreenUIDisplayed;

        private Vector3 _lastCursorPosition;
    
        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            PercentConversionValueFromSliderUI();
            _lastCursorPosition = _handleTransform.transform.position;
        }

        // Update is called once per frame
        void Update()
        {
            if (_isHorizontalSlider)
            {
                if (_handleTransform.localPosition.x < _minimumLimitCursorPosition)
                {
                    Debug.Log("handle position: "+_handleTransform.position.x + "limit min = "+ _minimumLimitCursorPosition);
                    Vector3 posLimit = new Vector3(_minimumLimitCursorPosition, _handleTransform.localPosition.y, _handleTransform.localPosition.z);
                    _handleTransform.localPosition = posLimit;
                }
                if (_handleTransform.localPosition.x > _maximumLimitCursorPosition)
                {
                    Debug.Log("handle position: "+_handleTransform.position.x + "limit max = "+ _maximumLimitCursorPosition);
                    Vector3 posLimit = new Vector3(_maximumLimitCursorPosition, _handleTransform.localPosition.y, _handleTransform.localPosition.z);
                    _handleTransform.localPosition = posLimit;
                }
            }
            else
            {
                if (_handleTransform.localPosition.y < _minimumLimitCursorPosition)
                {
                    Vector3 posLimit = new Vector3(_handleTransform.localPosition.x, _minimumLimitCursorPosition, _handleTransform.localPosition.z);
                    _handleTransform.localPosition = posLimit;
                }
                if (_handleTransform.localPosition.y > _maximumLimitCursorPosition)
                {
                    Vector3 posLimit = new Vector3(_handleTransform.localPosition.x, _maximumLimitCursorPosition, _handleTransform.localPosition.z);
                    _handleTransform.localPosition = posLimit;
                }
            }
            
            Debug.Log("handle position: "+_handleTransform.transform.localPosition.x);

            if (_handleTransform.transform.localPosition != _lastCursorPosition)
            {
                Debug.Log("handle position: "+_handleTransform.transform.position.x);
                _lastCursorPosition = _handleTransform.transform.localPosition;
                PercentConversionValueToSliderUI();
            }
        }

        private void PercentConversionValueToSliderUI()
        {
            if (_isHorizontalSlider)
            {
                float percentValue = _handleTransform.transform.localPosition.x / _maximumLimitCursorPosition * 100;
                _sliderScreenUIDisplayed.value = percentValue - 80;
            }
            else
            {
                float percentValue = _handleTransform.transform.localPosition.y / _maximumLimitCursorPosition * 100;
                _sliderScreenUIDisplayed.value = percentValue - 80;
            }
        }
        private void PercentConversionValueFromSliderUI()
        {
            float value = _maximumLimitCursorPosition * (_sliderScreenUIDisplayed.value + 80) / 100;
            if (_isHorizontalSlider && !Mathf.Approximately(_handleTransform.transform.position.x, value))
            {
                Vector3 pos = new Vector3(value, _handleTransform.transform.localPosition.y, _handleTransform.transform.localPosition.z);
                _handleTransform.transform.localPosition = pos;
                //_handleTransform.transform.Translate(value,_handleTransform.transform.localPosition.y,_handleTransform.transform.position.z);
            }
            else
            {
                Vector3 pos = new Vector3( _handleTransform.transform.localPosition.x, value, _handleTransform.transform.position.z);
                _handleTransform.transform.position = pos;
            }
            Debug.Log("handle position: "+_handleTransform.transform.localPosition.x + "value= "+value);
        }
    }
}

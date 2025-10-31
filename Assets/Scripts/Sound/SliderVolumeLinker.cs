using System;
using UnityEngine;
using Utils;
using Slider = UnityEngine.UI.Slider;

namespace Sound
{
    public class SliderVolumeLinker : MonoBehaviour
    {
        [SerializeField] private string _mixerName;
        [SerializeField] private Slider _mySlider;

        private float _volume;

        private void OnEnable()
        {
            GameEvents.OnSliderModified += UpdateSlidersSynchronisation;
        }

        private void OnDisable()
        {
            GameEvents.OnSliderModified -= UpdateSlidersSynchronisation;
        }

        private void Awake()
        {
            _mySlider.minValue = -80;
            _mySlider.maxValue = 20;
            _mySlider.value = 0;
            switch (_mixerName)
            {
                case "master":
                    if (PlayerPrefs.HasKey("masterVolume"))
                        LoadVolume();
                    else
                        SetVolume();
                    break;
                case "fx":
                    if (PlayerPrefs.HasKey("fxVolume"))
                        LoadVolume();
                    else
                        SetVolume();
                    break;
                case "music":
                    if (PlayerPrefs.HasKey("musicVolume"))
                        LoadVolume();
                    else
                        SetVolume();
                    break;
                case "ambiance":
                    if (PlayerPrefs.HasKey("ambianceVolume"))
                        LoadVolume();
                    else
                        SetVolume();
                    break;
            }
        }
        private void Update()
        {
            if (!Mathf.Approximately(_mySlider.value, _volume))
            {
                SetVolume();
            }
        }

        private void SetVolume()
        {
            GameEvents.OnSliderModified.Invoke(_mixerName, _mySlider.value);
            _volume = _mySlider.value;
            switch (_mixerName)
            {
                case "master":
                    PlayerPrefs.SetFloat("masterVolume", _volume);
                    break;
                case "fx":
                    PlayerPrefs.SetFloat("fxVolume", _volume);
                    break;
                case "music":
                    PlayerPrefs.SetFloat("musicVolume", _volume);
                    break;
                case "ambiance":
                    PlayerPrefs.SetFloat("ambianceVolume", _volume);
                    break;
            }
        }

        private void LoadVolume()
        {
            switch (_mixerName)
            {
                case "master":
                    _mySlider.value = PlayerPrefs.GetFloat("masterVolume");
                    break;
                case "fx":
                    _mySlider.value = PlayerPrefs.GetFloat("fxVolume");
                    break;
                case "music":
                    _mySlider.value = PlayerPrefs.GetFloat("musicVolume");
                    break;
                case "ambiance":
                    _mySlider.value = PlayerPrefs.GetFloat("ambianceVolume");
                    break;
            }
            SetVolume();
        }

        private void UpdateSlidersSynchronisation(string mixerID, float value)
        {
            if (mixerID == _mixerName)
            {
                _mySlider.value = value;
                _volume = value;
            }
        }
    }
}

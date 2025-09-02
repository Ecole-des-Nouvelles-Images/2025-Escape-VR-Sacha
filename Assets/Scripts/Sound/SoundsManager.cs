using System;
using UnityEngine;
using UnityEngine.Audio;
using Utils;

namespace Sound
{
    public class SoundsManager : MonoBehaviour
    {
        [Header("Mixer system")]
        [SerializeField] private AudioMixer _mixer;

        private void OnEnable()
        {
            GameEvents.OnSliderModified += UpdateMixerVolume;
        }
        private void OnDisable()
        {
            GameEvents.OnSliderModified -= UpdateMixerVolume;
        }

        private void OnDestroy()
        {
            GameEvents.OnSliderModified -= UpdateMixerVolume;
        }

        private void UpdateMixerVolume(string mixerID, float value)
        {
            _mixer.SetFloat(mixerID, value);
        }
    }
}
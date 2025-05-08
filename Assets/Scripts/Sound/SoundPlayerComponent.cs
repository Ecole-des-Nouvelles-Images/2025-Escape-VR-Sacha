using System;
using UnityEngine;
using Utils;

namespace Sound
{
    [RequireComponent(typeof(AudioSource))]
    public class SoundPlayerComponent : MonoBehaviour
    {
        [SerializeField] private string _cliplistName;
        [SerializeField] private bool _isRandomClip;
        [SerializeField] private int _clipIndex;
        
        private AudioSource _myAudioSource;

        private void Awake()
        {
            _myAudioSource = transform.GetComponent<AudioSource>();
        }

        public void PlaySound()
        {
            GameEvents.OnPlaySound.Invoke(_cliplistName, _isRandomClip, _clipIndex, _myAudioSource);
        }
    }
}

using System;
using UnityEngine;

namespace Sound
{
    public class AudioClipListReader : MonoBehaviour
    {
        [SerializeField] private AudioSource _myAudioSource;
        [SerializeField] private AudioClip[] _audioClips;
        [SerializeField] private bool _isLooping;
        
        private bool _isPlayingList;
        private int _index;

        private void Awake()
        {
            _myAudioSource.loop = false;
            _myAudioSource.playOnAwake = false;
        }

        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            if (!_myAudioSource.isPlaying)
            {
                PlayIndexedClip();
            }
        }

        // Update is called once per frame
        void Update()
        {
            if (!_myAudioSource.isPlaying && _isPlayingList)
            {
                PlayIndexedClip();
            }
        }

        private void PlayIndexedClip()
        {
            _isPlayingList = true;
            _myAudioSource.clip = _audioClips[_index];
            _myAudioSource.Play();
            if(_index < _audioClips.Length)
                _index += 1;
            else if(_isLooping == false)
            {
                _isPlayingList = false;
            }
            else
            {
                _index = 0;
            }
        }
    }
}

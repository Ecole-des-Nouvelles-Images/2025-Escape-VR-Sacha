using System;
using Props;
using UnityEngine;
using Utils;

namespace Sound
{
    public class AudioClipListReader : MonoBehaviour
    {
        [SerializeField] private AudioSource _myAudioSource;
        [SerializeField] private AudioClip[] _audioClips;
        [SerializeField] private bool _isLooping;
        [SerializeField] private string _myBoomboxID;

        private void OnEnable()
        {
            GameEvents.OnBoomboxInput += BoomboxEvent;
        }

        private void OnDisable()
        {
            GameEvents.OnBoomboxInput -= BoomboxEvent;
        }

        private void OnDestroy()
        {
            GameEvents.OnBoomboxInput -= BoomboxEvent;
        }

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
            if (_index < _audioClips.Length)
                _index += 1;
            else if (_isLooping == false)
            {
                _isPlayingList = false;
            }
            else
            {
                _index = 0;
            }
        }

        private void BoomboxEvent(string boomboxID, int commandID)
        {
            if (boomboxID == _myBoomboxID)
            {
                switch (commandID)
                {
                    case 1:
                        if(_myAudioSource.isPlaying)
                            _myAudioSource.Pause();
                        else if(!_myAudioSource.isPlaying && !_isPlayingList)
                        {
                            _myAudioSource.Play();
                        }
                        else
                        {
                            _myAudioSource.UnPause();
                        }
                        break;
                    case 3:
                        PlayIndexedClip();
                        break;
                    case 4:
                        if (_index == 1)
                            _index = _audioClips.Length - 1;
                        if (_index == 0)
                            _index = _audioClips.Length - 2;
                        PlayIndexedClip();
                        break;
                }
            }
        }
    }
}

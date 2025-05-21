using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Sound
{
    public class AudioClipRandomPlayer : MonoBehaviour
    {
        [SerializeField] private AudioSource _myAudioSource;
        [SerializeField] private List<AudioClip> _myClipList;
        [SerializeField] private bool _isLooping = true;
        [SerializeField] private bool _isRandomDelay = true;
        [SerializeField] private bool _dontPlaySameSound;
        [SerializeField] private float _delayTime;
        [SerializeField] private float _minDelayTime;
        [SerializeField] private float _maxDelayTime;

        private float _currentDelayTime;
        private int _lastSoundIndex;

        private void Awake()
        {
            ResetDelay();
            if (_isLooping == false)
            {
                PlayClip();
            }
        }

        private void Update()
        {
            if (_isLooping)
            {
                if (_currentDelayTime > 0)
                {
                    _currentDelayTime -= Time.deltaTime;
                }
                else
                {
                    PlayClip();
                    ResetDelay();
                }
            }
        }

        private void SelectRandomClip()
        {
            int newIndex = Random.Range(0, _myClipList.Count-1);
            if (newIndex == _lastSoundIndex && _dontPlaySameSound)
            {
                if (newIndex < _myClipList.Count-1)
                {
                    newIndex += 1;
                }
                else
                {
                    newIndex = 0;
                }
            }
            _lastSoundIndex = newIndex;
            _myAudioSource.clip = _myClipList[newIndex];
        }

        private void ResetDelay()
        {
            if (_isRandomDelay)
            {
                _currentDelayTime = Random.Range(_minDelayTime, _maxDelayTime);
            }
            else
            {
                _currentDelayTime = _delayTime;
            }
        }

        public void PlayClip()
        {
            SelectRandomClip();
            _myAudioSource.Play();
        }
    }
}

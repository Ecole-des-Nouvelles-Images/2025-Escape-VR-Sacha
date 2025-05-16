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
        [SerializeField] private float _delayTime;
        [SerializeField] private float _minDelayTime;
        [SerializeField] private float _maxDelayTime;

        private float _currentDelayTime;

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
            _myAudioSource.clip = _myClipList[Random.Range(0, _myClipList.Count - 1)];
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

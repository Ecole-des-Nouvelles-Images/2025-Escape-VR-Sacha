using System;
using Props;
using UnityEngine;
using Utils;

namespace Sound
{
    public class LivingRoomMusic : MonoBehaviour
    {
        [SerializeField] private AudioSource _myAudioSource;
        [SerializeField] private string _myRemoteID;

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
                NextClip();
                PlayIndexedClip();
            }
        }

        private void PlayIndexedClip()
        {
            _isPlayingList = true;
            switch (_index)
            {
               case 0:
                   _myAudioSource.clip = Resources.Load<AudioClip>("Musics/LivingRoom/I'llShowYouSomething");
                   break;
               case 1:
                   _myAudioSource.clip = Resources.Load<AudioClip>("Musics/LivingRoom/LiveForTheNight");
                   break;
               case 2:
                   _myAudioSource.clip = Resources.Load<AudioClip>("Musics/LivingRoom/SpeedUp");
                   break;
               case 3:
                   _myAudioSource.clip = Resources.Load<AudioClip>("Musics/LivingRoom/DaytimeTVDiscoGroove");
                   break;
               case 4:
                   _myAudioSource.clip = Resources.Load<AudioClip>("Musics/LivingRoom/PromWaltz");
                   break;
            }
            _myAudioSource.Play();
        }

        private void NextClip()
        {
            if (_index < 4)
                _index += 1;
            else
            {
                _index = 0;
            }
        }

        private void PreviousClip()
        {
            if (_index > 0)
                _index -= 1;
            else
            {
                _index = 4;
            }
        }

        private void BoomboxEvent(string boomboxID, int commandID)
        {
            if (boomboxID == _myRemoteID)
            {
                switch (commandID)
                {
                    case 1:
                        Debug.Log("play/pause \n is playing?"+ _myAudioSource.isPlaying  );
                        if(_myAudioSource.isPlaying)
                        {
                            Debug.Log("pause");
                            _isPlayingList = false;
                            _myAudioSource.Stop();
                        }
                        else
                        {
                            Debug.Log("play");
                            _isPlayingList = true;
                            _myAudioSource.Play();
                        }
                        break;
                    case 3:
                        Debug.Log("next");
                        NextClip();
                        PlayIndexedClip();
                        break;
                    case 4:
                        Debug.Log("previous");
                        PreviousClip();
                        PlayIndexedClip();
                        break;
                }
            }
        }
    }
}

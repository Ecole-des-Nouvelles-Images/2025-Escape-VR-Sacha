using System;
using System.Collections.Generic;
using Data;
using UnityEngine;
using Utils;
using Random = UnityEngine.Random;

namespace Son
{
    public class SoundsDeck : MonoBehaviour
    {
        [SerializeField] private List<AudioClipList> _audioData;

        private void OnEnable()
        {
            GameEvents.OnPlaySound += SetupAndPlay;
        }

        private void OnDisable()
        {
            GameEvents.OnPlaySound -= SetupAndPlay;
        }

        private void SetupAndPlay(string clipListName, bool isRandomClip, int indexClip, AudioSource audioSource)
        {
            if (isRandomClip)
            {
                for (int i = 0; i < _audioData.Count; i++)
                {
                    if (_audioData[i].name == clipListName)
                    {
                        audioSource.clip = _audioData[i].AudioClips[Random.Range(0, _audioData[i].AudioClips.Length-1)];
                        audioSource.loop = _audioData[i].IsLoopingSounds;
                        if (_audioData[i].Is3DSounds)
                            audioSource.spatialBlend = 1;
                        else
                            audioSource.spatialBlend = 0;
                        break;
                    }
                }
            }
            else
            {
                for (int i = 0; i < _audioData.Count; i++)
                {
                    if (_audioData[i].name == clipListName)
                    {
                        audioSource.clip = _audioData[i].AudioClips[indexClip];
                        audioSource.loop = _audioData[i].IsLoopingSounds;
                        if (_audioData[i].Is3DSounds)
                        {
                            audioSource.spatialBlend = 1;
                        }
                        else
                            audioSource.spatialBlend = 0;
                        break;
                    }
                }
            }
            audioSource.Play();
        }
    }
}

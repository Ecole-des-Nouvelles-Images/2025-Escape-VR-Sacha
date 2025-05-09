using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Sound
{
    public class ClipListComponent : MonoBehaviour
    {
        [SerializeField] private AudioSource _myAudioSource;
        [SerializeField] private List<AudioClip> _myClipList;
        [SerializeField] private bool _randomSelection = true;

        private void Awake()
        {
            if (_randomSelection)
            {
                _myAudioSource.clip = _myClipList[Random.Range(0, _myClipList.Count - 1)];
            }
        }
    }
}

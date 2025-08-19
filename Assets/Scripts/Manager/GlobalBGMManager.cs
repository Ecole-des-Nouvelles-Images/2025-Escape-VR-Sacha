using System;
using UnityEngine;
using Utils;

namespace Manager
{
    public class GlobalBGMManager : MonoBehaviour
    {
        [SerializeField] private AudioSource _bgmAudioSource;

        private void OnEnable()
        {
            GameEvents.OnPlayBGM += PlayBGM;
            GameEvents.OnStopBGM += StopBGM;
        }

        private void OnDisable()
        {
            GameEvents.OnPlayBGM -= PlayBGM;
            GameEvents.OnStopBGM -= StopBGM;
        }

        private void OnDestroy()
        {
            GameEvents.OnPlayBGM -= PlayBGM;
            GameEvents.OnStopBGM -= StopBGM;
        }

        private void PlayBGM(string id)
        {
            _bgmAudioSource.Stop();
            switch (id)
            {
                case "menu":
                    _bgmAudioSource.clip = Resources.Load<AudioClip>("Musics/Menu/ThisIsThePlace");
                    break;
                case "intro":
                    _bgmAudioSource.clip = Resources.Load<AudioClip>("Musics/IntroRoom/Pair");
                    break;
                case "corridor":
                    _bgmAudioSource.clip = Resources.Load<AudioClip>("Musics/Corridor/Escapism");
                    break;
                case "kid":
                    _bgmAudioSource.clip = Resources.Load<AudioClip>("Musics/KidRoom/MiniatureUniverse");
                    break;
                case "office":
                    _bgmAudioSource.clip = Resources.Load<AudioClip>("Musics/Office/Cellular");
                    break;
                case "bad":
                    _bgmAudioSource.clip = Resources.Load<AudioClip>("Musics/Credits/PhantomTides");
                    break;
                case "good":
                    _bgmAudioSource.clip = Resources.Load<AudioClip>("Musics/Credits/SolidBonds");
                    break;
            }
            _bgmAudioSource.Play();
        }

        private void StopBGM()
        {
            _bgmAudioSource.Stop();
        }
    }
}

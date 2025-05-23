using System;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(AudioSource))]
[RequireComponent(typeof(Button))]
public class ButtonAudioFeedBack : MonoBehaviour
{
    [SerializeField] private AudioClip _selectClip;
    [SerializeField] private AudioClip _clickClip;
    
    private AudioSource _audioSource;
    private Button _button;

    private void OnDisable()
    {
        _button.onClick.RemoveListener(PlayClickSound);
    }

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
        _button = GetComponent<Button>();
        _button.onClick.AddListener(PlayClickSound);
    }
    private void PlayClickSound()
    {
        _audioSource.clip = _clickClip;
        _audioSource.Play();
    }
}

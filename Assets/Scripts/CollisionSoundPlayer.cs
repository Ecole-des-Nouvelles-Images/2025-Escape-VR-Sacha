using System;
using Sound;
using UnityEngine;

[RequireComponent(typeof(AudioClipRandomPlayer))]
[RequireComponent(typeof(Collider))]
public class CollisionSoundPlayer : MonoBehaviour
{
    private AudioClipRandomPlayer _audioClipRandomPlayer;

    private void Awake()
    {
        _audioClipRandomPlayer = GetComponent<AudioClipRandomPlayer>();
    }

    private void OnCollisionEnter(Collision other)
    {
        _audioClipRandomPlayer.PlayClip();
    }
}

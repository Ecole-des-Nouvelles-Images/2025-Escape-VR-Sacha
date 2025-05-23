using UnityEngine;

namespace Sound
{
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
}

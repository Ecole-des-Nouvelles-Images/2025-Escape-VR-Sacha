using UnityEngine;

namespace Data
{
    [CreateAssetMenu(fileName = "AudioClipList", menuName = "Scriptable Objects/AudioClipList")]
    public class AudioClipList : ScriptableObject
    {
        public bool IsLoopingSounds;
        public bool Is3DSounds;
        public AudioClip[] AudioClips;
    }
}

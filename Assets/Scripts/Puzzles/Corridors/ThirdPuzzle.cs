using Manager;
using UnityEngine;
using Utils;

namespace Puzzles.Corridors
{
    public class ThirdPuzzle : Puzzle
    {
        [SerializeField] private string _unlockID;
        [SerializeField] private AudioSource _successAudioSource;

        private void OnEnable()
        {
            GameEvents.OnKeyboardUnlock += Unlock;
        }

        private void OnDisable()
        {
            GameEvents.OnKeyboardUnlock -= Unlock;
        }

        private void Unlock(string keyboardUnlockID)
        {
            if(keyboardUnlockID == _unlockID)
            {
                _successAudioSource.Play();
                UnlockPortal();
            }
        }
    }
}

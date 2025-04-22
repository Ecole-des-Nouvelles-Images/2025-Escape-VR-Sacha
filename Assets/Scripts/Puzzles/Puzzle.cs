using UnityEngine;
using Utils;

namespace Puzzles
{
    public abstract class Puzzle : MonoBehaviour
    {
        [SerializeField] private string _portalsToUnlockID;

        protected void UnlockPortal()
        {
            GameEvents.OnPuzzleCompleted.Invoke(_portalsToUnlockID);
        }
        protected void LockPortal()
        {
            GameEvents.OnSetupPuzzle.Invoke(_portalsToUnlockID);
        }
    }
}

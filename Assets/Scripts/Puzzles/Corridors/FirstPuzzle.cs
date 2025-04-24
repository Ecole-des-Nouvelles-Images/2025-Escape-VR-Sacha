using UnityEngine;
using UnityEngine.Serialization;

namespace Puzzles.Corridors
{
    public class FirstPuzzle : Puzzle
    {
        [FormerlySerializedAs("_unlockDetector")] [SerializeField] private ObjectDetectorByTag _unlockDetectorByTag;
        [FormerlySerializedAs("_lockDetector")] [SerializeField] private ObjectDetectorByTag _lockDetectorByTag;

        private void Update()
        {
            if(_lockDetectorByTag.ObjectDetected)
                LockPortal();
            if(_unlockDetectorByTag.ObjectDetected)
                UnlockPortal();
        }
    }
}
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
            if(_lockDetectorByTag.CamDetected)
                LockPortal();
            if(_unlockDetectorByTag.CamDetected)
                UnlockPortal();
        }
    }
}
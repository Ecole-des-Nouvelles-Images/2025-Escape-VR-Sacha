using System;
using UnityEngine;

namespace PuzzleCorridor
{
    public class FirstPuzzleCorridor : PuzzleCorridor
    {
        [SerializeField] private MainCamDetector _unlockDetector;
        [SerializeField] private MainCamDetector _lockDetector;

        private void Update()
        {
            if(_lockDetector.CamDetected)
                LockPortal();
            if(_unlockDetector.CamDetected)
                UnlockPortal();
        }
    }
}
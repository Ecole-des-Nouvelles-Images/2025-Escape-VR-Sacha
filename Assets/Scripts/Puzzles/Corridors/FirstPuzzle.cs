using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace Puzzles.Corridors
{
    public class FirstPuzzle : Puzzle
    {
        [SerializeField] private ObjectDetectorByTag _unlockDetectorByTag;
        [SerializeField] private ObjectDetectorByTag _lockDetectorByTag;
        [SerializeField] private GameObject _goodPanels;
        [SerializeField] private GameObject _badPanels;

        private int _pathCounter;

        private void Awake()
        {
            _goodPanels.SetActive(false);
            _badPanels.SetActive(true);
        }

        private void Update()
        {
            if(_lockDetectorByTag.ObjectDetected)
            {
                LockPortal();
                _pathCounter++;
            }
            if(_unlockDetectorByTag.ObjectDetected)
            {
                UnlockPortal();
                _pathCounter++;
            }
            if (_pathCounter >= 6 && !_goodPanels.activeSelf)
            {
                _badPanels.SetActive(false);
                _goodPanels.SetActive(true);
            }
        }
    }
}
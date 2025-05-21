using System.Collections.Generic;
using Puzzles;
using UnityEngine;
using Utils;

namespace SalleIntro
{
    public class IntroHandler : Puzzle
    {
        [SerializeField] private string _unlockID;
        [SerializeField] private List<PedestalComponent> _portalPedestals;
        [SerializeField] private List<PedestalComponent> _cubePedestals;

        [SerializeField] private GameObject _numberCube;
        [SerializeField] private GameObject _finalDoor;

        private bool _portalTriggered;
        private bool _cubeTriggered;
        
        private void OnEnable()
        {
            GameEvents.OnKeyboardUnlock += Unlock;
        }

        private void OnDisable()
        {
            GameEvents.OnKeyboardUnlock -= Unlock;
        }
        
        private void Start()
        {
            LockPortal();
            _finalDoor.SetActive(false);
        }

        public void CheckPedestalGroups()
        {
            if (!_portalTriggered && AreAllCorrectlyOccupied(_portalPedestals))
            {
                Debug.Log("Portal triggered");
                UnlockPortal();
                _portalTriggered = true;
            }

            if (!_cubeTriggered && AreAllCorrectlyOccupied(_cubePedestals))
            {
                Debug.Log("Cube triggered");
                _numberCube.SetActive(true);
                _cubeTriggered = true;
            }
        }

        private bool AreAllCorrectlyOccupied(List<PedestalComponent> pedestals)
        {
            foreach (var pedestal in pedestals)
            {
                if (!pedestal.IsCorrectlyOccupied)
                    return false;
            }
            return true;
        }
        
        private void Unlock(string keyboardUnlockID)
        {
            if(keyboardUnlockID == _unlockID)
                Ending();
        }

        private void Ending()
        {
            GameEvents.OnEndGame.Invoke();
        }
    }
}
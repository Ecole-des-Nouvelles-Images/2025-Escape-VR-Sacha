using System.Collections.Generic;
using Puzzles;
using UnityEngine;

namespace SalleIntro
{
    public class IntroHandler : Puzzle
    {
        [SerializeField] private List<PedestalComponent> _portalPedestals;
        [SerializeField] private List<PedestalComponent> _cubePedestals;

        //[SerializeField] private GameObject _numberCube;
        //[SerializeField] private GameObject _finalDoor;

        private bool _portalTriggered;
        private bool _cubeTriggered;

        private void Start()
        {
            //LockPortal();
        }

        public void CheckPedestalGroups()
        {
            if (!_portalTriggered && AreAllCorrectlyOccupied(_portalPedestals))
            {
                //UnlockPortal();
                Debug.Log("Portal triggered");
                _portalTriggered = true;
            }

            if (!_cubeTriggered && AreAllCorrectlyOccupied(_cubePedestals))
            {
                //_numberCube.SetActive(true);
                Debug.Log("Cube triggered");
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
    }
}
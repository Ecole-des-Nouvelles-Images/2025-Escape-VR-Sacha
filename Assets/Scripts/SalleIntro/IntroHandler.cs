using System.Collections;
using System.Collections.Generic;
using Puzzles;
using Sound;
using UnityEngine;
using UnityEngine.SceneManagement;
using Utils;

namespace SalleIntro
{
    public class IntroHandler : Puzzle
    {
        [Header("Puzzle Objects")]
        [SerializeField] private AudioSource _audioSource;
        [SerializeField] private string _unlockID;
        [SerializeField] private List<PedestalComponent> _portalPedestals;
        [SerializeField] private List<PedestalComponent> _cubePedestals;
        [SerializeField] private GameObject _numberCube;

        [Header("Tutorial Objects")]
        [SerializeField] private GameObject _tileTutorialObject;
        [SerializeField] private GameObject _pedestalObjectsGroup;

        private bool _portalTriggered;
        private bool _cubeTriggered;
        private bool _movementDone;

        [SerializeField] private DialogManager _dialogManager;

        private void OnEnable()
        {
            GameEvents.OnKeyboardUnlock += Unlock;
            SnapComponent.OnAnySnapped += OnSnapChanged;
            SnapComponent.OnAnyUnsnapped += OnSnapChanged;
        }

        private void OnDisable()
        {
            GameEvents.OnKeyboardUnlock -= Unlock;
            SnapComponent.OnAnySnapped -= OnSnapChanged;
            SnapComponent.OnAnyUnsnapped -= OnSnapChanged;
        }

        private void OnSnapChanged(SnapComponent snap)
        {
            CheckPedestalGroups();
        }

        private void Start()
        {
            LockPortal();
            _numberCube.SetActive(false);
            _pedestalObjectsGroup.SetActive(false); // hidden until tutorial complete

            HighlightTile(true);
        }

        public void OnPlayerStepOnTile()
        {
            if (_movementDone) return;

            _movementDone = true;
            HighlightTile(false);
            CheckTutorialProgress();
        }

        private void CheckTutorialProgress()
        {
            if (_movementDone /*&& _lookDone*/)
            {
                Debug.Log("Tutorial complete, enabling pedestals.");
                _pedestalObjectsGroup.SetActive(true);
                
                _dialogManager.PlayDialogue("4",1f);
            }
        }

        private void HighlightTile(bool highlight)
        {
            var renderer = _tileTutorialObject.GetComponent<Renderer>();
            if (renderer != null)
                renderer.material.color = highlight ? Color.green : Color.white;
        }

        public void CheckPedestalGroups()
        {
            if (!_portalTriggered && AreAllCorrectlyOccupied(_portalPedestals))
            {
                Debug.Log("Portal triggered");
                UnlockPortal();
                _audioSource.Play();
                _portalTriggered = true;
                
                _dialogManager.PlayDialogue("6",1f);
            }

            if (!_cubeTriggered && AreAllCorrectlyOccupied(_cubePedestals))
            {
                Debug.Log("Cube triggered");
                _numberCube.SetActive(true);
                _cubeTriggered = true;
                _dialogManager.PlayDialogue("13");
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
            if (keyboardUnlockID == _unlockID)
                Ending();
        }

        private void Ending()
        {
            //_dialogManager.PlayDialogue("14");
            SceneManager.LoadScene("CreditsBadEnd");
            //GameEvents.OnEndGame.Invoke();
        }
    }
}

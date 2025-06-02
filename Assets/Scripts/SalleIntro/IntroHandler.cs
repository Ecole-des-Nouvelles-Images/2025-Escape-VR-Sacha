using System.Collections;
using System.Collections.Generic;
using Puzzles;
using UnityEngine;
using Utils;

namespace SalleIntro
{
    public class IntroHandler : Puzzle
    {
        [Header("Puzzle Objects")]
        [SerializeField] private string _unlockID;
        [SerializeField] private List<PedestalComponent> _portalPedestals;
        [SerializeField] private List<PedestalComponent> _cubePedestals;
        [SerializeField] private GameObject _numberCube;
        [SerializeField] private GameObject _finalDoor;

        [Header("Tutorial Objects")]
        [SerializeField] private GameObject _tileTutorialObject;
        [SerializeField] private List<ScreenComponent> _lookScreens;
        [SerializeField] private GameObject _pedestalObjectsGroup;

        private bool _portalTriggered;
        private bool _cubeTriggered;

        private bool _movementDone;
        private bool _lookDone;
        private HashSet<ScreenComponent> _screensLookedAt = new HashSet<ScreenComponent>();

        [SerializeField] private DialogManager _dialogManager;

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
            
            _dialogManager.PlayDialogue("3",1f);
        }

        public void RegisterScreenLook(ScreenComponent screen)
        {
            if (!_screensLookedAt.Contains(screen))
            {
                _screensLookedAt.Add(screen);
                if (_screensLookedAt.Count >= _lookScreens.Count)
                {
                    _lookDone = true;
                    CheckTutorialProgress();
                }
            }
        }

        private void CheckTutorialProgress()
        {
            if (_movementDone && _lookDone)
            {
                Debug.Log("Tutorial complete, enabling pedestals.");
                _pedestalObjectsGroup.SetActive(true);
                
                StartCoroutine(PlaySequence());
            }
        }
        
        private IEnumerator PlaySequence()
        {
            _dialogManager.PlayDialogue("4",1f);
            yield return new WaitWhile(() => _dialogManager.IsDialoguePlaying());

            yield return new WaitForSeconds(1f);

            _dialogManager.PlayDialogue("5", 60f);
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
            _dialogManager.PlayDialogue("14");
            GameEvents.OnEndGame.Invoke();
        }
    }
}

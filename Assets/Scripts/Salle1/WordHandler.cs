using System;
using System.Collections;
using System.Collections.Generic;
using Puzzles;
using UnityEngine;
using Utils;

namespace Salle1
{
    public class WordHandler : Puzzle
    {
        [Serializable]
        private class WordEvent
        {
            public string TargetWord;
            public Action OnWordCorrect;
        }

        [SerializeField] private SnapComponent[] _pictureObjects;
        [SerializeField] private List<EmplacementComponent> _emplacements;

        [SerializeField] private GameObject _drawer;
        [SerializeField] private GameObject _closedTeddyBear;
        [SerializeField] private GameObject _openTeddyBear;
        [SerializeField] private GameObject _suitcase;
        [SerializeField] private GameObject _chestTop;

        [SerializeField] private List<GameObject> _drawerObjects;
        [SerializeField] private List<GameObject> _teddyObjects;
        [SerializeField] private List<GameObject> _suitcaseObjects;
        [SerializeField] private List<GameObject> _chestObjects;

        [SerializeField] private AudioSource _audioSource;

        private bool _puzzleCompleted = false;
        private int _lastStepReached = 0;
        private bool _isCheckingWord = false;

        private Dictionary<string, Action> _wordActions = new Dictionary<string, Action>();
        private HashSet<string> _completedWords = new HashSet<string>();

        private bool _drawerCheck;
        private bool _teddyBearCheck;
        private bool _suitcaseCheck;
        private bool _chestTopCheck;

        private void Start()
        {
            // Setup objets initiaux
            _closedTeddyBear.SetActive(true);
            _openTeddyBear.SetActive(false);

            _drawerObjects.ForEach(obj => obj.SetActive(false));
            _teddyObjects.ForEach(obj => obj.SetActive(false));
            _suitcaseObjects.ForEach(obj => obj.SetActive(false));
            _chestObjects.ForEach(obj => obj.SetActive(false));

            // Dictionnaire mots => actions
            _wordActions.Add("TIROIR", OnOpenDrawer);
            _wordActions.Add("PORTE", UnlockPortal);
            _wordActions.Add("OURS", OnOpenTeddy);
            _wordActions.Add("VALISE", OnOpenSuitCase);
            _wordActions.Add("BOITE", OnUnlockFinalChest);

            LockPortal();
        }

        private void OnEnable()
        {
            SnapComponent.OnAnySnapped += OnSnapChanged;
            SnapComponent.OnAnyUnsnapped += OnSnapChanged;
        }

        private void OnDisable()
        {
            SnapComponent.OnAnySnapped -= OnSnapChanged;
            SnapComponent.OnAnyUnsnapped -= OnSnapChanged;
        }

        private void OnSnapChanged(SnapComponent obj)
        {
            int snappedCount = CountSnappedObjects();

            if (snappedCount > _lastStepReached)
            {
                _lastStepReached = snappedCount;

                if (_lastStepReached == 3)
                    GameEvents.OnIncreaseScore.Invoke();
            }

            //Intégré la condition de vrai fin ici ?
            
            /*if (!_puzzleCompleted && snappedCount == _pictureObjects.Length)
            {
                UnlockPortal();
                _audioSource?.Play();
                _puzzleCompleted = true;
                enabled = false;
            }*/
        }

        private int CountSnappedObjects()
        {
            int count = 0;
            foreach (var obj in _pictureObjects)
            {
                if (obj.IsSnapped) count++;
            }
            return count;
        }

        public void CheckCurrentWord()
        {
            if (_isCheckingWord)
                return;

            _isCheckingWord = true;

            string sequence = "";
            foreach (var emplacement in _emplacements)
            {
                sequence += emplacement.CurrentLetter != null ? emplacement.CurrentLetter.LetterValue[0].ToString() : "-";
            }

            Debug.Log($"Current sequence: {sequence}");

            foreach (var kvp in _wordActions)
            {
                string targetWord = kvp.Key;

                int index = sequence.IndexOf(targetWord);
                if (index >= 0)
                {
                    kvp.Value?.Invoke();
                }
            }

            _isCheckingWord = false;
        }



        // ----- Actions spécifiques -----
        private void OnOpenDrawer()
        {
            if (_drawerCheck) return;

            Debug.Log("Le tiroir s'ouvre !");
            StartCoroutine(MoveOverTime(_drawer.transform, _drawer.transform.position, _drawer.transform.position + Vector3.back * 0.3f, 1f));
            _drawerObjects.ForEach(obj => obj.SetActive(true));
            _drawerCheck = true;
        }

        private void OnOpenTeddy()
        {
            if (_teddyBearCheck) return;

            Debug.Log("Le nounours s'ouvre !");
            _closedTeddyBear.SetActive(false);
            _openTeddyBear.SetActive(true);
            _teddyObjects.ForEach(obj => obj.SetActive(true));
            _teddyBearCheck = true;
        }

        private void OnOpenSuitCase()
        {
            if (_suitcaseCheck) return;

            Quaternion startRotation = _suitcase.transform.rotation;
            Quaternion endRotation = startRotation * Quaternion.Euler(0f, 30f, 0f);
            StartCoroutine(RotateOverTime(_suitcase.transform, startRotation, endRotation, 1f));
            _suitcaseObjects.ForEach(obj => obj.SetActive(true));
            _suitcaseCheck = true;
        }

        private void OnUnlockFinalChest()
        {
            if (_chestTopCheck) return;

            Quaternion startRotation = _chestTop.transform.rotation;
            Quaternion endRotation = startRotation * Quaternion.Euler(70f, 0, 0f);
            StartCoroutine(RotateOverTime(_chestTop.transform, startRotation, endRotation, 1f));
            _chestObjects.ForEach(obj => obj.SetActive(true));
            _chestTopCheck = true;
        }

        // ----- Coroutines utilitaires -----
        private IEnumerator MoveOverTime(Transform obj, Vector3 from, Vector3 to, float duration)
        {
            float elapsed = 0f;
            while (elapsed < duration)
            {
                obj.position = Vector3.Lerp(from, to, elapsed / duration);
                elapsed += Time.deltaTime;
                yield return null;
            }
            obj.position = to;
        }

        private IEnumerator RotateOverTime(Transform target, Quaternion start, Quaternion end, float duration)
        {
            float elapsed = 0f;
            while (elapsed < duration)
            {
                target.rotation = Quaternion.Slerp(start, end, elapsed / duration);
                elapsed += Time.deltaTime;
                yield return null;
            }
            target.rotation = end;
        }
    }
}

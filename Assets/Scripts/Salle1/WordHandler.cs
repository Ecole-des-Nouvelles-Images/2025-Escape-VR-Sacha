using System;
using System.Collections;
using System.Collections.Generic;
using Puzzles;
using UnityEngine;
using Utils;

namespace Salle1 {
    public class WordHandler : Puzzle
    {

        [Serializable]
        private class WordEvent
        {
            public string TargetWord;
            public Action OnWordCorrect;
        }

        [SerializeField] private SnapComponent[] _pictureObjects;
        private bool _puzzleCompleted = false;
        private int _lastStepReached = 0;
        
        [SerializeField] private List<EmplacementComponent> _emplacements;

        [SerializeField] private GameObject _drawer;
        [SerializeField] private GameObject _closedTeddyBear;
        [SerializeField] private GameObject _openTeddyBear;
        [SerializeField] private GameObject _suitcase;
        [SerializeField] private GameObject _chestTop;
        
        private bool _drawerCheck;
        private bool _teddyBearCheck;
        private bool _suitcaseCheck;
        private bool _chestTopCheck;
        
        [SerializeField] private List<GameObject> _drawerObjects;
        [SerializeField] private List<GameObject> _teddyObjects;
        [SerializeField] private List<GameObject> _suitcaseObjects;
        [SerializeField] private List<GameObject> _chestObjects;

        private Dictionary<string, Action> _wordActions = new Dictionary<string, Action>();

        private bool _isCheckingWord = false;

        private void Start()
        {
            _closedTeddyBear.SetActive(true);
            _openTeddyBear.SetActive(false);
            
            _drawerObjects.ForEach(obj => obj.SetActive(false));
            _teddyObjects.ForEach(obj => obj.SetActive(false));
            _suitcaseObjects.ForEach(obj => obj.SetActive(false));
            _chestObjects.ForEach(obj => obj.SetActive(false));
            
            _wordActions.Add("TIROIR", OnOpenDrawer);
            _wordActions.Add("PORTE", UnlockPortal);
            _wordActions.Add("OURS", OnOpenTeddy);
            _wordActions.Add("VALISE", OnOpenSuitCase);
            _wordActions.Add("BOITE", OnUnlockFinalChest);

            LockPortal();
        }
        
        private void Update() {
            int snappedCount = CountSnappedObjects();

            if (snappedCount > _lastStepReached) {
                _lastStepReached = snappedCount;

                switch (_lastStepReached) {
                    case 3:
                        Debug.Log("aaaa");
                        GameEvents.OnIncreaseScore.Invoke();
                        break;
                }
            }

            if (!_puzzleCompleted && snappedCount == _pictureObjects.Length) {
                UnlockPortal();
                _puzzleCompleted = true;
                enabled = false;
            }
        }

        private int CountSnappedObjects() {
            int count = 0;
            foreach (var obj in _pictureObjects) {
                if (obj.IsSnapped)
                    count++;
            }
            return count;
        }

        public List<Transform> GetEmplacements()
        {
            List<Transform> transforms = new List<Transform>();
            foreach (var emp in _emplacements)
            {
                if (emp != null)
                    transforms.Add(emp.transform);
            }

            return transforms;
        }

        public void CheckCurrentWord()
        {
            if (_isCheckingWord)
                return;

            _isCheckingWord = true;

            string currentWord = "";

            foreach (var emplacement in _emplacements)
            {
                if (emplacement.TryGetComponent(out EmplacementComponent e) && e.CurrentLetter != null)
                    currentWord += e.CurrentLetter.LetterValue[0];
            }

            Debug.Log($"Current Word: {currentWord}");

            if (_wordActions.TryGetValue(currentWord, out Action action))
            {
                action?.Invoke();
                foreach (var e in _emplacements)
                {
                    if (e.TryGetComponent(out EmplacementComponent emp))
                        emp.ClearIfOccupant(null);
                }
            }

            _isCheckingWord = false;
        }

        private void OnOpenDrawer()
        {
            if (_drawerCheck != true)
            {
                Debug.Log("Le tiroir s'ouvre !");
                StartCoroutine(MoveOverTime(
                    _drawer.transform,
                    _drawer.transform.position,
                    _drawer.transform.position + Vector3.back * 0.3f,
                    1f
                ));
                _drawerObjects.ForEach(obj => obj.SetActive(true));
                _drawerCheck = true;
            }
        }
        
        private void OnOpenTeddy()
        {
            if (_teddyBearCheck != true)
            {
                Debug.Log("Le nounours s'ouvre !");
                _closedTeddyBear.SetActive(false);
                _openTeddyBear.SetActive(true);
                _teddyObjects.ForEach(obj => obj.SetActive(true));
                _teddyBearCheck = true;
            }
            
        }

        private void OnOpenSuitCase()
        {
            if (_suitcaseCheck != true)
            {
                Quaternion startRotation = _suitcase.transform.rotation;
                Quaternion endRotation = startRotation * Quaternion.Euler(0f, 30f, 0f);

                StartCoroutine(RotateOverTime(
                    _suitcase.transform,
                    startRotation,
                    endRotation,
                    1f
                ));

                _suitcaseObjects.ForEach(obj => obj.SetActive(true));
                _suitcaseCheck = true;
            }
        }

        private void OnUnlockFinalChest()
        {
            if (_chestTopCheck != true)
            {
                Quaternion startRotation = _chestTop.transform.rotation;
                Quaternion endRotation = startRotation * Quaternion.Euler(70f, 0, 0f);

                StartCoroutine(RotateOverTime(
                    _chestTop.transform,
                    startRotation,
                    endRotation,
                    1f
                ));
                
                Vector3 moveTarget = _chestTop.transform.position + Vector3.up * 0.1f + Vector3.right * 0.15f;
                
                StartCoroutine(MoveOverTime(
                    _chestTop.transform,
                    _chestTop.transform.position,
                    _chestTop.transform.position + Vector3.up * 0.1f + Vector3.right * 0.15f + Vector3.forward * 0.01f,
                    1f
                ));
                
                _chestObjects.ForEach(obj => obj.SetActive(true));
                _chestTopCheck = true;
            }
            
        }
        
        private IEnumerator MoveOverTime(Transform obj, Vector3 from, Vector3 to, float duration) {
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

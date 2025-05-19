using System;
using System.Collections;
using System.Collections.Generic;
using Puzzles;
using UnityEngine;

namespace Salle1 {
    public class WordHandler : Puzzle
    {

        [Serializable]
        private class WordEvent
        {
            public string TargetWord;
            public Action OnWordCorrect;
        }

        [SerializeField] private List<EmplacementComponent> _emplacements;

        [SerializeField] private GameObject _drawer;
        [SerializeField] private GameObject _closedTeddyBear;
        [SerializeField] private GameObject _openTeddyBear;
        [SerializeField] private GameObject _suitcase;
        [SerializeField] private GameObject _chestTop;
        
        
        //[SerializeField] private List<GameObject> _teddyObjects;
        [SerializeField] private List<GameObject> _drawerObjects;
        [SerializeField] private List<GameObject> _suitcaseObjects;

        private Dictionary<string, Action> _wordActions = new Dictionary<string, Action>();

        private bool _isCheckingWord = false;

        private void Start()
        {
            _closedTeddyBear.SetActive(false);
            
            _drawerObjects.ForEach(obj => obj.SetActive(false));
            _suitcaseObjects.ForEach(obj => obj.SetActive(false));
            
            _wordActions.Add("OUVRE", OnOpenDrawer);
            _wordActions.Add("PORTE", OnOpenDoor);
            _wordActions.Add("OURS", OnOpenTeddy);
            _wordActions.Add("VALISE", OnOpenSuitCase);
            _wordActions.Add("SOUVENIR", OnUnlockFinalChest);

            LockPortal();
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
            Debug.Log("Le tiroir s'ouvre !");
            StartCoroutine(MoveOverTime(
                _drawer.transform,
                _drawer.transform.position,
                _drawer.transform.position + Vector3.right * 0.5f,
                1f
            ));
            _drawerObjects.ForEach(obj => obj.SetActive(true));
        }

        private void OnOpenDoor()
        {
            UnlockPortal();
            Debug.Log("La porte s'ouvre !");
        }
        private void OnOpenTeddy()
        {
            _openTeddyBear.SetActive(false);
            _closedTeddyBear.SetActive(true);
            Debug.Log("Le nounours s'ouvre !");
        }

        private void OnOpenSuitCase()
        {
            StartCoroutine(MoveOverTime(
                _suitcase.transform,
                _suitcase.transform.position,
                _suitcase.transform.position + Vector3.right * 0.5f,
                1f
            ));
            _suitcaseObjects.ForEach(obj => obj.SetActive(true));
        }
        private void OnUnlockFinalChest()
        {
            StartCoroutine(MoveOverTime(
                _chestTop.transform,
                _chestTop.transform.position,
                _chestTop.transform.position + Vector3.right * 0.5f,
                1f
            ));
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
    }
}

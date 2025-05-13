using System;
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

        [SerializeField] private GameObject _closedTeddyBear;
        [SerializeField] private GameObject _openTeddyBear;
        [SerializeField] private GameObject _suitcase;
        [SerializeField] private GameObject _chest;

        private Dictionary<string, Action> _wordActions = new Dictionary<string, Action>();

        private bool _isCheckingWord = false;

        private void Start()
        {
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
                return; // Prevent recursive calls

            _isCheckingWord = true; // Set the flag to prevent re-entry

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
        }

        private void OnOpenDoor() => Debug.Log("La trappe magique apparaît !");
        private void OnOpenTeddy()
        {
            _closedTeddyBear.SetActive(false);
            _closedTeddyBear.SetActive(true);
            Debug.Log("Le nounours s'ouvre !");
        }
        
        private void OnOpenSuitCase() {}
        private void OnUnlockFinalChest() => Debug.Log("Le coffre final est déverrouillé !");
    }
}

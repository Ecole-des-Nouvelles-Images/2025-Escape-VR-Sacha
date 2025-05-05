using System;
using System.Collections.Generic;
using Puzzles;
using UnityEngine;
using UnityEngine.Serialization;

namespace Salle1 {
    public class WordHandler : Puzzle {

        [Serializable]
        private class WordEvent {
            [FormerlySerializedAs("targetWord")] public string TargetWord;
            public Action OnWordCorrect;
        }

        [SerializeField] private List<Transform> _emplacements;

        private Dictionary<Transform, char> _selectedLetters = new Dictionary<Transform, char>();
        private Dictionary<string, Action> _wordActions = new Dictionary<string, Action>();
        
        public List<Transform> GetEmplacements() => _emplacements;

        private void Start() {
            _wordActions.Add("OUVRE", OnOpenDrawer);
            _wordActions.Add("PORTE", OnOpenTrap);
            _wordActions.Add("OURS", OnOpenTeddy);
            _wordActions.Add("SOUVENIR", OnUnlockFinalChest);
            LockPortal();
        }

        public void AddLetter(LetterComponent letterComponent, Transform emplacement) {
            if (!_selectedLetters.ContainsKey(emplacement)) {
                _selectedLetters[emplacement] = letterComponent.LetterValue[0];
            }
            CheckCurrentWord();
        }

        public void RemoveLetter(Transform emplacement) {
            if (_selectedLetters.ContainsKey(emplacement)) {
                _selectedLetters.Remove(emplacement);
            }
            CheckCurrentWord();
        }

        public void CheckCurrentWord() {
            string currentWord = "";

            foreach (var emplacement in _emplacements) {
                if (emplacement.TryGetComponent(out EmplacementComponent comp) && comp.CurrentLetter != null) {
                    currentWord += comp.CurrentLetter.LetterValue[0];
                } else {
                    currentWord += "-";
                }
            }

            Debug.Log($"Current Word: {currentWord}");

            if (_wordActions.TryGetValue(currentWord, out Action action)) {
                Debug.Log($"Mot correct : {currentWord}");
                action?.Invoke();

                // Clear all emplacements
                foreach (var emplacement in _emplacements) {
                    if (emplacement.TryGetComponent(out EmplacementComponent comp)) {
                        comp.CurrentLetter = null;
                    }
                }
            }
        }


        private void ClearLettersForWord(string word) {
            foreach (var emplacement in _emplacements) {
                if (_selectedLetters.ContainsKey(emplacement) && _selectedLetters[emplacement].ToString() == word[_emplacements.IndexOf(emplacement)].ToString()) {
                    _selectedLetters.Remove(emplacement);
                }
            }
        }

        private void OnOpenDrawer() {
            Debug.Log("Le tiroir s'ouvre !");
            UnlockPortal();
        }

        private void OnOpenTrap() => Debug.Log("La trappe magique apparaît !");
        private void OnOpenTeddy() => Debug.Log("Le nounours s'ouvre !");
        private void OnUnlockFinalChest() => Debug.Log("Le coffre final est déverrouillé !");
    }
}

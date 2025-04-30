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

        private void Start() {
            _wordActions.Add("OUVRE", OnOpenDrawer);
            _wordActions.Add("PORTE", OnOpenTrap);
            _wordActions.Add("OURS", OnOpenTeddy);
            _wordActions.Add("SOUVENIR", OnUnlockFinalChest);
            //LockPortal();
        }

        public void AddLetter(LetterComponent letterComponent, Transform emplacement) {
            if (!_selectedLetters.ContainsKey(emplacement)) {
                _selectedLetters[emplacement] = letterComponent.LetterValue[0];
            }
            CheckWord();
        }

        public void RemoveLetter(Transform emplacement) {
            if (_selectedLetters.ContainsKey(emplacement)) {
                _selectedLetters.Remove(emplacement);
            }
            CheckWord();
        }

        private void CheckWord() {
            string currentWord = "";

            for (int i = 0; i < _emplacements.Count; i++) {
                Transform emplacement = _emplacements[i];
                if (_selectedLetters.TryGetValue(emplacement, out var c)) {
                    currentWord += c;
                }
            }

            if (string.IsNullOrEmpty(currentWord)) return;

            if (_wordActions.TryGetValue(currentWord, out Action action)) {
                Debug.Log($"Mot correct : {currentWord}");
                action?.Invoke();
                _selectedLetters.Clear();
            } else {
                Debug.Log($"Mot incorrect : {currentWord}");
            }
        }

        private void OnOpenDrawer() => Debug.Log("Le tiroir s'ouvre !");
        private void OnOpenTrap() => Debug.Log("La trappe magique apparaît !");
        private void OnOpenTeddy() => Debug.Log("Le nounours s'ouvre !");
        private void OnUnlockFinalChest() => Debug.Log("Le coffre final est déverrouillé !");
    }
}

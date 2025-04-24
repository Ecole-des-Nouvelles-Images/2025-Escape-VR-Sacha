using System.Collections.Generic;
using Puzzles;
using UnityEngine;

namespace Salle1 {
    public class WordHandler : Puzzle {

        [SerializeField] private string _targetWord = "BLUE";
        private Dictionary<Transform, char> _selectedLetters = new Dictionary<Transform, char>();
        //public UnityEvent OnWordCorrect;

        [SerializeField] private List<Transform> _emplacements;
        private List<Transform> _occupiedEmplacements = new List<Transform>();

        public void SnapToEmplacement(Transform obj, Transform emplacement) {
            if (_occupiedEmplacements.Contains(emplacement)) {
                return;
            }
            obj.position = emplacement.position;
            obj.rotation = emplacement.rotation;
            _occupiedEmplacements.Add(emplacement);
        }

        public void UnsnapFromEmplacement(Transform obj) {
            foreach (Transform emplacement in _occupiedEmplacements) {
                if (Vector3.Distance(obj.position, emplacement.position) < 0.1f) {
                    _occupiedEmplacements.Remove(emplacement);
                    break;
                }
            }
        }

        public void AddLetter(LettersComponent letterComponent, Transform emplacement) {
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
                if (_selectedLetters.ContainsKey(emplacement)) {
                    currentWord += _selectedLetters[emplacement];
                }
            }

            if (currentWord == _targetWord) {
                Debug.Log("Mot correct !");
                //OnWordCorrect?.Invoke();
                _selectedLetters.Clear();
                _occupiedEmplacements.Clear();
            } else {
                Debug.Log("Mot incorrect: " + currentWord);
            }
        }
        
    }
}

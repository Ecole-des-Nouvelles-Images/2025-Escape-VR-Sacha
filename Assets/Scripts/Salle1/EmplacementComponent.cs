using UnityEngine;

namespace Salle1 {
    public class EmplacementComponent : MonoBehaviour {
        [SerializeField] private WordHandler _wordHandler;

        public LetterComponent CurrentLetter { get; set; }
        private SnapComponent _currentOccupant;
        private bool _justSnapped = false;

        private void OnTriggerEnter(Collider other) {
            if (!other.TryGetComponent(out LetterComponent letter)) return;
            if (!other.TryGetComponent(out SnapComponent snap)) return;

            snap.SnappedCallback += OnLetterSnapped;
        }

        private void OnTriggerExit(Collider other) {
            if (!other.TryGetComponent(out SnapComponent snap)) return;
            if (_currentOccupant != snap) return;

            if (_justSnapped) return;

            ClearLetter();
        }

        private void OnLetterSnapped(SnapComponent snappedObj) {
            if (!snappedObj.TryGetComponent(out LetterComponent letter)) return;

            CurrentLetter = letter;
            _currentOccupant = snappedObj;

            _wordHandler.CheckCurrentWord();

            _justSnapped = true;
            Invoke(nameof(ResetSnapState), 0.1f);
        }

        private void ResetSnapState() {
            _justSnapped = false;
        }

        private void ClearLetter() {
            CurrentLetter = null;
            _currentOccupant = null;
            _wordHandler.CheckCurrentWord();
        }
    }
}
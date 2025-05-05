using UnityEngine;

namespace Salle1 {
    public class EmplacementComponent : MonoBehaviour 
    {
        [SerializeField] private WordHandler _wordHandler;

        private void OnTriggerEnter(Collider other) {
            if (!other.TryGetComponent(out LetterComponent letter)) return;
            if (!other.TryGetComponent(out SnapComponent snap)) return;

            snap.SnappedCallback += OnLetterSnapped;
        }

        private void OnTriggerExit(Collider other) {
            if (!other.TryGetComponent(out LetterComponent letter)) return;
            if (!other.TryGetComponent(out SnapComponent snap)) return;

            snap.SnappedCallback -= OnLetterSnapped;
            _wordHandler.RemoveLetter(transform);
        }

        private void OnLetterSnapped(SnapComponent snappedObj) {
            if (!snappedObj.TryGetComponent(out LetterComponent letter)) return;
            _wordHandler.AddLetter(letter, transform);
        }
    }
}
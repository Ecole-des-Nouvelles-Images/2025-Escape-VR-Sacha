using UnityEngine;

namespace Salle1 {
    public class EmplacementComponent : MonoBehaviour {

        [SerializeField] private WordHandler _wordHandler;
        [SerializeField] private Transform _currentObject;
        private Rigidbody _objRigidbody;

        private RigidbodyConstraints _originalConstraints;
        private bool _hadGravity;

        private void OnTriggerEnter(Collider other) {
            if (other.GetComponent<LettersComponent>() != null && _currentObject == null) {
                LettersComponent letterComponent = other.GetComponent<LettersComponent>();
                _objRigidbody = other.GetComponent<Rigidbody>();

                if (_objRigidbody == null) return;

                _originalConstraints = _objRigidbody.constraints;
                _hadGravity = _objRigidbody.useGravity;

                _objRigidbody.constraints = RigidbodyConstraints.FreezeAll;
                _objRigidbody.useGravity = false;

                _currentObject = other.transform;
                _wordHandler.SnapToEmplacement(_currentObject, transform);
                _wordHandler.AddLetter(letterComponent, transform);
            }
        }

        private void OnTriggerExit(Collider other) {
            if (other.GetComponent<LettersComponent>() != null && _currentObject == other.transform) {

                if (_objRigidbody != null) {
                    _objRigidbody.constraints = _originalConstraints;
                    _objRigidbody.useGravity = _hadGravity;
                }

                _wordHandler.UnsnapFromEmplacement(_currentObject);
                _wordHandler.RemoveLetter(transform);
                _currentObject = null;
            }
        }
    }
}

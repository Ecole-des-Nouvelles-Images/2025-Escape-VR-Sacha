using UnityEngine;

namespace ProtoElias.Salle1 {
    public class EmplacementComponent : MonoBehaviour {

        [SerializeField] private WordHandler _wordHandler;
        [SerializeField] private Transform _currentObject;
        private Rigidbody _objRigidbody;

        private void OnTriggerEnter(Collider other) {
            if (other.GetComponent<LettersComponent>() != null && _currentObject == null) {
                LettersComponent letterComponent = other.GetComponent<LettersComponent>();
                _objRigidbody = other.GetComponent<Rigidbody>();
                _objRigidbody.constraints = RigidbodyConstraints.FreezeAll;
                _currentObject = other.transform;
                _wordHandler.SnapToEmplacement(_currentObject, transform);
                _wordHandler.AddLetter(letterComponent, transform);
            }
        }

        private void OnTriggerExit(Collider other) {
            if (other.GetComponent<LettersComponent>() != null && _currentObject == other.transform) {
                _objRigidbody.constraints = RigidbodyConstraints.None;
                _wordHandler.UnsnapFromEmplacement(_currentObject);
                _wordHandler.RemoveLetter(transform);
                _currentObject = null;
            }
        }
    }
}
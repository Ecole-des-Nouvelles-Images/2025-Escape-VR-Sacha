using UnityEngine;

namespace Salle3 {
    public class OfficeObject : MonoBehaviour {

        //public string ObjectName;
        public bool IsCorrectlyPlaced = false;

        [Header("Cible de placement")]
        [SerializeField] private GameObject _target;

        [Header("Tolérances")]
        [SerializeField] private float _positionTolerance = 0.1f;
        [SerializeField] private float _rotationTolerance = 5f;

        private bool _alreadySnapped = false;
        
        private void Update() {
            if (_target == null) return;

            CheckPlacement();
        }
        
        private void CheckPlacement() {
            bool positionCheck = Vector3.Distance(transform.localPosition, _target.transform.localPosition) <= _positionTolerance;
            bool rotationCheck = Quaternion.Angle(transform.localRotation, _target.transform.localRotation) <= _rotationTolerance;

            IsCorrectlyPlaced = positionCheck && rotationCheck;

            if (IsCorrectlyPlaced && !_alreadySnapped) {
                Debug.Log("yes");
                SnapToTarget();
                _alreadySnapped = true;
            }
        }
        
        private void SnapToTarget() {
            transform.localPosition = _target.transform.localPosition;
            transform.localRotation = _target.transform.localRotation;

        }
        
        private void OnDrawGizmos() {
            if (_target != null) {
                Gizmos.color = Color.cyan;
                Gizmos.DrawWireSphere(_target.transform.position, 0.05f);
                Gizmos.DrawLine(_target.transform.position, _target.transform.position + _target.transform.forward * 0.2f);
            }
        }
    }
}

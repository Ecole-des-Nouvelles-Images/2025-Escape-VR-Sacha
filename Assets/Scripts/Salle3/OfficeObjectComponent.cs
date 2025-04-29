using UnityEngine;

namespace Salle3 {
    public class OfficeObject : MonoBehaviour {
        
        //public string ObjectName;
        public bool IsCorrectlyPlaced = false;

        [SerializeField] private GameObject _target;
        private float _positionTolerance = 0.25f;
        private bool _alreadySnapped = false;
        
        private void Update() {
            if (_target == null) return;

            CheckPlacement();
        }
        
        private void CheckPlacement() {
            bool positionCheck = Vector3.Distance(transform.localPosition, _target.transform.localPosition) 
                                 <= _positionTolerance;

            IsCorrectlyPlaced = positionCheck;

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
    }
}

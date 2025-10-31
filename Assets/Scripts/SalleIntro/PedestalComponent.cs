using UnityEngine;

namespace SalleIntro
{
    public class PedestalComponent : MonoBehaviour
    {
        [SerializeField] private GameObject _targetObject;
        [SerializeField] private IntroHandler _introHandler;

        private SnapComponent _currentSnap;

        private void Awake()
        {
            if (_targetObject != null)
                _currentSnap = _targetObject.GetComponent<SnapComponent>();
        }

        public bool IsCorrectlyOccupied => 
            _currentSnap != null &&
            _currentSnap.IsSnapped &&
            Vector3.Distance(_currentSnap.transform.position, transform.position) < 0.01f;
    }
}
using UnityEngine;

namespace SalleIntro
{
    public class PedestalComponent : MonoBehaviour
    {
        public bool IsCorrectlyOccupied => _activeObject == _targetObject;

        [SerializeField] private GameObject _targetObject;
        [SerializeField] private IntroHandler _introHandler;

        private GameObject _activeObject;

        private void OnTriggerEnter(Collider other)
        {
            if (_activeObject == null && IsValidObject(other))
            {
                _activeObject = other.gameObject;
                _introHandler.CheckPedestalGroups();
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.gameObject == _activeObject)
            {
                _activeObject = null;
                _introHandler.CheckPedestalGroups();
            }
        }

        private bool IsValidObject(Collider other)
        {
            return other.CompareTag("SnapObject");
        }
    }
}
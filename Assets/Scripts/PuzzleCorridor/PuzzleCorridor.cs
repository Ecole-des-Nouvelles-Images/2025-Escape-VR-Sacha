using UnityEngine;

namespace PuzzleCorridor
{
    public abstract class PuzzleCorridor : MonoBehaviour
    {
        [SerializeField] private GameObject _portalToUnlock;

        private void Awake()
        {
            if(_portalToUnlock.activeSelf)
                _portalToUnlock.SetActive(false);
        }

        protected void UnlockPortal()
        {
            _portalToUnlock.SetActive(true);
        }
        protected void LockPortal()
        {
            _portalToUnlock.SetActive(false);
        }
    }
}

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
            if(_portalToUnlock.activeSelf == false)
                _portalToUnlock.SetActive(true);
        }
        protected void LockPortal()
        {
            if(_portalToUnlock.activeSelf)
                _portalToUnlock.SetActive(false);
        }
    }
}

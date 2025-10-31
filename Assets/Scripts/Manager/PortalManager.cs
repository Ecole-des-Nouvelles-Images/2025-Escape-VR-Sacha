using System;
using Props.Portal;
using UnityEngine;
using Utils;

namespace Manager
{
    public class PortalManager : MonoBehaviour
    {
        [SerializeField] private PortalEntry[] _portalsEntry;
        
        private Animator _portalAnimator;
        
        private void OnEnable()
        {
            GameEvents.OnPuzzleCompleted += PortalEnabled;
            GameEvents.OnSetupPuzzle += PortalDisabled;
        }

        private void OnDisable()
        {
            GameEvents.OnPuzzleCompleted -= PortalEnabled;
            GameEvents.OnSetupPuzzle -= PortalDisabled;
        }

        private void OnDestroy()
        {
            GameEvents.OnPuzzleCompleted -= PortalEnabled;
            GameEvents.OnSetupPuzzle -= PortalDisabled;
        }

        private void PortalEnabled(string portalID)
        {
            foreach (PortalEntry portalScript in _portalsEntry)
            {
                if (portalScript.portalID == portalID && portalScript.gameObject.activeSelf == false)
                {
                    portalScript.gameObject.SetActive(true);
                }
            }
        }
        
        private void PortalDisabled(string portalID)
        {
            foreach (PortalEntry portalScript in _portalsEntry)
            {
                if (portalScript.portalID == portalID && portalScript.gameObject.activeSelf)
                {
                    portalScript.gameObject.SetActive(false);
                }
            }
        }
    }
}
using Props.PortalV2;
using UnityEngine;
using Utils;

namespace Manager
{
    public class PortalManager : MonoBehaviour
    {
        [SerializeField] private PortalV2[] _portalsEntry;
        
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
        
        private void PortalEnabled(string portalID)
        {
            foreach (PortalV2 portalScript in _portalsEntry)
            {
                if (portalScript.portalID == portalID && portalScript.gameObject.activeSelf == false)
                {
                    Debug.Log($"Portal {portalScript.portalID} is enabled");
                    portalScript.gameObject.SetActive(true);
                }
            }
        }
        
        private void PortalDisabled(string portalID)
        {
            foreach (PortalV2 portalScript in _portalsEntry)
            {
                if (portalScript.portalID == portalID && portalScript.gameObject.activeSelf)
                {
                    Debug.Log($"Portal {portalScript.portalID} is disabled");
                    portalScript.gameObject.SetActive(false);
                }
            }
        }
    }
}
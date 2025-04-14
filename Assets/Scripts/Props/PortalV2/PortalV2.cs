using System;
using Manager;
using UnityEngine;
using UnityEngine.Serialization;
using Utils;

namespace Props.PortalV2
{
    public class PortalV2 : MonoBehaviour
    {
        [SerializeField] private GameObject _destinationPortal;
        [SerializeField] private bool _isOneUse;
        [SerializeField] private string _portalID;
        [SerializeField] private string _destinationRoomID;

        private void OnEnable()
        {
            GameEvents.OnPuzzleCompleted += PortalEnabled;
        }

        private void OnDisable()
        {
            GameEvents.OnPuzzleCompleted -= PortalEnabled;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("MainCamera"))
            {
                Debug.Log("TP");
                Teleport(other.transform.parent.transform);
            }
        }
        private void Teleport(Transform player)
        {
            GameEvents.OnRoomChanged.Invoke(_destinationRoomID);
            Vector3 offset = player.position - transform.position;
            offset += _destinationPortal.transform.position;
            offset.y = player.position.y;
            player.position = offset;
            if (_isOneUse)
            {
                _destinationPortal.SetActive(false);
                gameObject.SetActive(false);
            }
        }

        private void PortalEnabled(string portalID)
        {
            if (portalID == _portalID && !gameObject.activeSelf)
            {
                gameObject.SetActive(true);
            }
        }
    }
}

using System;
using UnityEngine;

namespace Props.PortalV2
{
    public class PortalV2 : MonoBehaviour
    {
        [SerializeField] private GameObject _destinationPortal;
        [SerializeField] private bool _isOneWay;

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
            Vector3 offset = player.position - transform.position;
            offset += _destinationPortal.transform.position;
            offset.y = player.position.y;
            player.position = offset;
            if (_isOneWay)
            {
                _destinationPortal.SetActive(false);
                gameObject.SetActive(false);
            }
        }
    }
}

using UnityEngine;
using Utils;

namespace Props.Portal
{
    public class TeleportationByContact : MonoBehaviour
    {
        [SerializeField] private PortalExit _destinationPortal;
        [SerializeField] private bool _isOneUse;
        [SerializeField] private string _destinationRoomID;
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("MainCamera"))
            {
                Debug.Log("TP " + transform.parent.gameObject.name);
                Teleport(other.transform.parent.transform);
            }
        }
        private void Teleport(Transform player)
        {
            GameEvents.OnRoomChanged.Invoke(_destinationRoomID);
            Vector3 offset = player.position - transform.position;
            offset += _destinationPortal.TpTarget.position;
            offset.y = player.position.y;
            player.position = offset;
            if (_isOneUse)
            {
                _destinationPortal.gameObject.SetActive(false);
                gameObject.SetActive(false);
            }
        }
    }
}

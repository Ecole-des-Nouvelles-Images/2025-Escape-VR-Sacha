using UnityEngine;
using Utils;

namespace Props.Portal
{
    public class TeleportationByContact : MonoBehaviour
    {
        [SerializeField] private GameObject _destinationPortal;
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
            offset += _destinationPortal.transform.position;
            offset.y = player.position.y;
            player.position = offset;
            if (_isOneUse)
            {
                _destinationPortal.SetActive(false);
                gameObject.SetActive(false);
            }
        }
    }
}

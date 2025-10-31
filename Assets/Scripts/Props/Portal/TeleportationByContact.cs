using Sound;
using UnityEngine;
using Utils;

namespace Props.Portal
{
    public class TeleportationByContact : MonoBehaviour
    {
        [SerializeField] private PortalExit _destinationPortal;
        [SerializeField] private string _destinationPortalID;
        [SerializeField] private string _openingDialogID;
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("MainCamera"))
            {
                //Debug.Log("TP " + transform.parent.gameObject.name);
                Teleport(other.transform.parent.transform);
            }
        }
        private void Teleport(Transform player)
        {
            if (_openingDialogID != null)
            {
                FindAnyObjectByType<DialogManager>().PlayDialogue(_openingDialogID,3f);
            }
            GameEvents.OnNextRoom.Invoke();
            GameEvents.OnTeleport.Invoke();
            Vector3 offset = player.position - transform.position;
            offset += _destinationPortal.TpTarget.position;
            offset.y = player.position.y;
            player.position = offset;
            transform.parent.gameObject.SetActive(false);
        }
    }
}

using System;
using Manager;
using UnityEngine;

namespace Character
{
    public class PlayerHeadTrigger : MonoBehaviour
    {
        //[SerializeField] private Collider _mainCamCollider;
        private int _portalTouched;
        private bool _portalIsOneWay;
        private void OnEnable()
        {
            GameEvents.OnPortalTriggered += SetPortalIsOneWayValue;
        }

        private void OnDisable()
        {
            GameEvents.OnPortalTriggered -= SetPortalIsOneWayValue;
        }

        private void SetPortalIsOneWayValue(bool isOneWay)
        {
            _portalIsOneWay = isOneWay;
        }
    }
}

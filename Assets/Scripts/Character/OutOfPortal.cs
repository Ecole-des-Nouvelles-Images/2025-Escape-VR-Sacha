using System;
using Manager;
using UnityEngine;
using Utils;

namespace Character
{
    public class OutOfPortal : MonoBehaviour
    {
        [SerializeField] private Collider _mainCamCollider;
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

        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("Portal"))
            {
                _portalTouched += 1;
                if(_portalTouched == 2)
                {
                    _mainCamCollider.enabled = true;
                    _portalTouched = 0;
                }
                other.transform.GetComponent<Renderer>().enabled = true;
                // if (_portalIsOneWay)
                // {
                //     other.enabled = false;
                //     //other.transform.GetComponent<Renderer>().enabled = false;
                // }
                // else
                // {
                //     
                // }
            }
        }

        private void SetPortalIsOneWayValue(bool isOneWay)
        {
            _portalIsOneWay = isOneWay;
        }
    }
}

using System;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Interactors;
using Utils;

namespace Manager
{
    public class XRControllerManager : MonoBehaviour
    {
        [SerializeField] private NearFarInteractor _leftNearFarInteractor;
        [SerializeField] private NearFarInteractor _rightNearFarInteractor;

        private void OnEnable()
        {
            GameEvents.OnEnableFarInteractor += EnableFarInteraction;
            GameEvents.OnDisableFarInteractor += DisableFarInteraction;
        }

        private void OnDisable()
        {
            GameEvents.OnEnableFarInteractor -= EnableFarInteraction;
            GameEvents.OnDisableFarInteractor -= DisableFarInteraction;
        }

        private void OnDestroy()
        {
            GameEvents.OnEnableFarInteractor -= EnableFarInteraction;
            GameEvents.OnDisableFarInteractor -= DisableFarInteraction;
        }

        private void EnableFarInteraction()
        {
            _leftNearFarInteractor.enableFarCasting = true;
            _rightNearFarInteractor.enableFarCasting = true;
        }
        private void DisableFarInteraction()
        {
            _leftNearFarInteractor.enableFarCasting = false;
            _rightNearFarInteractor.enableFarCasting = false;
        }
    }
}

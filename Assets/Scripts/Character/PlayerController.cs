using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR;

namespace Character
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private Transform _roomBounds;
        [SerializeField] private InputActionProperty _recenterButton;

        void Update()
        {
            if (_recenterButton.action.WasPressedThisFrame())
            {
                Debug.Log("Recenter button pressed!");
                Recenter();
            }
        }

        private void Recenter()
        {
            Vector3 headsetPosition = InputTracking.GetLocalPosition(XRNode.Head);
            Quaternion headsetRotation = InputTracking.GetLocalRotation(XRNode.Head);

            _roomBounds.position = headsetPosition;
            _roomBounds.rotation = headsetRotation;
        }

        void OnEnable()
        {
            _recenterButton.action.Enable();
        }

        void OnDisable()
        {
            _recenterButton.action.Disable();
        }
    }
}
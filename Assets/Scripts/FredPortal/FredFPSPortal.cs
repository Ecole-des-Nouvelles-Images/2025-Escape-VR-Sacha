using System;
using Portal;
using UnityEngine;
using UnityEngine.Serialization;

namespace FredPortal
{
    public class FredFPSPortal : MonoBehaviour
    {
        [FormerlySerializedAs("_destinationFPSPortal")] [FormerlySerializedAs("destinationPortal")] [SerializeField] private FredFPSPortal _destinationFredFPSPortal;
        [SerializeField] private string playerTag = "Player";
        [SerializeField] private string playerCameraTag = "MainCamera";
        [SerializeField] private bool isActive = true;
    
        private PortalTrigger _portalTrigger;
        private Camera _portalCamera;
        private MeshRenderer _portalRenderer;
        private Camera _destinationPortalCamera;
        private Transform _playerTransform;
        private Transform _playerCameraTransform;
        private Material _portalMaterial;
        private bool _isHidden;

        private void Awake()
        {
            _portalTrigger = GetComponentInChildren<PortalTrigger>();
            if (_portalTrigger == null) throw new Exception("Portal Trigger not found");
            
            _portalCamera = GetComponentInChildren<Camera>();
            if (_portalCamera == null) throw new Exception("Portal Camera not found");
                
            _portalRenderer = GetComponentInChildren<MeshRenderer>();
            if (_portalRenderer == null) throw new Exception("Portal MeshRenderer not found");
            _portalMaterial = _portalRenderer.material;
            
            GameObject playerCameraGameObject = GameObject.FindWithTag(playerCameraTag);
            if (playerCameraGameObject == null) throw new Exception("Player Camera GameObject not found");
            _playerCameraTransform = playerCameraGameObject.transform;
        }

        private void Start()
        {
            if (_destinationFredFPSPortal == null)
            {
                _portalRenderer.enabled = false;
            }
            else
            {
                var portalRenderTexture = new RenderTexture(1024, 1024, 16, RenderTextureFormat.ARGBHalf);
                portalRenderTexture.Create();
                _portalMaterial = new Material(Shader.Find("Custom/Portal"));
                _portalMaterial.SetTexture("_MainTex", portalRenderTexture);
                _portalMaterial.SetInteger("displayMask", isActive ? 1 : 0);
                _portalRenderer.material = _portalMaterial;
                _destinationFredFPSPortal._portalCamera.targetTexture = portalRenderTexture;
            }
        }

        private void OnEnable()
        {
            _portalTrigger.OnTriggerEntered += PortalEntered;
            _portalTrigger.OnTriggerExited += PortalExited;
        }

        private void OnDisable()
        {
            _portalTrigger.OnTriggerEntered -= PortalEntered;
            _portalTrigger.OnTriggerExited -= PortalExited;
        }

        private void Update()
        {
            if (_destinationFredFPSPortal == null) return;
            // Camera Position
            var offset = _playerCameraTransform.position - _destinationFredFPSPortal.transform.position;
            _portalCamera.transform.position = transform.position + offset;
            // Camera Rotation
            _portalCamera.transform.rotation = _playerCameraTransform.rotation;
            // Portal Mask
            _portalMaterial.SetInteger("displayMask", isActive ? 1 : 0);
            _portalRenderer.enabled = !_isHidden;
        }

        private void PortalEntered(Collider other)
        {
            if (_destinationFredFPSPortal == null || _isHidden) return;
            if (!other.CompareTag(playerTag)) return;
            var offset = other.transform.position - transform.position;
            other.transform.position = _destinationFredFPSPortal.transform.position + offset;
            _destinationFredFPSPortal._isHidden = true;
        }

        private void PortalExited(Collider other)
        {
            if (_destinationFredFPSPortal == null) return;
            if (!other.CompareTag(playerTag)) return;
            _isHidden = false;
        }
    }
}
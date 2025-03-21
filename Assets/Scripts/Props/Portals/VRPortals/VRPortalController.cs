using Manager;
using Props.Portals.FPSPortals;
using UnityEngine;
using UnityEngine.Experimental.Rendering;

namespace Props.Portals.VRPortals
{
    public class VRPortalController : MonoBehaviour
    {
        private bool _isActive = true;
        private VRPortal _myVRPortalComponent;
        private MeshRenderer _portalRenderer;
        private Material _portalMaterial;
        private RenderTexture _portalTexture;
        private GameObject _mainCamera;
        
        private void Awake()
        {
            _portalRenderer = GetComponent<MeshRenderer>();
            _myVRPortalComponent = transform.parent.GetComponent<VRPortal>();
            _isActive = _myVRPortalComponent.IsActive;
            _portalMaterial = _myVRPortalComponent.PortalCustomShader;
            _portalTexture = new RenderTexture(1920, 1080, GraphicsFormat.R8G8B8A8_UNorm,
                GraphicsFormat.D32_SFloat_S8_UInt);
        }
        private void Start()
        {
            _myVRPortalComponent.SetupCameraRenderTexture(_portalTexture);
            _portalRenderer.material = _portalMaterial;
        }
        
        private void Update()
        {
            _portalMaterial.SetInteger("displayMask", _isActive ? 1:0);
            _portalMaterial.SetTexture("_MainTex", _portalTexture);
        }
        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("MainCamera"))
            {
                _myVRPortalComponent.PlayerTeleportation(other.transform.parent.transform);
                other.enabled = false;
                _myVRPortalComponent.ActiveComponentOfDestination();
            }
        }
    }
}

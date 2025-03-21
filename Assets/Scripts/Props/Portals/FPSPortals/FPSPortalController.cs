using Manager;
using UnityEngine;
using UnityEngine.Experimental.Rendering;

namespace Props.Portals.FPSPortals
{
    public class FPSPortalController : MonoBehaviour
    {
        private bool _isActive = true;
        private FPSPortal _myFPSPortalComponent;
        private MeshRenderer _portalRenderer;
        private Material _portalMaterial;
        private RenderTexture _portalTexture;
        private GameObject _mainCamera;
        
        private void Awake()
        {
            _portalRenderer = GetComponent<MeshRenderer>();
            _myFPSPortalComponent = transform.parent.GetComponent<FPSPortal>();
            _isActive = _myFPSPortalComponent.IsActive;
            _portalMaterial = _myFPSPortalComponent.PortalCustomShader;
            _portalTexture = new RenderTexture(1920, 1080, GraphicsFormat.R8G8B8A8_UNorm,
                GraphicsFormat.D32_SFloat_S8_UInt);
        }
        private void Start()
        {
            _myFPSPortalComponent.SetupCameraRenderTexture(_portalTexture);
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
                _myFPSPortalComponent.PlayerTeleportation(other.transform.parent.transform);
                other.enabled = false;
                GameEvents.OnPortalTriggered.Invoke(_myFPSPortalComponent.IsOneWayTeleportation);
                _myFPSPortalComponent.ActiveComponentOfDestination();
            }
        }
    }
}

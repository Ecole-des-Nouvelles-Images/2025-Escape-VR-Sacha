using UnityEngine;
using UnityEngine.Experimental.Rendering;

namespace Props.Portals
{
    public class PortalController : MonoBehaviour
    {
        private bool _isActive = true;
        private Portal _myPortalComponent;
        private MeshRenderer _portalRenderer;
        private Material _portalMaterial;
        private RenderTexture _portalTexture;
        private GameObject _mainCamera;
        private int _resolutionX;
        private int _resolutionY;
        
        private void Awake()
        {
            _portalRenderer = GetComponent<MeshRenderer>();
            _myPortalComponent = transform.parent.GetComponent<Portal>();
            _isActive = _myPortalComponent.IsActive;
            _portalMaterial = _myPortalComponent.PortalCustomShader;
            _resolutionX = _myPortalComponent.ResolutionX;
            _resolutionY = _myPortalComponent.ResolutionY;
            _portalTexture = new RenderTexture(_resolutionX, _resolutionY, GraphicsFormat.R8G8B8A8_UNorm,
                GraphicsFormat.D32_SFloat_S8_UInt);
        }
        private void Start()
        {
            _myPortalComponent.SetupCameraRenderTexture(_portalTexture);
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
                _myPortalComponent.PlayerTeleportation(other.transform.parent.transform);
                other.enabled = false;
                _myPortalComponent.ActiveComponentOfDestination();
            }
        }
    }
}
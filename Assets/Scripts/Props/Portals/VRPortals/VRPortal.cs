using UnityEngine;

namespace Props.Portals.FPSPortals
{
    public class VRPortal : MonoBehaviour
    {
        [SerializeField] private GameObject _destinationPortal;
        [SerializeField] private Material _portalCustomShader;
        [SerializeField] private bool _isActive = true;
        
        public Transform TargetTransform {get{return _targetTransform;}}
        public Transform SourceTransform {get{return _sourceTransform;}}
        public Material PortalCustomShader {get{return _portalCustomShader;}}
        public bool IsActive {get{return _isActive;}}
        
        private GameObject _myCamera;
        private Transform _targetTransform;
        private Transform _sourceTransform;

        private void Awake()
        {
            _targetTransform = _destinationPortal.transform;
            _sourceTransform = transform.GetChild(0).transform;
            _myCamera = transform.GetChild(1).transform.gameObject;
        }

        public void PlayerTeleportation( Transform player)
        {
            //Position
            Vector3 offset = player.position - _sourceTransform.position;
            offset += _targetTransform.position;
            offset.y = player.position.y;
            player.position = offset;
            //Rotation
            player.rotation = player.rotation;
        }

        public void SetupCameraRenderTexture(RenderTexture portalTexture)
        {
            _myCamera.GetComponent<Camera>().targetTexture = portalTexture;
        }

        public void ActiveComponentOfDestination()
        {
            if (_destinationPortal.GetComponentInChildren<Collider>().enabled == false)
            {
                _destinationPortal.GetComponentInChildren<Collider>().enabled = true;
            }
            if (_destinationPortal.GetComponentInChildren<Renderer>().enabled == true)
            {
                _destinationPortal.GetComponentInChildren<Renderer>().enabled = false;
            }
        }
    }
}

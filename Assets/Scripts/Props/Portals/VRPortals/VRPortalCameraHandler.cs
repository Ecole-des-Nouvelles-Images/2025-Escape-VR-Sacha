using Props.Portals.FPSPortals;
using UnityEngine;

namespace Props.Portals.VRPortals
{
    public class VRPortalCameraHandler : MonoBehaviour
    {
        private Transform _targetTransform;
        private Transform _sourceTransform;
        private Transform _mainCameraTransform;
        
        private void Start()
        {
            _mainCameraTransform = GameObject.FindGameObjectWithTag("MainCamera")?.transform;
            _targetTransform = transform.parent.GetComponent<VRPortal>().TargetTransform;
            _sourceTransform = transform.parent.GetComponent<VRPortal>().SourceTransform;
            if (_mainCameraTransform != null)
                transform.GetComponent<Camera>().fieldOfView = _mainCameraTransform.GetComponent<Camera>().fieldOfView;
        }
        private void Update()
        {
            //Position
            Vector3 offset = _mainCameraTransform.position - _sourceTransform.position;
            offset += _targetTransform.position;
            offset.y = _mainCameraTransform.position.y;
            transform.position = offset;
            //Rotation
            transform.rotation = new Quaternion(transform.rotation.x,_mainCameraTransform.rotation.y,transform.rotation.z,transform.rotation.w);
        }
    }
}

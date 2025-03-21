using UnityEngine;

namespace Props.Portals.FPSPortals
{
    public class FPSPortalCameraHandler : MonoBehaviour
    {
        private Transform _targetTransform;
        private Transform _sourceTransform;
        private Transform _mainCameraTransform;
        
        private void Start()
        {
            _mainCameraTransform = GameObject.FindGameObjectWithTag("MainCamera")?.transform;
            _targetTransform = transform.parent.GetComponent<FPSPortal>().TargetTransform;
            _sourceTransform = transform.parent.GetComponent<FPSPortal>().SourceTransform;
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
            transform.rotation = _mainCameraTransform.rotation;
        }
    }
}

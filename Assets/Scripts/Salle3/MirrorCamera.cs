using UnityEngine;

namespace Salle3
{
    public class MirrorCamera : MonoBehaviour
    {
        [SerializeField] private Transform _cameraOffset;
        [SerializeField] private Transform _mirrorSurface;
        [SerializeField] private Camera _mirrorCamera;

        private void Awake()
        {
            if (_mirrorCamera != null)
            {
                _mirrorCamera.stereoTargetEye = StereoTargetEyeMask.None;
            }
        }

        private void LateUpdate()
        {
            if (_cameraOffset == null || _mirrorSurface == null || _mirrorCamera == null)
                return;

            Vector3 mirrorNormal = _mirrorSurface.forward;
            Vector3 toCamera = _cameraOffset.position - _mirrorSurface.position;
            Vector3 reflectedPosition = Vector3.Reflect(toCamera, mirrorNormal);

            _mirrorCamera.transform.position = _mirrorSurface.position + reflectedPosition;

            _mirrorCamera.transform.rotation = Quaternion.LookRotation(mirrorNormal, Vector3.up);
        }
    }
}
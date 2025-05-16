using UnityEngine;

namespace Salle3
{
    [RequireComponent(typeof(Camera))]
    public class MirrorCamera : MonoBehaviour
    {
        [SerializeField] private Transform _mainCamera;
        [SerializeField] private Transform _mirror;

        private Camera _cam;

        void Start()
        {
            _cam = GetComponent<Camera>();
        }

        void LateUpdate()
        {
            // Réflexion horizontale
            Vector3 localPlayer = _mirror.InverseTransformPoint(_mainCamera.position);
            Vector3 lookAtMirror = _mirror.TransformPoint(new Vector3(-localPlayer.x, localPlayer.y, localPlayer.z));
            transform.LookAt(lookAtMirror);

            // ➤ Appliquer un plan de clipping aligné au miroir
            SetObliqueClippingPlane();
        }

        private void SetObliqueClippingPlane()
        {
            Vector3 pos = _mirror.position;
            Vector3 normal = _mirror.forward;

            // Plan dans l’espace caméra
            Vector4 clipPlaneWorldSpace = new Vector4(normal.x, normal.y, normal.z, -Vector3.Dot(normal, pos));
            Vector4 clipPlaneCameraSpace = Matrix4x4.Transpose(Matrix4x4.Inverse(_cam.worldToCameraMatrix)) * clipPlaneWorldSpace;

            _cam.projectionMatrix = _cam.CalculateObliqueMatrix(clipPlaneCameraSpace);
        }
    }
}
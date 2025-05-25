using UnityEngine;

namespace Salle3
{
    public class MirrorCamera : MonoBehaviour
    {
        [SerializeField] private Transform _mainCamera;
        [SerializeField] private Transform _mirror;

        void LateUpdate()
        {
            Vector3 localPlayer = _mirror.InverseTransformPoint(_mainCamera.position);
            Vector3 mirroredPosition =
                _mirror.TransformPoint(new Vector3(-localPlayer.x, localPlayer.y, localPlayer.z));

            transform.position = mirroredPosition;
        }
    }
}
using UnityEngine;

namespace ProtoElias.Salle3
{
    public class MirrorCamera : MonoBehaviour
    {
        public Transform mainCamera;
        public Transform mirror;

        void LateUpdate()
        {
            Vector3 camPos = mainCamera.position;
            Vector3 mirrorNormal = mirror.forward;
            Vector3 mirrorPos = mirror.position;

            Vector3 d = camPos - mirrorPos;
            Vector3 reflected = d - 2 * Vector3.Dot(d, mirrorNormal) * mirrorNormal;

            transform.position = mirrorPos + reflected;

            Vector3 lookDir = mainCamera.forward;
            Vector3 reflectedDir = lookDir - 2 * Vector3.Dot(lookDir, mirrorNormal) * mirrorNormal;

            transform.rotation = Quaternion.LookRotation(reflectedDir, Vector3.up);
        }
    }
}
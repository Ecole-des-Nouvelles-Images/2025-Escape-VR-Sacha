using UnityEngine;

namespace Salle3
{
    public class MirrorCamera : MonoBehaviour
    { 
        [SerializeField]private Transform _mainCamera; 
        [SerializeField]private Transform _mirror;

        void LateUpdate()
        {
            Vector3 mirrorNormal = _mirror.forward;

            transform.position = _mirror.position;

            Vector3 lookDir = _mainCamera.position - _mirror.position;
            Vector3 reflectedDir = lookDir - 2 * Vector3.Dot(lookDir, mirrorNormal) * mirrorNormal;

            transform.rotation = Quaternion.LookRotation(reflectedDir, Vector3.up);
        }
    }
}
using UnityEngine;

namespace Salle3
{
    public class MirrorComponent : MonoBehaviour
    {
        [Tooltip("The real object to mirror.")]
        public Transform originalObject;

        [Tooltip("Offset on the X axis between real and mirrored objects.")]
        public float mirrorOffsetX = 3f;

        private Vector3 lastPosition;
        private Quaternion lastRotation;

        void LateUpdate()
        {
            if (originalObject == null)
                return;

            // Only update if original moved
            if (originalObject.position != lastPosition || originalObject.rotation != lastRotation)
            {
                MirrorTransform();
                lastPosition = originalObject.position;
                lastRotation = originalObject.rotation;
            }
        }

        private void MirrorTransform()
        {
            // Mirror position by applying fixed X offset
            Vector3 mirroredPos = originalObject.position;
            mirroredPos.x += mirrorOffsetX;
            transform.position = mirroredPos;

            // Mirror rotation (Y and Z flipped due to -1 scale on X)
            Vector3 mirroredEuler = originalObject.rotation.eulerAngles;
            mirroredEuler.y = -mirroredEuler.y;
            mirroredEuler.z = -mirroredEuler.z;
            transform.rotation = Quaternion.Euler(mirroredEuler);
        }
    }
}


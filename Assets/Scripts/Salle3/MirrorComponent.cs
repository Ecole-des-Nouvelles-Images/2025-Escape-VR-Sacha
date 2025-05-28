using UnityEngine;

namespace Salle3
{
    public class MirrorComponent : MonoBehaviour
    {
        [Tooltip("The real object to mirror.")]
        public Transform originalObject;

        [Tooltip("The mirror plane position on the X axis.")]
        private float mirrorPlaneX = 1.5f;

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
            // Calculate the mirrored position
            Vector3 mirroredPos = originalObject.position;
            mirroredPos.x = mirrorPlaneX - (originalObject.position.x - mirrorPlaneX);
            transform.position = mirroredPos;

            // Mirror rotation (Y and Z flipped due to -1 scale on X)
            Vector3 mirroredEuler = originalObject.rotation.eulerAngles;
            mirroredEuler.y = -mirroredEuler.y;
            mirroredEuler.z = -mirroredEuler.z;
            transform.rotation = Quaternion.Euler(mirroredEuler);
        }
    }
}
using System;
using UnityEngine;

namespace Utils
{
    public class CameraBinder : MonoBehaviour
    {
        private Camera _cam;
        public Camera Cam => _cam;
    
        private void Start()
        {
            _cam = Camera.main;

            if (!_cam)
            {
                throw new NullReferenceException("Unable to get MainCamera on object {gameObject.name}");
            }
        }
    }
}

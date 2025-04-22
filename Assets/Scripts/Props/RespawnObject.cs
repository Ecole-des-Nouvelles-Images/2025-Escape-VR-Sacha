using UnityEngine;

namespace Props
{
    public class RespawnObject : MonoBehaviour
    {
        [SerializeField] private float _timeBeforeRespawn;
        
        private Vector3 _beginPos;
        private float _currentTime;
        private bool _isGrabbed;
        
        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            _beginPos = transform.position;
        }

        // Update is called once per frame
        void Update()
        {
            if (!transform.parent && _isGrabbed == false)
            {
                _isGrabbed = true;
            }

            if (transform.parent && _isGrabbed)
            {
                _isGrabbed = false;
            }

            if (transform.position != _beginPos && !_isGrabbed)
            {
                _currentTime += Time.deltaTime;
            }

            if (_currentTime >= _timeBeforeRespawn)
            {
                transform.position = _beginPos;
                _currentTime = 0;
            }
        }
    }
}

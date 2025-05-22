using System;
using UnityEngine;

namespace Props
{
    public class RespawnObject : MonoBehaviour
    {
        [SerializeField] private float _timeBeforeRespawn;
        [SerializeField] private AudioSource _myAudioSource;
        
        private Vector3 _beginPos;
        private Quaternion _beginRot;
        private float _currentTime;
        private bool _isGrabbed;
        private Rigidbody _rigidbody;

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();
        }

        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            _beginPos = transform.position;
            _beginRot = transform.rotation;
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
                ResetObject();
                _currentTime = 0;
            }
        }

        private void ResetObject()
        {
            transform.position = _beginPos;
            transform.rotation = _beginRot;
            _rigidbody.linearVelocity = Vector3.zero;
            _myAudioSource.Play();
        }
    }
}

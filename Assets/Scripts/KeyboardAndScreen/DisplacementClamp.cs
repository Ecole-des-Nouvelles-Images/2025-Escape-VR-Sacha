using System;
using UnityEngine;

namespace KeyboardAndScreen
{
    public class DisplacementClamp : MonoBehaviour
    {
        public enum Axes
        {
            X,
            Y,
            Z
        }
        
        [SerializeField] private Axes _axes = Axes.X;
        [SerializeField] private float _maxPushDisplacement = 0.5f;
        [SerializeField] private bool _moveForward = true;
        
        private Vector3 _originalPosition;
        private Vector3 _newPos;

        private void Awake()
        {
            _originalPosition = transform.position;
        }

        private void Update()
        {
            switch (_axes)
            {
                case Axes.X:
                    if (_moveForward)
                    {
                        if (transform.position.x > _originalPosition.x +_maxPushDisplacement)
                        {
                            _newPos = new Vector3(_originalPosition.x +_maxPushDisplacement, transform.position.y, transform.position.z);
                        }
                        else if (transform.position.x < _originalPosition.x )
                        {
                            _newPos = new Vector3(_originalPosition.x, transform.position.y, transform.position.z);
                        }
                    }
                    else 
                    {
                        if (transform.position.x < _originalPosition.x - _maxPushDisplacement)
                        {
                            _newPos = new Vector3(_originalPosition.x - _maxPushDisplacement, transform.position.y, transform.position.z);
                        }
                        else if (transform.position.x > _originalPosition.x )
                        {
                            _newPos = new Vector3(_originalPosition.x, transform.position.y, transform.position.z);
                        }
                    }
                    break;
                case Axes.Y:
                    if (_moveForward)
                    {
                        if (transform.position.y > _originalPosition.y + _maxPushDisplacement)
                        {
                            _newPos = new Vector3(transform.position.x, _originalPosition.y + _maxPushDisplacement, transform.position.z);
                        }
                        else if (transform.position.y < _originalPosition.y )
                        {
                            _newPos = new Vector3(transform.position.x, _originalPosition.y, transform.position.z);
                        }
                    }
                    else 
                    {
                        if (transform.position.y < _originalPosition.y - _maxPushDisplacement)
                        {
                            _newPos = new Vector3(transform.position.x, _originalPosition.y - _maxPushDisplacement, transform.position.z);
                        }
                        else if (transform.position.y > _originalPosition.y )
                        {
                            _newPos = new Vector3(transform.position.x, _originalPosition.y, transform.position.z);
                        }
                    }
                    break;
                case Axes.Z:
                    if (_moveForward)
                    {
                        if (transform.position.z > _originalPosition.z + _maxPushDisplacement)
                        {
                            _newPos = new Vector3(transform.position.x, transform.position.y, _originalPosition.z + _maxPushDisplacement);
                        }
                        else if (transform.position.z < _originalPosition.z )
                        {
                            _newPos = new Vector3(transform.position.x, transform.position.y, _originalPosition.z);
                        }
                    }
                    else 
                    {
                        if (transform.position.x < _originalPosition.z - _maxPushDisplacement)
                        {
                            _newPos = new Vector3(transform.position.x, transform.position.y, _originalPosition.z - _maxPushDisplacement);
                        }
                        else if (transform.position.z > _originalPosition.z )
                        {
                            _newPos = new Vector3(transform.position.x, transform.position.y, _originalPosition.z);
                        }
                    }
                    break;
            }
        }
    }
}
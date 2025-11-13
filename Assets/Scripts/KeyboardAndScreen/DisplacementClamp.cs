using System;
using UnityEngine;

namespace KeyboardAndScreen
{
    public class DisplacementClamp : MonoBehaviour
    {
        [SerializeField] private float _maxPushDisplacement = 0.5f;
        [SerializeField] private bool _moveForward = true;
        
        private NumepadKeyPhysicControler _physicController;
        private Vector3 _originalPosition;

        private void Awake()
        {
            _originalPosition = transform.position;
            _physicController = GetComponent<NumepadKeyPhysicControler>();
        }

        private void Update()
        {
            switch (_physicController.axes)
            {
                case NumepadKeyPhysicControler.Axes.X:
                    if (_moveForward)
                    {
                        if (transform.position.x > _maxPushDisplacement + _originalPosition.x)
                        {
                            Vector3 newPos = new Vector3(_maxPushDisplacement + _originalPosition.x, transform.position.y, transform.position.z) ;
                        }
                    }
                    else 
                    {
                        if (transform.position.x < _maxPushDisplacement - _originalPosition.x)
                        {
                            
                        }
                    }
                    break;
                case NumepadKeyPhysicControler.Axes.Y:
                    if (_moveForward)
                    {
                        if (transform.position.y > _maxPushDisplacement + _originalPosition.y)
                        {
                            
                        }
                    }
                    else 
                    {
                        if (transform.position.y < _maxPushDisplacement - _originalPosition.y)
                        {
                            
                        }
                    }
                    break;
                case NumepadKeyPhysicControler.Axes.Z:
                    if (_moveForward)
                    {
                        if (transform.position.z > _maxPushDisplacement + _originalPosition.z)
                        {
                            
                        }
                    }
                    else 
                    {
                        if (transform.position.x < _maxPushDisplacement - _originalPosition.x)
                        {
                            
                        }
                    }
                    break;
            }
        }
    }
}
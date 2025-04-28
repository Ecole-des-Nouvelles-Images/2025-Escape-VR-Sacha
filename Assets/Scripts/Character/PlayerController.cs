using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Character
{
    public class PlayerController : MonoBehaviour
    {
        
        [SerializeField] private Transform _body;
        [SerializeField] private Transform _head;
        [SerializeField] private float _mouvSpeed;
        [SerializeField] private float _minimalVerticalRotLimit;
        [SerializeField] private float _maximalVerticalRotLimit;
        
        private Rigidbody _rbBody;
        private Vector2 _rotationInputValue;
        private Vector2 _moveInputValue;
        private void Awake() {
            _rbBody = _body.GetComponent<Rigidbody>();
        }
        
        private void OnLook(InputValue value)
        {
            _rotationInputValue = value.Get<Vector2>();
        }
        private void OnMove(InputValue value)
        {
            _moveInputValue = value.Get<Vector2>();
        }

        private void Update()
        {
            Vector3 movement = new Vector3(_moveInputValue.x, 0, _moveInputValue.y);
            _body.Translate(movement * (_mouvSpeed * Time.deltaTime));
            _body.Rotate(0, _rotationInputValue.x, 0);
            _head.Rotate(Mathf.Clamp(_rotationInputValue.y*-1,_minimalVerticalRotLimit,_maximalVerticalRotLimit), 0, 0);
        }
    }
}

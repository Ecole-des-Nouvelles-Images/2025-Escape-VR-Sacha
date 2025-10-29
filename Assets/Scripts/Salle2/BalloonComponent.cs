using System;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace Salle2
{
    public class BalloonComponent : MonoBehaviour
    {
        private Rigidbody _rigidbody;
        [SerializeField] private int _ballonCode;

        private void Start()
        {
            _rigidbody = GetComponent<Rigidbody>();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("Hand"))
            {
                _rigidbody.AddForce(0, 10, 0, ForceMode.Impulse);
            }
        }
        
        
        
    }
}

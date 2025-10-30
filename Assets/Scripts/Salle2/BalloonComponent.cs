using UnityEngine;
using System.Collections;

namespace Salle2
{
    public class BalloonComponent : MonoBehaviour
    {
        private Rigidbody _rigidbody;
        private Collider _collider;
        public string BallonCode;
        private LivingroomHandler _livingroomHandler;

        private void Start()
        {
            _rigidbody = GetComponent<Rigidbody>();
            _collider = GetComponent<Collider>();
            _livingroomHandler = FindObjectOfType<LivingroomHandler>();
        }

        private void OnCollisionEnter(Collision other)
        {
            if (other.gameObject.CompareTag("Hands"))
            {
                _rigidbody.AddForce(Vector3.up * 0.5f, ForceMode.Impulse);
                _livingroomHandler.BallonTouched(BallonCode);
                StartCoroutine(DisableColliderForSeconds(0.3f));
            }
        }

        private IEnumerator DisableColliderForSeconds(float seconds)
        {
            _collider.enabled = false;
            yield return new WaitForSeconds(seconds);
            _collider.enabled = true;
        }
    }
}
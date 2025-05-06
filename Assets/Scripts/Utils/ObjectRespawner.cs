using System;
using UnityEngine;

namespace Utils
{
    public class ObjectRespawner : MonoBehaviour
    {
        [SerializeField] private bool _isFilteredByTag;
        [SerializeField] private string[] _objectsTagToSpawn;
        [SerializeField] private GameObject _spawnAnchor;
        private void OnTriggerExit(Collider other)
        {
            if (_isFilteredByTag)
            {
                for (int i = 0; i < _objectsTagToSpawn.Length; i++)
                {
                    if (other.CompareTag(_objectsTagToSpawn[i]))
                    {
                        other.transform.position = _spawnAnchor.transform.position;
                        other.transform.GetComponent<Rigidbody>().linearVelocity = Vector3.zero;
                    }
                }
            }
            other.transform.position = _spawnAnchor.transform.position;
            other.transform.GetComponent<Rigidbody>().linearVelocity = Vector3.zero;
        }
    }
}

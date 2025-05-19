using System;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Interactables;
using UnityEngine.XR.Interaction.Toolkit.Transformers;
using Random = UnityEngine.Random;

namespace Puzzles.Corridors
{
    public class SecondPuzzle : Puzzle
    {
        [SerializeField] private GameObject[] _allPuzzleObjects;
        [SerializeField] private GameObject[] _allPuzzleObjectsCopy;
        [SerializeField] private GameObject[] _allKeyObjects;
        [SerializeField] private Material _ghostMat;
        
        private Material _mainMat;
        private int _keyIndex;

        private void Awake()
        {
            for (int i = 0; i < _allPuzzleObjectsCopy.Length; i++)
            {
                if(_allPuzzleObjectsCopy[i].activeSelf)
                    _allPuzzleObjectsCopy[i].SetActive(false);
            }
        }

        private void Start()
        {
            _keyIndex = RandomSelect(_allKeyObjects);
            ActivationObject(_allKeyObjects);
            _mainMat = _allKeyObjects[_keyIndex].GetComponent<MeshRenderer>().material;
            _allKeyObjects[_keyIndex].GetComponent<MeshRenderer>().material = _ghostMat;
            _allPuzzleObjects[_keyIndex].tag = "KeyObject";
            LockPortal();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("KeyObject"))
            {
                other.gameObject.SetActive(false);
                _allKeyObjects[_keyIndex].GetComponent<MeshRenderer>().material = _mainMat;
                for (int i = 0; i < _allPuzzleObjects.Length; i++)
                {
                    _allPuzzleObjects[i].SetActive(false);
                    if(i != _keyIndex)
                        _allPuzzleObjectsCopy[i].SetActive(true);
                }
                UnlockPortal();
            }
        }

        private int RandomSelect(GameObject[] arrayOfObjects)
        {
            return Random.Range(0, arrayOfObjects.Length);
        }

        private void ActivationObject(GameObject[] arrayOfObjects)
        {
            for (int i = 0; i < arrayOfObjects.Length; i++)
            {
                if(i != _keyIndex)
                    arrayOfObjects[i].SetActive(false);
                else
                {
                    arrayOfObjects[i].SetActive(true);
                }
            }
        }
    }
}

using UnityEngine;
using Random = UnityEngine.Random;

namespace Puzzles.Corridors
{
    public class SecondPuzzle : Puzzle
    {
        [SerializeField] GameObject[] _allPuzzleObjects;
        [SerializeField] GameObject[] _allKeyObjects;
        [SerializeField] Material _ghostMat;
        [SerializeField] Material _mainMat;
        
        private int _keyIndex;

        private void Start()
        {
            _keyIndex = RandomSelect(_allKeyObjects);
            ActivationObject(_allKeyObjects);
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

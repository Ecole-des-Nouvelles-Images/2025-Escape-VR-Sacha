using UnityEngine;

namespace Utils
{
    public class ClueActivator : MonoBehaviour
    { 
        [SerializeField] private GameObject[] _clues;
        [SerializeField] private string _myPuzzleID;

        private void OnEnable()
        {
            GameEvents.OnActualizeClue += ClueEnabling;
        }

        private void OnDisable()
        {
            GameEvents.OnActualizeClue -= ClueEnabling;
        }

        private void ClueEnabling(string id, int clueSelected)
        {
            if (id == _myPuzzleID && _clues.Length-1 >= clueSelected)
            {
                for (int i = 0; i < _clues.Length; i++)
                {
                    if(i == clueSelected)
                        _clues[i].SetActive(true);
                    else
                        _clues[i].SetActive(false);
                }
            }
        }
    }
}

using Puzzles;
using UnityEngine;
using System.Collections.Generic;

namespace Salle2
{
    public class LivingroomHandler : Puzzle
    {
        [SerializeField] private GameObject[] _balloons;
        private string _code = "123456";
        private List<string> _currentInput = new List<string>();

        public void BallonTouched(string ballonCode)
        {
            Debug.Log("Ballon touch : " + ballonCode);
            _currentInput.Add(ballonCode);

            if (_currentInput.Count > _code.Length || _currentInput[_currentInput.Count - 1] != _code[_currentInput.Count - 1].ToString())
            {
                _currentInput.Clear();
                Debug.Log("wrong code");
            }

            if (_currentInput.Count == _code.Length && string.Join("", _currentInput) == _code)
            {
                Debug.Log("correct code, opening portal");
                UnlockPortal();
            }
        }
    }
}
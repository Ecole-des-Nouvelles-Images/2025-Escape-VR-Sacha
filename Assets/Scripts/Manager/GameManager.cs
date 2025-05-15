using System;
using UnityEngine;
using Utils;

namespace Manager
{
    public class GameManager : MonoBehaviour
    {
        public bool IsGameEnding;

        [SerializeField] private int _endScore;
        private int _score;

        private void OnEnable()
        {
            GameEvents.OnIncreaseScore += IncreaseScore;
            GameEvents.OnEndGame += EndgameTriggered;
        }

        private void OnDisable()
        {
            GameEvents.OnIncreaseScore -= IncreaseScore;
            GameEvents.OnEndGame -= EndgameTriggered;
        }

        private void IncreaseScore()
        {
            _score++;
        }

        private void EndgameTriggered()
        {
            if (_score >= _endScore)
            {
                GameEvents.OnEnd(true);
            }
            else
            {
                GameEvents.OnEnd(false);
            }
        }
    }
}

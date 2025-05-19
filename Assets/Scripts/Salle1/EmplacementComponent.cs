using UnityEngine;

namespace Salle1 {
    public class EmplacementComponent : MonoBehaviour
    {
        [SerializeField] private WordHandler _wordHandler;

        public LetterComponent CurrentLetter { get; private set; }
        private SnapComponent _currentOccupant;

        public void SetLetter(LetterComponent letter, SnapComponent occupant)
        {
            CurrentLetter = letter;
            _currentOccupant = occupant;
            _wordHandler.CheckCurrentWord();
        }

        public void ClearIfOccupant(SnapComponent snap)
        {
            if (_currentOccupant == snap)
            {
                CurrentLetter = null;
                _currentOccupant = null;
                _wordHandler.CheckCurrentWord();
            }
        }
    }
}
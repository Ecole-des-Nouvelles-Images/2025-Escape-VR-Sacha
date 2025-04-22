using UnityEngine;
using Utils;

namespace Salle3 {
    public class ObjectPlacementManager : MonoBehaviour 
    {
        [SerializeField] private OfficeObject[] _officeObjects;
        private bool _puzzleCompleted = false;

        private void Start()
        {
            _officeObjects = GetComponentsInChildren<OfficeObject>();
        }

        private void Update() 
        {
            if (AllObjectsCorrectlyPlaced()&&!_puzzleCompleted) {
                GameEvents.OnPuzzleCompleted.Invoke("toFill");
                _puzzleCompleted = true;
                enabled = false;
            }
        }

        private bool AllObjectsCorrectlyPlaced() 
        {
            foreach (var obj in _officeObjects) {
                if (!obj.IsCorrectlyPlaced)
                    return false;
            }
            return true;
        }
    }
}
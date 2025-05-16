using Puzzles;
using UnityEngine;
using UnityEngine.Serialization;

namespace SalleIntro
{
    public class IntroHandler : Puzzle
    {
        [FormerlySerializedAs("playerCamera")] [SerializeField] private Transform _playerCamera;
        private bool _hasMovedToTarget = false;

        [FormerlySerializedAs("rayDistance")] [SerializeField] private float _rayDistance = 10f;
        [FormerlySerializedAs("lookLayerMask")] [SerializeField] private LayerMask _lookLayerMask;
        private bool[] _hasLookedAtTarget;
        private bool _hasLookedAtAllTargets;
        private int _totalLookTargets;

        [FormerlySerializedAs("puzzleTable")] [SerializeField] private GameObject _puzzleTable;
        private bool _puzzleActivated = false;

        private void Start()
        {
            _totalLookTargets = LayerMask.LayerToName(_lookLayerMask.value) == "" ? 3 : _lookLayerMask.value;
            _hasLookedAtTarget = new bool[_totalLookTargets];
            _puzzleTable.SetActive(false);
        }

        private void Update()
        {
            if (_hasMovedToTarget && !_puzzleActivated && !_hasLookedAtAllTargets)
            {
                CheckLookAround();
            }
        }

        public void PlayerMovedToTarget()
        {
            _hasMovedToTarget = true;
        }

        private void CheckLookAround()
        {
            Ray ray = new Ray(_playerCamera.position, _playerCamera.forward);
            if (Physics.Raycast(ray, out RaycastHit hitInfo, _rayDistance, _lookLayerMask))
            {
                ScreenComponent target = hitInfo.collider.GetComponent<ScreenComponent>();
                if (target && !_hasLookedAtTarget[target.TargetIndex])
                {
                    _hasLookedAtTarget[target.TargetIndex] = true;
                    //Debug.Log($"Regardé l'objet {target.TargetIndex} !");
                }
            }

            if (AllLookTargetsCompleted())
            {
                _hasLookedAtAllTargets = true;
                ActivatePuzzle();
            }
        }

        private bool AllLookTargetsCompleted()
        {
            foreach (bool looked in _hasLookedAtTarget)
            {
                if (!looked) return false;
            }
            return true;
        }

        private void ActivatePuzzle()
        {
            _puzzleActivated = true;
            _puzzleTable.SetActive(true);
            Debug.Log("Puzzle activé !");
            
            //a faire : logique du puzzle, semblable au rail salle 1.
        }
    }
}

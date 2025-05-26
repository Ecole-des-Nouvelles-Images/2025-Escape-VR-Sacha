using System.Collections;
using Puzzles;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

namespace Salle3 {
    public class OfficeHandler : Puzzle 
    {
        [SerializeField] private SnapComponent[] _officeObjects;
        private bool _puzzleCompleted = false;
        private int _lastStepReached = 0;
        
        [SerializeField] private GameObject _drawerMirrorObject;
        [SerializeField] private XRGrabInteractable _closetObjectGrab;
        
        [SerializeField] private GameObject _drawer;
        [SerializeField] private GameObject _closetDoor1, _closetDoor2;

        private void Start() {
            if (_officeObjects == null || _officeObjects.Length == 0) {
                _officeObjects = GetComponentsInChildren<SnapComponent>();
            }
            
            _drawerMirrorObject.SetActive(false);
            _closetObjectGrab.enabled = false;
            LockPortal();
        }

        private void Update() {
            int snappedCount = CountSnappedObjects();

            if (snappedCount > _lastStepReached) {
                _lastStepReached = snappedCount;

                switch (_lastStepReached) {
                    case 1:
                        OpenDrawer();
                        break;
                    case 3:
                        OpenCloset();
                        break;
                }
            }

            if (!_puzzleCompleted && snappedCount == _officeObjects.Length) {
                UnlockPortal();
                Debug.Log("Puzzle is complete");
                _puzzleCompleted = true;
                enabled = false;
            }
        }

        private int CountSnappedObjects() {
            int count = 0;
            foreach (var obj in _officeObjects) {
                if (obj.IsSnapped)
                    count++;
            }
            return count;
        }

        private void OpenDrawer() {
            Vector3 startPos = _drawer.transform.position;
            Vector3 targetPos = startPos + Vector3.right * 0.5f;
            StartCoroutine(MoveOverTime(_drawer.transform, startPos, targetPos, 1f));
            Debug.Log("Le tiroir s'ouvre !");
            _drawerMirrorObject.SetActive(true);
        }


        private void OpenCloset() {
            StartCoroutine(MoveOverTime(
                _closetDoor1.transform,
                _closetDoor1.transform.position,
                _closetDoor1.transform.position + Vector3.right * 0.5f,
                1f
            ));

            StartCoroutine(MoveOverTime(
                _closetDoor2.transform,
                _closetDoor2.transform.position,
                _closetDoor2.transform.position + Vector3.left * 0.5f,
                1f
            ));

            Debug.Log("L'armoire est ouverte !");
            _closetObjectGrab.enabled = true;
        }

        private IEnumerator MoveOverTime(Transform obj, Vector3 from, Vector3 to, float duration) {
            float elapsed = 0f;
            while (elapsed < duration)
            {
                obj.position = Vector3.Lerp(from, to, elapsed / duration);
                elapsed += Time.deltaTime;
                yield return null;
            }
            obj.position = to;
        }
    }
}
using UnityEngine;

namespace Salle3 {
    public class OfficeObject : MonoBehaviour {
        [SerializeField] private SnapComponent _snap;

        public bool IsCorrectlyPlaced => _snap != null && _snap.IsSnapped;
    }
}
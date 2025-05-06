using UnityEngine;

namespace Props.Portal
{
    public class PortalEntry : MonoBehaviour
    {
        public static readonly int IsFrontOpen = Animator.StringToHash("isFrontOpen");
        public static readonly int IsBackOpen = Animator.StringToHash("isBackOpen");
        
        [SerializeField] private string _portalID;
        [SerializeField] private ObjectDetectorByTag _frontDetector;
        [SerializeField] private ObjectDetectorByTag _backDetector;
        [SerializeField] private PortalExit _portalExit;
        
        public string portalID => _portalID;
        
        private Animator _animator;

        private void Awake()
        {
            _animator = GetComponent<Animator>();
        }

        private void Update()
        {
            if(_backDetector.ObjectDetected)
            {
                _animator.SetBool(IsFrontOpen, true);
                _portalExit.OpenFront();
            }
            else 
                _animator.SetBool(IsFrontOpen, false);
            if(_frontDetector.ObjectDetected)
            {
                _animator.SetBool(IsBackOpen, true);
                _portalExit.OpenBack();
            }
            else 
                _animator.SetBool(IsBackOpen, false);
        }
    }
}

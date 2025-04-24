using UnityEngine;

namespace Props.Portal
{
    public class Portal : MonoBehaviour
    {
        public static readonly int IsFrontOpen = Animator.StringToHash("isFrontOpen");
        public static readonly int IsBackOpen = Animator.StringToHash("isBackOpen");
        
        [SerializeField] private string _portalID;
        [SerializeField] private ObjectDetectorByTag _frontDetector;
        [SerializeField] private ObjectDetectorByTag _backDetector;
        
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
            }
            else 
                _animator.SetBool(IsFrontOpen, false);
            if(_frontDetector.ObjectDetected)
            {
                _animator.SetBool(IsBackOpen, true);
            }
            else 
                _animator.SetBool(IsBackOpen, false);
        }
    }
}

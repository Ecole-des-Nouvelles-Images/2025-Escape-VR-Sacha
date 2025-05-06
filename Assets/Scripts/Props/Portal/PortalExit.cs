using UnityEngine;

namespace Props.Portal
{
    public class PortalExit : MonoBehaviour
    {
        public static readonly int IsFrontOpenExit = Animator.StringToHash("isFrontOpenExit");
        public static readonly int IsBackOpenExit = Animator.StringToHash("isBackOpenExit");
        
        [SerializeField] private ObjectDetectorByTag _mainCamDetector;
        [SerializeField] private Transform _tpTarget;
        
        public Transform TpTarget { get { return _tpTarget; } }
        
        private Animator _animator;
        private float _timerBeforDissolve = .5f;
        private bool _isTimerOn;
        
        private void Awake()
        {
            _animator = GetComponent<Animator>();
        }
        
        
        // Update is called once per frame
        void Update()
        {
            if (_mainCamDetector.ObjectDetected == false)
            {
                _animator.SetBool(IsFrontOpenExit, false);
                _animator.SetBool(IsBackOpenExit, false);
                _isTimerOn = true;
            }

            if (_isTimerOn)
            {
                _timerBeforDissolve -= Time.deltaTime;
            }

            if (_timerBeforDissolve <= 0)
            {
                gameObject.SetActive(false); //will be replaced by dissolve fx
            }
        }

        public void OpenFront()
        {
            _animator.SetBool(IsBackOpenExit, false);
            _animator.SetBool(IsFrontOpenExit, true);
        }
        public void OpenBack()
        {
            _animator.SetBool(IsFrontOpenExit, false);
            _animator.SetBool(IsBackOpenExit, true);
        }
    }
}

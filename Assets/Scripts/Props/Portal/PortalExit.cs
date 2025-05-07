using System;
using UnityEngine;

namespace Props.Portal
{
    public class PortalExit : MonoBehaviour
    {
        public static readonly int IsFrontOpenExit = Animator.StringToHash("isOpenFrontExit");
        public static readonly int IsBackOpenExit = Animator.StringToHash("isOpenBackExit");
        
        [SerializeField] private ObjectDetectorByTag _mainCamDetector;
        [SerializeField] private Transform _tpTarget;
        [SerializeField] private GameObject _openFront;
        [SerializeField] private GameObject _openBack;
        
        public Transform TpTarget { get { return _tpTarget; } }
        
        private Animator _animatorFront;
        private Animator _animatorBack;
        private float _timerBeforDissolve = 1.5f;
        private bool _isTimerOn;
        private bool _isAfterTp;
        
        private void Awake()
        {
            _animatorFront = _openFront.transform.GetComponent<Animator>();
            _animatorBack = _openBack.transform.GetComponent<Animator>();
            
            _openFront.SetActive(false);
            _openBack.SetActive(false);
        }

        private void Start()
        {
            _animatorFront.SetBool(IsFrontOpenExit, true);
            _animatorBack.SetBool(IsBackOpenExit, true);
        }

        // Update is called once per frame
        void Update()
        {
            
            if (_isAfterTp == false && _mainCamDetector.ObjectDetected)
            {
                _isAfterTp = true;
            }
            if (_mainCamDetector.ObjectDetected == false && _isAfterTp)
            {
                if(_openFront.activeSelf)
                    _animatorFront.SetBool(IsFrontOpenExit, false);
                if(_openBack.activeSelf)
                    _animatorBack.SetBool(IsBackOpenExit, false);
                _isTimerOn = true;
            }
            if (_isTimerOn)
            {
                _timerBeforDissolve -= Time.deltaTime;
            }
            if (_timerBeforDissolve <= 0 && _isAfterTp )
            {
                gameObject.SetActive(false); //will be replaced by dissolve fx
            }
        }

        public void OpenFront()
        {
            _openFront.SetActive(true);
        }
        public void OpenBack()
        {
            _openBack.SetActive(true);
        }
    }
}

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
        [SerializeField] private AudioSource _portalAudio;
        [SerializeField] private AudioClip _openDoorSound;
        [SerializeField] private AudioClip _closeDoorSound;
        
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
                if (_animator.GetBool(IsBackOpen) == false)
                {
                    _animator.SetBool(IsBackOpen, true);
                    _portalExit.OpenFront();
                    if (_openDoorSound && _portalAudio.isPlaying == false)
                    {
                        _portalAudio.clip = _openDoorSound;
                        _portalAudio.Play();
                    }
                }
            }
            else if(_animator.GetBool(IsBackOpen))
            {
                _animator.SetBool(IsBackOpen, false);
                if (_closeDoorSound && _portalAudio.isPlaying == false)
                {
                    _portalAudio.clip = _closeDoorSound;
                    _portalAudio.Play();
                }
            }
            if(_frontDetector.ObjectDetected)
            {
                if (_animator.GetBool(IsFrontOpen) == false)
                {
                    _animator.SetBool(IsFrontOpen, true);
                    _portalExit.OpenBack();
                    if (_openDoorSound && _portalAudio.isPlaying == false)
                    {
                        _portalAudio.clip = _openDoorSound;
                        _portalAudio.Play();
                    }
                }
            }
            else if(_animator.GetBool(IsFrontOpen))
            {
                _animator.SetBool(IsFrontOpen, false);
                if (_closeDoorSound && _portalAudio.isPlaying == false)
                {
                    _portalAudio.clip = _closeDoorSound;
                    _portalAudio.Play();
                }
            }
        }
    }
}

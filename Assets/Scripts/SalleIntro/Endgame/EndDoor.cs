using System;
using UnityEngine;
using Utils;

namespace SalleIntro.Endgame
{
    public class EndDoor : MonoBehaviour
    {
        public static readonly int IsFrontOpen = Animator.StringToHash("isFrontOpen");
        
        [SerializeField] private GameObject _goodEndDoor;
        [SerializeField] private GameObject _badEndDoor;

        private Animator _goodDoorAnimator;
        private Animator _badDoorAnimator;
        private bool _isTimerOn;
        private bool _isGoodEnd;
        private float _time = 1f;

        private void OnEnable()
        {
            GameEvents.OnEnd += OpenDoor;
        }

        private void OnDisable()
        {
            GameEvents.OnEnd -= OpenDoor;
        }

        private void Awake()
        {
            _goodDoorAnimator = _goodEndDoor.GetComponent<Animator>();
            _badDoorAnimator = _badEndDoor.GetComponent<Animator>();
        }

        private void Update()
        {
            if(_isTimerOn)
                _time -= Time.deltaTime;
            if (_time <= 0)
            {
                GameEvents.OnDoorOpened?.Invoke(_isGoodEnd);
            }
        }

        private void OpenDoor(bool isGoodEnd)
        {
            _isGoodEnd = isGoodEnd;
            if (isGoodEnd)
            {
                _badEndDoor.SetActive(false);
                _goodEndDoor.SetActive(true);
                _goodDoorAnimator.SetBool(IsFrontOpen, true);
            }
            else
            {
                _goodEndDoor.SetActive(false);
                _badEndDoor.SetActive(true);
                _badDoorAnimator.SetBool(IsFrontOpen, true);
            }

            _isTimerOn = true;
        }
    }
}

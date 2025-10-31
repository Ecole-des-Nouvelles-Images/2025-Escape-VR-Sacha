using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using Utils;

namespace SalleIntro.Endgame
{
    public class EndGame : MonoBehaviour
    {
        [SerializeField] private GameObject _endGameObject;
        [SerializeField] private string _myRoomID;
        [SerializeField] private float _timeBeforeTpInCredit;
        [SerializeField] private string _badEndFaderID;
        [SerializeField] private string _goodEndFaderID;

        private bool _isTimerOn;
        private float _timer;
        private bool _isGoodEnd;

        private void OnEnable()
        {
            GameEvents.OnDoorOpened += EndPlayer;
        }

        private void OnDisable()
        {
            GameEvents.OnDoorOpened -= EndPlayer;
        }

        private void Awake()
        {
            _endGameObject.SetActive(false);
            _timer = _timeBeforeTpInCredit;
        }

        private void Update()
        {
            if (_isTimerOn)
            {
                _timer -= Time.deltaTime;
            }
            if (_timer <= 0 && _isGoodEnd)
            {
                SceneManager.LoadScene("CreditsGoodEnd");
            }
            else if (_timer <= 0 && _isGoodEnd == false)
            {
                SceneManager.LoadScene("CreditsBadEnd");
            }
        }

        public void CallEnd()
        {
            GameEvents.OnEndGame?.Invoke();
        }

        private void EndPlayer(bool isWin)
        {
            _isGoodEnd = isWin;
            if (isWin)
            {
                GameEvents.OnFadeScreen(_goodEndFaderID, false);
            }
            else
            {
                GameEvents.OnFadeScreen(_badEndFaderID, false);
            }
            _isTimerOn = true;
        }
    }
}

using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.XR.Interaction.Toolkit.Interactables;
using Utils;

namespace Puzzles.LivingRoom
{
    public class PuzzleLivingRoom : Puzzle
    {
        public static readonly int IsCakeOpen = Animator.StringToHash("isCakeOpen");
        public static readonly int NextOpen = Animator.StringToHash("NextOpen");
        
        [SerializeField] private GameObject[] _candles;
        [SerializeField] private GameObject _lettre;
        [SerializeField] private GameObject[] _candlesSockets;
        [SerializeField] private Animator _cakeAnimator;
        [SerializeField] private Animator _commodeAnimator;
        [SerializeField] private AudioSource _successAudioSource;
        [SerializeField] private int[] _firstCode;
        [SerializeField] private int[] _secondCode;
        [SerializeField] private int[] _thirdCode;
        [SerializeField] private int[] _bonusCode;
        
        private int _candleCount;
        private int _puzzleStates; //1-3 + 1 (bonus)
        private int[] _currentCode; // 0=empty, 1=0, 2=1, etc...
        private string _debugCode;

        private void Awake()
        {
            _lettre.SetActive(false);
            _currentCode = new int[3];
            _puzzleStates = 1;
            if(_candlesSockets[1].activeSelf)
                _candlesSockets[1].SetActive(false);
            for (int i = 0; i < _candles.Length; i++)
            {
                if (i != 1 && i != 8)
                {
                    _candles[i].SetActive(false);
                }
            }
            _cakeAnimator.SetBool(IsCakeOpen, false);
        }

        private void Start()
        {
            GameEvents.OnActualizeClue.Invoke("LivingRoom",0);
            LockPortal();
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("KeyObject"))
            {
                _cakeAnimator.SetBool(IsCakeOpen, false);
            }
        }

        private void CodeValidation()
        {
            _debugCode = "";
            for (int i = 0; i < _currentCode.Length; i++)
            {
                _debugCode += _currentCode[i].ToString();
            }
            Debug.Log(_debugCode);
            switch (_puzzleStates)
            {
                case 1:
                    if (!Helper.IntArrayEquals(_currentCode,_firstCode))
                        return;
                    _successAudioSource.Play();
                    _puzzleStates = 2;
                    _candlesSockets[1].SetActive(true);
                    _candles[0].SetActive(true);
                    _candles[2].SetActive(true);
                    _candles[4].SetActive(true);
                    _candles[6].SetActive(true);
                    _commodeAnimator.SetTrigger(NextOpen);
                    GameEvents.OnActualizeClue.Invoke("LivingRoom",1);
                    Debug.Log("passage à l'étape : " + _puzzleStates);
                    break;
                case 2:
                    if (!Helper.IntArrayEquals(_currentCode,_secondCode))
                        return;
                    _successAudioSource.Play();
                    _puzzleStates = 3;
                    _candles[3].SetActive(true);
                    _candles[5].SetActive(true);
                    _candles[7].SetActive(true);
                    _candles[9].SetActive(true);
                    _commodeAnimator.SetTrigger(NextOpen);
                    GameEvents.OnActualizeClue.Invoke("LivingRoom",2);
                    break;
                case 3:
                    if (!Helper.IntArrayEquals(_currentCode,_thirdCode))
                        return;
                    _successAudioSource.Play();
                    _puzzleStates = 4;
                    _lettre.SetActive(true);
                    _cakeAnimator.SetBool(IsCakeOpen, true);
                    GameEvents.OnActualizeClue.Invoke("LivingRoom",3);
                    _commodeAnimator.SetTrigger(NextOpen);
                    UnlockPortal();
                    break;
                case 4:
                    if (!Helper.IntArrayEquals(_currentCode,_bonusCode))
                        return;
                    _successAudioSource.Play();
                    GameEvents.OnIncreaseScore.Invoke();
                    break;
            }
        }

        public void AddValueInCode(int value, int index)
        {
            _currentCode[index] = value;
            CodeValidation();
        }
        public void RemoveValueInCode(int index)
        {
            _currentCode[index] = 0;
            CodeValidation();
        }
    }
}

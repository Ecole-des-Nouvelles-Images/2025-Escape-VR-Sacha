using System;
using System.Collections.Generic;
using UnityEngine;
using Utils;

namespace Puzzles.LivingRoom
{
    public class PuzzleLivingRoom : Puzzle
    {
        [SerializeField] private GameObject[] _candles;
        [SerializeField] private GameObject[] _smartphones;
        [SerializeField] private CandlesSockets[] _candlesSockets;
        [SerializeField] private int[] _firstCode;
        [SerializeField] private int[] _secondCode;
        [SerializeField] private int[] _thirdCode;
        [SerializeField] private int[] _bonusCode;
        
        private int _candleSocketsStates;
        private int _puzzleStates; //1-3 + 1 (bonus)
        private int[] _currentCode; // 0=empty, 1=0, 2=1, etc...
        private string _debugCode;

        private void Awake()
        {
            _currentCode = new int[5];
            _puzzleStates = 1;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Candle"))
            {
                CandleEnter(other.transform.GetComponent<Candle>());
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("Candle"))
            {
                CandleExit(other.transform.GetComponent<Candle>());
            }
        }

        private void Update()
        {
            ActualiseSocketsPreset();
        }

        private void CandleEnter(Candle candle)
        {
            if (_candleSocketsStates >= 0 && _candleSocketsStates < 5)
            {
                _currentCode[_candleSocketsStates] = candle.MyValue;
                Debug.Log("CandleEnter =>"+ candle.MyValue);
                candle.transform.parent = _candlesSockets[_candleSocketsStates].transform.GetChild(_candleSocketsStates);
                candle.transform.position = Vector3.zero;
                candle.transform.rotation = Quaternion.identity;
                candle.transform.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation;
                candle.transform.GetComponent<Rigidbody>().constraints =
                    RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionZ;
                _candleSocketsStates++;
                CodeValidation();
            }
        }
        private void CandleExit(Candle candle)
        {
            for (int i = 0; i < _currentCode.Length; i++)
            {
                if (candle.MyValue == _currentCode[i])
                {
                    _currentCode[i] = 0;
                    Debug.Log(candle.MyValue + " => CandleExit");
                    candle.transform.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
                    _candleSocketsStates--;
                    CodeValidation();
                }
            }
        }

        private void CodeValidation()
        {
            switch (_puzzleStates)
            {
                case 1:
                    if (!Helper.IntArrayEquals(_currentCode,_firstCode))
                        return;
                    _puzzleStates++;
                    Debug.Log("passage à l'étape : " + _puzzleStates);
                    break;
                case 2:
                    if (!Helper.IntArrayEquals(_currentCode,_secondCode))
                        return;
                    _puzzleStates++;
                    break;
                case 3:
                    if (!Helper.IntArrayEquals(_currentCode,_thirdCode))
                        return;
                    _puzzleStates++;
                    UnlockPortal();
                    break;
                case 4:
                    if (!Helper.IntArrayEquals(_currentCode,_bonusCode))
                        return;
                    //increase alternative score
                    break;
            }
            _debugCode = "";
            for (int i = 0; i < _currentCode.Length; i++)
            {
                _debugCode += _currentCode[i].ToString();
            }
            Debug.Log(_debugCode);
        }

        private void ActualiseSocketsPreset()
        {
            for (int i = 0; i < _candlesSockets.Length; i++)
            {
                if (i == _candleSocketsStates && !_candlesSockets[i].gameObject.activeSelf)
                {
                    _candlesSockets[i].gameObject.SetActive(true);
                }
                else
                {
                    _candlesSockets[i].gameObject.SetActive(false);
                }
            }
        }
    }
}

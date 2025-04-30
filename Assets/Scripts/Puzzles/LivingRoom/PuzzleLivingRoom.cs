using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Interactables;
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
        
        private int _candleCount;
        private int _puzzleStates; //1-3 + 1 (bonus)
        private int[] _currentCode; // 0=empty, 1=0, 2=1, etc...
        private string _debugCode;

        private void Awake()
        {
            _candleCount = 0;
            _currentCode = new int[5];
            _puzzleStates = 1;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Candle"))
            {
                
                Debug.Log("contact =>"+ other.name);
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

        private void CandleEnter(Candle candle)
        {
            candle.transform.GetComponent<XRGrabInteractable>().enabled = false;
            _currentCode[_candleCount] = candle.MyValue;
            Debug.Log("CandleEnter =>"+ candle.MyValue);
            _candlesSockets[_candleCount].AddCandle(candle.gameObject, _candleCount);
            //ActualiseSocketsPreSet();
            //candle.transform.parent = _candlesSockets[_candleSocketsStates].transform.GetChild(_candleSocketsStates);
            //_candlesSockets[_candleCount-1].gameObject.SetActive(true);
            // if (_candleCount > 1)
            // {
            //     MoveCandleToActualPreset(true);
            // }
            //_candlesSockets[_candleCount].RefreshCandles();
            //CodeValidation();
            /*if (_candleCount is >= 0 and < 5)
            {
                
            }*/
            candle.transform.GetComponent<XRGrabInteractable>().enabled = true;
            
            _candleCount ++;
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
                    _candleCount-=1;
                    ActualiseSocketsPreSet();
                    MoveCandleToActualPreset(false);
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
        private void ActualiseSocketsPreSet()
        {
            for (int i = 0; i < _candlesSockets.Length; i++)
            {
                if (i != _candleCount)
                {
                    _candlesSockets[i].gameObject.SetActive(false);
                }
                else
                {
                    _candlesSockets[i].gameObject.SetActive(true);
                }
            }
        }
        private void MoveCandleToActualPreset(bool toNextPreset)
        {
            if (_candleCount > 1 && toNextPreset)
            {
                _candlesSockets[_candleCount-2].TransferCandles(_candlesSockets[_candleCount-1]);
                Debug.Log("MoveCandleToNextPreset =>" + _candleCount);
                // for (int i = 0; i < _candleCount; i++)
                // {
                //     _candlesSockets[_candleCount - 1].transform.GetChild(i).transform.GetChild(0).transform
                //             .parent =
                //         _candlesSockets[_candleCount].transform.GetChild(i).transform;
                // }
            }
            if (_candleCount < 5 && toNextPreset == false)
            {
                for (int i = 0; i <= _candleCount; i++)
                {
                    _candlesSockets[_candleCount + 1].transform.GetChild(i).transform.GetChild(0).transform
                            .parent =
                        _candlesSockets[_candleCount].transform.GetChild(i).transform;
                }
                Debug.Log("MoveCandleToLastPreset =>" + _candleCount);
            }
        }

        // private void RefreshCandlesInSocketPreset(CandlesSockets candlesSockets)
        // {
        //     for (int i = 0; i < candlesSockets.transform.; i++)
        //     {
        //         
        //     }
        // }
    }
}

using System;
using System.Collections.Generic;
using UnityEngine;

namespace Puzzles.LivingRoom
{
    public class PuzzleParty : Puzzle
    {
        [SerializeField] private GameObject[] _candles;
        [SerializeField] private GameObject[] _smartphones;
        [SerializeField] private CandlesSockets[] _candlesSockets;
        [SerializeField] private int[] _firstCode;
        [SerializeField] private int[] _secondCode;
        [SerializeField] private int[] _thirdCode;
        [SerializeField] private int[] _bonusCode;
        
        private int _candleSocketsStates;
        private List<bool> _puzzleStates = new List<bool>(); //will add true when code is true
        private int[] _currentCode; // 0=empty, 1=0, 2=1, etc...

        private void Awake()
        {
            _currentCode = new int[5];
        }

        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
        
        }

        // Update is called once per frame
        void Update()
        {
        
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

        private void CandleEnter(Candle candle)
        {
            if (_candleSocketsStates >= 0 && _candleSocketsStates < 5)
            {
                _currentCode[_candleSocketsStates] = candle.MyValue;
                Debug.Log("CandleEnter =>"+ candle.MyValue);
                for (int i = 0; i < _candlesSockets.Length; i++)
                {
                    if (i == _candleSocketsStates)
                    {
                        _candlesSockets[i].gameObject.SetActive(true);
                    }
                    else
                    {
                        _candlesSockets[i].gameObject.SetActive(false);
                    }
                }
                candle.transform.position = Vector3.zero;
                candle.transform.rotation = Quaternion.identity;
                candle.transform.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation;
                _candleSocketsStates++;
                CodeValidation();
            }
        }
        private void CandleExit(Candle candle)
        {
            _currentCode[_candleSocketsStates] = 0;
            Debug.Log("CandleEnter =>"+ candle.MyValue);
            candle.transform.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
            _candleSocketsStates--;
            CodeValidation();
        }

        private void CodeValidation()
        {
            string x = "";
            for (int i = 0; i < _currentCode.Length; i++)
            {
                x += _currentCode[i].ToString();
                
            }
            Debug.Log(x);
        }
    }
}

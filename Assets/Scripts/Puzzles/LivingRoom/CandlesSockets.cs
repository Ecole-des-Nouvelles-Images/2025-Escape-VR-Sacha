using System.Collections.Generic;
using UnityEngine;

namespace Puzzles.LivingRoom
{
    public class CandlesSockets : MonoBehaviour
    {
        [SerializeField] private GameObject[] _candlesSocket;
        
        private List<GameObject> _candles = new List<GameObject>();
        public void AddCandle(GameObject candle, int position)
        {
            if (_candlesSocket[position].transform.childCount == 0)
            {
                candle.transform.SetParent(_candlesSocket[position].transform);
            }
            _candles.Add(candle);
        }

        public void RemoveCandle(GameObject candle)
        {
            _candles.Remove(candle);
        }

        public void RefreshCandles()
        {
            
            for (int i = 0; i < _candlesSocket.Length; i++)
            {
                if (_candlesSocket[i].transform.childCount != 0)
                {
                    Debug.Log(_candlesSocket[i].transform.GetChild(0).gameObject.name);
                    _candlesSocket[i].transform.GetChild(0).transform.position = _candlesSocket[i].transform.GetChild(0).transform.parent.position;
                    _candlesSocket[i].transform.GetChild(0).transform.rotation = new Quaternion(0, 0, 0, 0);
                    _candlesSocket[i].transform.GetChild(0).transform.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation;
                    _candlesSocket[i].transform.GetChild(0).transform.GetComponent<Rigidbody>().constraints =
                        RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionZ; 
                }
            }
                
        }

        public void TransferCandles(CandlesSockets candlesSocket)
        {
            for (int i = 0; i < _candlesSocket.Length; i++)
            {
                candlesSocket.AddCandle(_candles[i],i);
                RemoveCandle(_candles[i]);
            }
            
        }
    }
}

using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace Puzzles.LivingRoom
{
    public class CandleSocket : MonoBehaviour
    {
        [SerializeField] private PuzzleLivingRoom _myPuzzle;
        [SerializeField] private int _myIndexInCode;

        private GameObject _myCandle;
        private bool _isCandleOn;

        private void Awake()
        {
            _myCandle = null;
        }

        private void FixedUpdate()
        {
            if (_isCandleOn == false && _myCandle &&_myCandle.transform.parent == transform)
            {
                _myCandle.transform.SetParent(_myPuzzle.transform);
                _myCandle = null;
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Candle") && _myCandle == null)
            {
                _myCandle = other.gameObject;
                _myCandle.transform.parent = transform;
                _isCandleOn = true;
                RefreshCandle();
                _myPuzzle.AddValueInCode(_myCandle.GetComponent<Candle>().MyValue,_myIndexInCode);
            }
        }
        private void OnTriggerExit(Collider other)
        {
            if (_myCandle != null && other.gameObject == _myCandle)
            {
                _myPuzzle.RemoveValueInCode(_myIndexInCode);
                _myCandle.GetComponent<Candle>().FXDisable();
                _isCandleOn = false;
                _myCandle = null;
            }
        }

        public void RefreshCandle()
        {
            _myCandle.transform.position = transform.position;
            _myCandle.transform.rotation = transform.rotation;
            _myCandle.transform.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation;
            _myCandle.transform.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionZ;
            _myCandle.GetComponent<Candle>().FXEnable();
        }
    }
}

using System;
using UnityEngine;

namespace Puzzles.LivingRoom
{
    public class Candle : MonoBehaviour
    {
        [SerializeField] private int _myValue;
        [SerializeField] private GameObject _myFX;
        public int MyValue
        {
            get => _myValue;
        }

        private void Awake()
        {
            FXDisable();
        }

        public void FXEnable()
        {
            if(_myFX.activeSelf == false)
                _myFX.SetActive(true);
        }
        public void FXDisable()
        {
            if(_myFX.activeSelf)
                _myFX.SetActive(false);
        }
    }
}

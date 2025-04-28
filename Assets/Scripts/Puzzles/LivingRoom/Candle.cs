using System;
using UnityEngine;

namespace Puzzles.LivingRoom
{
    public class Candle : MonoBehaviour
    {
        [SerializeField] int _myValue;
        public int MyValue
        {
            get => _myValue;
        }
    }
}

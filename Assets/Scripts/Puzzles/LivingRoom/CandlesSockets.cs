using UnityEngine;

namespace Puzzles.LivingRoom
{
    public class CandlesSockets : MonoBehaviour
    {
        [SerializeField] private GameObject[] _candlesSocket;
        
        public void AddCandle(GameObject candle, int position)
        {
            
            if (_candlesSocket[position].transform.childCount == 0)
            {
                candle.transform.SetParent(_candlesSocket[position].transform);
            }
        }
    }
}

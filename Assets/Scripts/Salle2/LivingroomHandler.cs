using Puzzles;
using UnityEngine;

namespace Salle2
{
    public class LivingroomHandler : Puzzle
    {
        [SerializeField] private GameObject[] _emplacements;
        [SerializeField] private GameObject _lettre;
        
        
        
        public void BallonTouched(int ballonCode)
        {
        Debug.unityLogger.Log("BallonTouched", ballonCode);
        }
        
    }
}

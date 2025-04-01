using KeyboardAndScreen;
using UnityEngine;

namespace PuzzleCorridor
{
    public class ThirdPuzzleCorridor : PuzzleCorridor
    {
        [SerializeField] private SystemAndScreen _systemAndScreen;
        
        void Update()
        {
            if (_systemAndScreen && _systemAndScreen.IsUnlock)
            {
                UnlockPortal();
            }
        }
    }
}

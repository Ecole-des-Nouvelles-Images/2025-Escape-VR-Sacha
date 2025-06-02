using UnityEngine;
using Utils;

namespace Props
{
    public class BoomBoxCommand : MonoBehaviour
    {
        [SerializeField] private string _myBoomboxID;
        public void Play()
        {
            GameEvents.OnBoomboxInput.Invoke(_myBoomboxID, 1);
        }
        public void NextSong()
        {
            GameEvents.OnBoomboxInput.Invoke(_myBoomboxID, 3);
        }

        public void PreviousSong()
        {
            GameEvents.OnBoomboxInput.Invoke(_myBoomboxID, 4);
        }
    }
}
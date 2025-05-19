using UnityEngine;
using UnityEngine.Serialization;

namespace SalleIntro
{
    public class TileComponent : MonoBehaviour
    {
        [FormerlySerializedAs("introHandler")] [SerializeField] private IntroHandler _introHandler;

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                //_introHandler.PlayerMovedToTarget();
            }
        }
    }
}
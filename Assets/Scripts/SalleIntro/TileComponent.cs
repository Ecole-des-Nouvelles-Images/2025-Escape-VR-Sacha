using UnityEngine;

namespace SalleIntro
{
    public class TileComponent : MonoBehaviour
    {
        [SerializeField] private IntroHandler _introHandler;

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                _introHandler.OnPlayerStepOnTile();
            }
        }
    }
}
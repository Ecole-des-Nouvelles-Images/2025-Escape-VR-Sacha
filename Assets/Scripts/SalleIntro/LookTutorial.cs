using UnityEngine;

namespace SalleIntro
{
    public class LookTutorial : MonoBehaviour
    {
        /*[SerializeField] private Camera _playerCamera;
        [SerializeField] private float _lookDistance = 30f;
        [SerializeField] private LayerMask _screenLayer;
        [SerializeField] private IntroHandler _introHandler;

        private void Update()
        {
            Ray ray = new Ray(_playerCamera.transform.position, _playerCamera.transform.forward);
            if (Physics.Raycast(ray, out RaycastHit hit, _lookDistance, _screenLayer))
            {
                var screen = hit.collider.GetComponent<ScreenComponent>();
                if (screen != null)
                {
                    _introHandler.RegisterScreenLook(screen);
                }
            }
        }*/
    }
}
using System;
using Manager;
using UnityEngine;

public class BackMenuButton : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("KeyObject"))
        {
            SceneLoader.OnLoad.Invoke("MainMenu");
        }
    }
}

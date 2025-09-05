using System;
using Manager;
using UnityEngine;

public class RunGameButton : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("KeyObject"))
        {
            SceneLoader.OnLoad.Invoke("Integration");
        }
    }
}

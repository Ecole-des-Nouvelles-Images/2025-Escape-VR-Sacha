using System;
using UnityEngine;

public class MainCamDetector : MonoBehaviour
{
    public bool CamDetected;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("MainCamera"))
        {
            CamDetected = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("MainCamera"))
        {
            CamDetected = false;
        }
    }
}

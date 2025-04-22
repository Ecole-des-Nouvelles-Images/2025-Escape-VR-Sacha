using System;
using UnityEngine;

public class ObjectDetectorByTag : MonoBehaviour
{
    public bool CamDetected;
    [SerializeField]private string _tagName;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(_tagName))
        {
            CamDetected = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(_tagName))
        {
            CamDetected = false;
        }
    }
}

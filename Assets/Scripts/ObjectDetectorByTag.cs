using System;
using UnityEngine;
using UnityEngine.Serialization;

public class ObjectDetectorByTag : MonoBehaviour
{
    [FormerlySerializedAs("CamDetected")] public bool ObjectDetected;
    [SerializeField]private string _tagName;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(_tagName))
        {
            ObjectDetected = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(_tagName))
        {
            ObjectDetected = false;
        }
    }
}

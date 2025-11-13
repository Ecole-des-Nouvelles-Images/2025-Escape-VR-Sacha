using System.Collections.Generic;
using UnityEngine;

public class NumepadKeyPhysicControler : MonoBehaviour
{
    public enum Axes
    {
        X,
        Y,
        Z
    }
    [SerializeField] private Axes _axes = Axes.X;
    [SerializeField] private Rigidbody[] _keyPhysicList;

    public Axes axes { get; private set ;}

    private List<Vector3> _keysOriginPosition = new List<Vector3>();
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        switch (_axes)
        {
            case Axes.X:
                foreach (Rigidbody keyPhysic in _keyPhysicList)
                {
                    keyPhysic.constraints = RigidbodyConstraints.None;
                    keyPhysic.constraints = RigidbodyConstraints.FreezeRotation;
                    keyPhysic.constraints = RigidbodyConstraints.FreezePositionY;
                    keyPhysic.constraints = RigidbodyConstraints.FreezePositionZ;
                }
                break;
            case Axes.Y:
                foreach (Rigidbody keyPhysic in _keyPhysicList)
                {
                    keyPhysic.constraints = RigidbodyConstraints.None;
                    keyPhysic.constraints = RigidbodyConstraints.FreezeRotation;
                    keyPhysic.constraints = RigidbodyConstraints.FreezePositionX;
                    keyPhysic.constraints = RigidbodyConstraints.FreezePositionZ;
                }
                break;
            case Axes.Z:
                foreach (Rigidbody keyPhysic in _keyPhysicList)
                {
                    keyPhysic.constraints = RigidbodyConstraints.None;
                    keyPhysic.constraints = RigidbodyConstraints.FreezeRotation;
                    keyPhysic.constraints = RigidbodyConstraints.FreezePositionX;
                    keyPhysic.constraints = RigidbodyConstraints.FreezePositionY;
                }
                break;
        }
    }

    private void Update()
    {
        for (int i = 0; i < _keysOriginPosition.Count; i++)
        {
            
        }
    }
}

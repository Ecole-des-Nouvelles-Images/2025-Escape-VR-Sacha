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
}

using UnityEngine;
using Utils;

public class TestEndgame : MonoBehaviour
{
    public void ActiveEndgame()
    {
        GameEvents.OnRoomChanged.Invoke("R1");
    }
}

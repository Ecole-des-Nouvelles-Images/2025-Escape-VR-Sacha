using UnityEngine;
using UnityEngine.Serialization;

namespace SalleIntro
{
    public class ScreenComponent : MonoBehaviour
    {
        [FormerlySerializedAs("targetIndex")] public int TargetIndex;
    }
}

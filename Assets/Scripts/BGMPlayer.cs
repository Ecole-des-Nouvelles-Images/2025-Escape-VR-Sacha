using UnityEngine;
using Utils;

public class BGMPlayer : MonoBehaviour
{
    [SerializeField] private string _bgmId;
    void Start()
    {
        GameEvents.OnPlayBGM.Invoke(_bgmId);
    }
}

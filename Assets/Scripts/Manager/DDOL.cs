using System;
using UnityEngine;

namespace Manager
{
    public class DDOL : MonoBehaviour
    {
        private void Awake()
        {
            DontDestroyOnLoad(this);
        }
    }
}

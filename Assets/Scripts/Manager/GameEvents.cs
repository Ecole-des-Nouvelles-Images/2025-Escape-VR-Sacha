using System;
using UnityEngine;

namespace Manager
{
    public static class GameEvents
    {
        public static Action<string> OnRoomChanged;
        public static Action<bool> OnPortalTriggered;
    }
}

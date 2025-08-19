using System;
using UnityEngine;
using Utils;

namespace Manager
{
    public class PuzzleManager : MonoBehaviour
    {
        private void OnEnable()
        {
            GameEvents.OnOpenDrawer += HandleDrawer;
            GameEvents.OnOpenTrap += HandleTrap;
            GameEvents.OnOpenTeddy += HandleTeddy;
            GameEvents.OnUnlockFinalChest += HandleFinalChest;
        }

        private void OnDisable()
        {
            GameEvents.OnOpenDrawer -= HandleDrawer;
            GameEvents.OnOpenTrap -= HandleTrap;
            GameEvents.OnOpenTeddy -= HandleTeddy;
            GameEvents.OnUnlockFinalChest -= HandleFinalChest;
        }

        private void OnDestroy()
        {
            GameEvents.OnOpenDrawer -= HandleDrawer;
            GameEvents.OnOpenTrap -= HandleTrap;
            GameEvents.OnOpenTeddy -= HandleTeddy;
            GameEvents.OnUnlockFinalChest -= HandleFinalChest;
        }

        private void HandleDrawer() => Debug.Log("Drawer opens from PuzzleManager");
        private void HandleTrap() => Debug.Log("Trap opens from PuzzleManager");
        private void HandleTeddy() => Debug.Log("Teddy opens from PuzzleManager");
        private void HandleFinalChest() => Debug.Log("Final chest unlocks from PuzzleManager");
    }
}
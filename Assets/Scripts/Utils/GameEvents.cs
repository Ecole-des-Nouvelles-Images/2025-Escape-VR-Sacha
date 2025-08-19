  using System;
  using UnityEngine;

  namespace Utils
{
    public static class GameEvents
    {
#pragma warning disable UDR0001
        public static Action OnTeleport;

        public static Action OnIncreaseScore;
        public static Action OnEndGame;
        public static Action OnTriggerEndGame;
        public static Action OnEnableFarInteractor;
        public static Action OnDisableFarInteractor;
        public static Action OnOpenDrawer;
        public static Action OnOpenTrap;
        public static Action OnOpenTeddy;
        public static Action OnUnlockFinalChest;
        public static Action OnNextRoom;
        public static Action OnStopBGM;
        
        public static Action<bool> OnEnd;
        public static Action<bool> OnDoorOpened;
        public static Action<string> OnRoomChanged;
        
        public static Action<string> OnKeyboardUnlock;
        public static Action<string> OnSetupPuzzle;
        public static Action<string> OnPuzzleCompleted;
        public static Action<string> OnPlayBGM;
        
        public static Action<string,bool>  OnFadeScreen;
        
        public static Action<string, int> OnActualizeClue;
        public static Action<string, int> OnBoomboxInput;
        
        public static Action<string, float> OnSliderModified;
#pragma warning restore UDR0001
    }
}

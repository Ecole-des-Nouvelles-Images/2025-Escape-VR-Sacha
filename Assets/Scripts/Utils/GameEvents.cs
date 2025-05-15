  using System;
  using UnityEngine;

  namespace Utils
{
    public static class GameEvents
    {
        public static Action OnTeleport;
        public static Action OnIncreaseScore;
        public static Action OnEndGame;
        public static Action OnTriggerEndGame;
        
        public static Action<bool> OnEnd;
        public static Action<bool> OnDoorOpened;
        
        public static Action<string> OnRoomChanged;
        public static Action<string> OnKeyboardUnlock;
        public static Action<string> OnSetupPuzzle;
        public static Action<string> OnPuzzleCompleted;
        public static Action<string> OnPlayCutscene;
        
        public static Action<string,bool>  OnFadeScreen;
        
        public static Action<string, int> OnActualizeClue;
        
        public static Action<string, float> OnSliderModified;
        
        public static Action<string, bool, int, AudioSource> OnPlaySound;
    }
}

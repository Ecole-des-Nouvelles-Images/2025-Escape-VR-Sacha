  using System;

namespace Utils
{
    public static class GameEvents
    {
        public static Action<string> OnRoomChanged;
        public static Action<string> OnKeyboardUnlock;
        public static Action<string> OnSetupPuzzle;
        public static Action<string> OnPuzzleCompleted;
        public static Action<string> OnPlayCutscene;
        public static Action<bool> OnPortalTriggered;
        public static Action OnCandleInside;
        public static Action OnCandleOutside;
    }
}

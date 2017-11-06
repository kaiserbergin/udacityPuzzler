using System;

namespace Assets.PuzzlerVR.Scripts.MiniGames {
    public class PuzzlerMiniGameInputEventArgs : EventArgs {
        public InputResults InputResult { get; set; }
        public int InputID { get; set; }
    }
}

using System;
using System.Collections;
using System.Collections.Generic;

namespace Assets.PuzzlerVR.Scripts.MiniGames {
    public enum InputResults { PASS, FAIL, NEUTRAL }
    public class PuzzlerMiniGame {
        public event EventHandler PuzzlerMiniGameFailed;
        public event EventHandler PuzzlerMiniGameSolved;
        public event EventHandler<PuzzlerMiniGameInputEventArgs> PuzzlerInputReceived;

        protected virtual void OnPuzzlerMiniGameFailed() {
            if(PuzzlerMiniGameFailed != null) {
                PuzzlerMiniGameFailed(this, EventArgs.Empty);
            }
        }
        protected virtual void OnPuzzlerMiniGameSolved() {
            if(PuzzlerMiniGameSolved != null) {
                PuzzlerMiniGameSolved(this, EventArgs.Empty);
            }
        }
        protected virtual void OnPuzzlerInputReceived(InputResults inputResult, int inputId) {
            if(PuzzlerInputReceived != null) {
                PuzzlerInputReceived(
                    this, 
                    new PuzzlerMiniGameInputEventArgs() {
                        InputResult = inputResult,
                        InputID = inputId
                    }
                );
            }
        }
    }
}


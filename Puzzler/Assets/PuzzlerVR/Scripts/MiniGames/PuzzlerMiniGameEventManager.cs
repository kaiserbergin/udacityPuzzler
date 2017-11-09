using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.PuzzlerVR.Scripts.MiniGames {
    public class PuzzlerMiniGameEventManager : MonoBehaviour {
        public static PuzzlerMiniGameEventManager instance = null;
        public event EventHandler PuzzlerMiniGameFailed;
        public event EventHandler PuzzlerMiniGameSolved;
        public event EventHandler<PuzzlerMiniGameInputEventArgs> PuzzlerInputReceived;

        private void Awake() {
            if(instance == null) {
                instance = this;
            } else if (instance != this) {
                Destroy(gameObject);
            }
        }

        public virtual void OnPuzzlerMiniGameFailed(string miniGameId) {
            if (PuzzlerMiniGameFailed != null) {
                PuzzlerMiniGameFailed(this, new PuzzlerMiniGameEventArgs { MiniGameId = miniGameId });
            }
        }
        public virtual void OnPuzzlerMiniGameSolved(string miniGameId) {
            if (PuzzlerMiniGameSolved != null) {
                PuzzlerMiniGameSolved(this, new PuzzlerMiniGameEventArgs { MiniGameId = miniGameId });
            }
        }
        public virtual void OnPuzzlerInputReceived(string miniGameId, InputResults inputResult, int inputId) {
            if (PuzzlerInputReceived != null) {
                PuzzlerInputReceived(
                    this,
                    new PuzzlerMiniGameInputEventArgs() {
                        MiniGameId = miniGameId,
                        InputResult = inputResult,
                        InputID = inputId
                    }
                );
            }
        }
    }
}

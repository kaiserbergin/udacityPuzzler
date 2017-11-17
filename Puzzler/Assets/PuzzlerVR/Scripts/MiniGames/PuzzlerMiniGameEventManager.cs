using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.PuzzlerVR.Scripts.MiniGames {
    public interface IMiniGameEventManager {
        void OnMiniGameFailed(string miniGameId);
        void OnMiniGameSolved(string miniGameId);
        void OnMiniGameInputReceived(string miniGameId, InputResults inputResult, int inputId);
        void OnRequestMiniGamePlay(string miniGameId);
        void OnRequestMiniGameExit(string miniGameId);
    }
    public class PuzzlerMiniGameEventManager : MonoBehaviour, IMiniGameEventManager {
        public static PuzzlerMiniGameEventManager instance = null;
        public event EventHandler<PuzzlerMiniGameEventArgs> PuzzlerMiniGameFailed;
        public event EventHandler<PuzzlerMiniGameEventArgs> PuzzlerMiniGameSolved;
        public event EventHandler<PuzzlerMiniGameEventArgs> MiniGameRequestPlay;
        public event EventHandler<PuzzlerMiniGameEventArgs> MiniGameRequestExit;
        public event EventHandler<PuzzlerMiniGameInputEventArgs> PuzzlerInputReceived;
        public event EventHandler<PuzzlerMiniGameEventArgs> PuzzlerMiniGameRequested;

        private void Awake() {
            if(instance == null) {
                instance = this;
            } else if (instance != this) {
                Destroy(gameObject);
            }
        }

        public virtual void OnMiniGameFailed(string miniGameId) {
            if (PuzzlerMiniGameFailed != null) {
                PuzzlerMiniGameFailed(this, new PuzzlerMiniGameEventArgs { MiniGameId = miniGameId });
            }
        }
        public virtual void OnMiniGameSolved(string miniGameId) {
            if (PuzzlerMiniGameSolved != null) {
                PuzzlerMiniGameSolved(this, new PuzzlerMiniGameEventArgs { MiniGameId = miniGameId });
            }
        }
        public virtual void OnMiniGameInputReceived(string miniGameId, InputResults inputResult, int inputId) {
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

        public virtual void OnRequestMiniGamePlay(string miniGameId) {
            if(MiniGameRequestPlay != null) {
                MiniGameRequestPlay(this, new PuzzlerMiniGameEventArgs { MiniGameId = miniGameId });
            }
        }

        public virtual void OnRequestMiniGameExit(string miniGameId) {
            if (MiniGameRequestExit != null) {                
                MiniGameRequestExit(this, new PuzzlerMiniGameEventArgs { MiniGameId = miniGameId });
            }
        }

        public virtual void OnPuzzlerMiniGameRequested(string miniGameId) {
            if (PuzzlerMiniGameRequested != null) {
                PuzzlerMiniGameRequested(this, new PuzzlerMiniGameEventArgs { MiniGameId = miniGameId });
            }
        }
    }
}

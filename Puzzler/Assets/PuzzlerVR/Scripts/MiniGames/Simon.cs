using UnityEngine;

namespace Assets.PuzzlerVR.Scripts.MiniGames {
    public class Simon : PuzzlerMiniGame {
        private IMiniGameEventManager miniGameEventManager;
        private string miniGameId;
        private int[] inputIds;
        private int[] inputSequence;
        private int inputCount;
        private int inputSequenceCount;
        private int inputIndex;
        private int failureThreshold;
        private int failureCount;

        public Simon(IMiniGameEventManager miniGameEventManager, string miniGameId = "simon", int inputCount = 5, int inputSequenceCount = 5, int failureThreshold = 3) {
            this.miniGameEventManager = miniGameEventManager;
            this.miniGameId = miniGameId;
            this.inputCount = inputCount;
            this.inputSequenceCount = inputSequenceCount;
            this.failureThreshold = failureThreshold;
            GenerateInputIds();
            GenerateInputSequence();
        }

        public string GetMiniGameId() {
            return miniGameId;
        }

        public int[] GetInputIds() {
            return inputIds;
        }

        public int GetInputIndex() {
            return inputIndex;
        }

        public int[] GetInputSequence() {
            return inputSequence;
        }

        public int GetFailureThreshold() {
            return failureThreshold;
        }

        public int GetFailureCount() {
            return failureCount;
        }

        private void GenerateInputIds() {
            inputIds = new int[inputCount];
            for (int i = 0; i < inputCount; i++) {
                inputIds[i] = i;
            }
        }

        private void GenerateInputSequence() {
            inputSequence = new int[inputSequenceCount];
            ShuffleInputSequence();
        }

        private void ShuffleInputSequence() {
            for (int i = 0; i < inputSequenceCount; i++) {
                inputSequence[i] = inputIds[Random.Range(0, inputCount)];
            }
            inputIndex = 0;
            failureCount = 0;
        }

        public void VerifyInput(int inputId) {
            if(inputId == inputSequence[inputIndex]) {
                miniGameEventManager.OnMiniGameInputReceived(miniGameId, InputResults.PASS, inputId);
                inputIndex++;
                if(inputIndex == inputSequence.Length) {
                    miniGameEventManager.OnMiniGameSolved(miniGameId);
                }
            } else {
                miniGameEventManager.OnMiniGameInputReceived(miniGameId, InputResults.FAIL, inputId);
                failureCount++;
                inputIndex = 0;
                if(failureCount == failureThreshold) {
                    miniGameEventManager.OnMiniGameFailed(miniGameId);
                }
            }
        }

        public void ClearInputs() {
            inputIndex = 0;
        }

        public void Restart() {
            ShuffleInputSequence();
        }
    }
}

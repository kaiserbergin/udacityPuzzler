using UnityEngine;

namespace Assets.PuzzlerVR.Scripts.MiniGames {
    public class Simon : PuzzlerMiniGame {
        private string miniGameId;
        private int[] inputIds;
        private int[] inputSequence;
        private int inputCount;
        private int inputSequenceCount;
        private int inputIndex;
        private int failureThreshold;
        private int failureCount;

        public Simon(string miniGameId = "simon", int inputCount = 5, int inputSequenceCount = 5, int failureThreshold = 3) {
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
                PuzzlerMiniGameEventManager.instance.OnPuzzlerInputReceived(miniGameId, InputResults.PASS, inputId);
                inputIndex++;
                if(inputIndex == inputSequence.Length) {
                    PuzzlerMiniGameEventManager.instance.OnPuzzlerMiniGameSolved(miniGameId);
                }
            } else {
                PuzzlerMiniGameEventManager.instance.OnPuzzlerInputReceived(miniGameId, InputResults.FAIL, inputId);
                failureCount++;
                inputIndex = 0;
                if(failureCount == failureThreshold) {
                    PuzzlerMiniGameEventManager.instance.OnPuzzlerMiniGameFailed(miniGameId);
                }
            }
        }

        public void Restart() {
            ShuffleInputSequence();
        }
    }
}

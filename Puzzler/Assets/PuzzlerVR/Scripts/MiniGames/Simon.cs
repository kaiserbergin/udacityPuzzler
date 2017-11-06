using UnityEngine;

namespace Assets.PuzzlerVR.Scripts.MiniGames {
    public class Simon : PuzzlerMiniGame {
        private int[] inputIds;
        private int[] inputSequence;
        private int inputCount;
        private int inputSequenceCount;
        private int inputIndex;
        private int failureThreshold;
        private int failureCount;

        public Simon(int inputCount = 5, int inputSequenceCount = 5, int failureThreshold = 3) {            
            this.inputCount = inputCount;
            this.inputSequenceCount = inputSequenceCount;
            this.failureThreshold = failureThreshold;
            GenerateInputIds();
            GenerateInputSequence();
        }

        public int[] GetInputIds() {
            return inputIds;
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
                OnPuzzlerInputReceived(InputResults.PASS, inputId);
                inputIndex++;
                if(inputIndex == inputSequence.Length) {
                    OnPuzzlerMiniGameSolved();
                }
            } else {
                OnPuzzlerInputReceived(InputResults.FAIL, inputId);
                failureCount++;
                inputIndex = 0;
                if(failureCount == failureThreshold) {
                    OnPuzzlerMiniGameFailed();
                }
            }
        }

        public void Restart() {
            ShuffleInputSequence();
        }
    }
}

using UnityEngine;

namespace Assets.PuzzlerVR.Scripts.MiniGames {
    public class FindTheOne : PuzzlerMiniGame {
        private int miniGameId;
        private int[] inputIds;
        private int inputCount;
        private int theOneId;
        private float timeLimit;

        public FindTheOne(int miniGameId, int inputCount = 10) {
            this.miniGameId = miniGameId;
            this.inputCount = inputCount;
            inputIds = new int[inputCount];
            GenerateInputIds();
        }

        private void GenerateInputIds() {
            for(int i = 0; i < inputCount; i++) {
                inputIds[i] = i;
            }
            theOneId = Random.Range(0, inputCount);
        }

        public int[] GetInputIds() {
            return inputIds;
        }

        public void Restart() {
            GenerateInputIds();
        }

        public void InputReceived(int inputId) {
            if(inputId == theOneId) {
                OnPuzzlerInputReceived(InputResults.PASS, inputId);
            }
            else {
                OnPuzzlerInputReceived(InputResults.FAIL, inputId);
            }
        }

        public void ValidateTimePassed(float timePassed) {
            if(timePassed >= timeLimit) {
                OnPuzzlerMiniGameFailed();
            }
        }
    }
}

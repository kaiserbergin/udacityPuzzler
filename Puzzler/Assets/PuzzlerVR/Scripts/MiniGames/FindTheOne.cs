using UnityEngine;

namespace Assets.PuzzlerVR.Scripts.MiniGames {
    public class FindTheOne : PuzzlerMiniGame {
        private string miniGameId;
        private int[] inputIds;
        private int inputCount;
        private int theOneId;
        private float timeLimit;
        private int discoveryCountForWin;
        private int discoveries;

        public FindTheOne(string miniGameId = "findTheOne", int inputCount = 10, int discoveryCountForWin = 3) {
            this.miniGameId = miniGameId;
            this.inputCount = inputCount;
            this.discoveryCountForWin = discoveryCountForWin;
            inputIds = new int[inputCount];
            discoveries = 0;
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
                PuzzlerMiniGameEventManager.instance.OnPuzzlerInputReceived(miniGameId, InputResults.PASS, inputId);
                discoveries++;
                if(discoveries == discoveryCountForWin) {
                    PuzzlerMiniGameEventManager.instance.OnPuzzlerMiniGameSolved(miniGameId);
                }
            }
            else {
                PuzzlerMiniGameEventManager.instance.OnPuzzlerInputReceived(miniGameId, InputResults.FAIL, inputId);
            }
        }

        public void ValidateTimePassed(float timePassed) {
            if(timePassed >= timeLimit) {
                PuzzlerMiniGameEventManager.instance.OnPuzzlerMiniGameFailed(miniGameId);
            }
        }
    }
}

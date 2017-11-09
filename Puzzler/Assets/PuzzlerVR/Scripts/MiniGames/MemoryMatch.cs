using System.Collections.Generic;
using UnityEngine;

namespace Assets.PuzzlerVR.Scripts.MiniGames {
    public struct Coords {
        public int x;
        public int y;
        public int z;
    }

    public class MemoryMatchInput {
        public Coords coords;
        public int inputId;
        public int matchValue;
    }
    public class MemoryMatch : PuzzlerMiniGame {
        private string miniGameId;
        private Coords[] inputCoords;
        private int[] inputIds;
        private int[] matchValues;
        private MemoryMatchInput[,] inputGrid;
        private int gridLength;
        private int gridHeight;
        private MemoryMatchInput lastViewedInput;
        private List<MemoryMatchInput> correctMatches;
        private float timeLimit;

        public MemoryMatch(string miniGameId = "memoryMatch", int gridLength = 4, int gridHeight = 4, float timeLimit = 60f) {
            this.miniGameId = miniGameId;
            this.gridLength = gridLength;
            this.gridHeight = gridHeight;
            this.timeLimit = timeLimit;
            GenerateInputIds();
            GenerateMatchValues();
            CreateGrid();
        }

        private void GenerateMatchValues() {
            int matchIdCount = gridLength * gridHeight / 2;
            matchValues = new int[matchIdCount];
            for(int i = 0; i < matchIdCount; i++) {
                matchValues[i] = i;
            }
        }

        private void GenerateInputIds() {
            int inputIdCount = gridLength * gridHeight;
            inputIds = new int[inputIdCount];
            for (int i = 0; i < inputIdCount; i++) {
                inputIds[i] = i;
            }
        }

        private void CreateGrid() {
            inputGrid = new MemoryMatchInput[gridLength, gridHeight];
            ShuffleInputSequence();
        }

        private void ShuffleInputSequence() {
            List<int> matchValuesToAdd = new List<int>();
            int inputId = 0;
            for(int i = 0; i < matchValues.Length; i++) {
                matchValuesToAdd.Add(matchValues[i]);
                matchValuesToAdd.Add(matchValues[i]);
            }
            for(int i = 0; i < gridLength; i++) {
                for(int j = 0; j < gridHeight; j++) {
                    if(matchValuesToAdd.Count > 0) {
                        int matchValueIndex = Random.Range(0, matchValuesToAdd.Count);
                        inputGrid[i, j] = new MemoryMatchInput {
                            inputId = inputId,
                            coords = new Coords { x = i, y = j },
                            matchValue = matchValuesToAdd[matchValueIndex]
                        };
                        matchValuesToAdd.RemoveAt(matchValueIndex);
                        inputId++;
                    }                    
                }
            }
            correctMatches = new List<MemoryMatchInput>();
        }

        public MemoryMatchInput[,] GetInputGrid() {
            return inputGrid;
        }

        public List<MemoryMatchInput> GetCorrectMatches() {
            return correctMatches;
        }

        public MemoryMatchInput GetLastViewedInput() {
            return lastViewedInput;
        }

        public void Restart() {
            ShuffleInputSequence();
        }

        public void InputReceived(MemoryMatchInput currentInput) {
            if(lastViewedInput == null) {
                lastViewedInput = currentInput;
                PuzzlerMiniGameEventManager.instance.OnPuzzlerInputReceived(miniGameId, InputResults.NEUTRAL, currentInput.inputId);
            } else if(lastViewedInput.inputId == currentInput.inputId) {
                lastViewedInput = null;
                PuzzlerMiniGameEventManager.instance.OnPuzzlerInputReceived(miniGameId, InputResults.NEUTRAL, currentInput.inputId);
            } else if(lastViewedInput.matchValue == currentInput.matchValue) {
                correctMatches.Add(lastViewedInput);
                correctMatches.Add(currentInput);
                PuzzlerMiniGameEventManager.instance.OnPuzzlerInputReceived(miniGameId, InputResults.PASS, currentInput.inputId);
                if(correctMatches.Count == inputIds.Length) {
                    PuzzlerMiniGameEventManager.instance.OnPuzzlerMiniGameSolved(miniGameId);
                }
            } else if(lastViewedInput.matchValue != currentInput.matchValue) {
                lastViewedInput = null;
                PuzzlerMiniGameEventManager.instance.OnPuzzlerInputReceived(miniGameId, InputResults.FAIL, currentInput.inputId);
            }
        }

        public void ValidateFailureThreshold(float timePassed) {
            if(timePassed >= timeLimit) {
                PuzzlerMiniGameEventManager.instance.OnPuzzlerMiniGameFailed(miniGameId);
            }
        }
    }
}

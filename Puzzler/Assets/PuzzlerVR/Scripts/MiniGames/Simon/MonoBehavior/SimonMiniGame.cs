using UnityEngine;
using System.Collections;
using Assets.PuzzlerVR.Scripts.MiniGames;
using System.Collections.Generic;

public class SimonMiniGame : MonoBehaviour {
    public Simon simon;
    public string miniGameId = "simon";
    public int inputCount = 5;
    public int inputSequenceCount = 5;
    public int failureThreshold = 3;

    private void Awake() {
        simon = new Simon(miniGameId, inputCount, inputSequenceCount, failureThreshold);

        Debug.Log("Simon input sequence: " + string.Join("", new List<int>(simon.GetInputSequence()).ConvertAll(i => i.ToString()).ToArray()));
    }
}

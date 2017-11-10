using UnityEngine;
using System.Collections;
using Assets.PuzzlerVR.Scripts.MiniGames;

public class Player : MonoBehaviour {

    private void Start() {
        if(PuzzlerMiniGameEventManager.instance != null) {
            PuzzlerMiniGameEventManager.instance.PuzzlerInputReceived += OnMiniGameInputReceived;
            PuzzlerMiniGameEventManager.instance.PuzzlerMiniGameSolved += OnMiniGameSolved;
            PuzzlerMiniGameEventManager.instance.PuzzlerMiniGameFailed += OnMiniGameFailed;
        }
    }

    private void OnMiniGameInputReceived(object source, PuzzlerMiniGameInputEventArgs args) {
        Debug.Log("MiniGameId: " + args.MiniGameId);
        Debug.Log("InputID: " + args.InputID);
        Debug.Log("InputResult: " + args.InputResult);
    }

    private void OnMiniGameSolved(object source, PuzzlerMiniGameEventArgs args) {
        Debug.Log("MiniGameId: " + args.MiniGameId);
    }

    private void OnMiniGameFailed(object source, PuzzlerMiniGameEventArgs args) {
        Debug.Log("MiniGameId: " + args.MiniGameId);
    }
}

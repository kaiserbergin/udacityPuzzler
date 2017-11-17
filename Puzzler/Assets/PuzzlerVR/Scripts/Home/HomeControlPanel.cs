using UnityEngine;
using System.Collections;
using Assets.PuzzlerVR.Scripts.MiniGames;

public class HomeControlPanel : MonoBehaviour {
    public void OnMiniGameSelected(string miniGameId) {
        if (PuzzlerMiniGameEventManager.instance != null) {
            PuzzlerMiniGameEventManager.instance.OnPuzzlerMiniGameRequested(miniGameId);
        }
    }
}

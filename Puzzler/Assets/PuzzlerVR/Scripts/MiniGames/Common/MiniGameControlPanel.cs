using UnityEngine;
using System.Collections;
using Assets.PuzzlerVR.Scripts.MiniGames;

public class MiniGameControlPanel : MonoBehaviour {
    public string miniGameId;
    
    public void OnPlayClick() {
        if(PuzzlerMiniGameEventManager.instance != null) {
            PuzzlerMiniGameEventManager.instance.OnRequestMiniGamePlay(miniGameId);
        }
    }
    public void OnExitClick() {
        if (PuzzlerMiniGameEventManager.instance != null) {
            PuzzlerMiniGameEventManager.instance.OnRequestMiniGameExit(miniGameId);
        }
    }
}

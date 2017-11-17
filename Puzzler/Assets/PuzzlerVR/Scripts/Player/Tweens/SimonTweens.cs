using UnityEngine;
using System.Collections;
using DG.Tweening;
using Assets.PuzzlerVR.Scripts.MiniGames;

public class SimonTweens : MonoBehaviour {

    public GameObject player;
    public GameObject homePortal;
    public GameObject simonPortal;
    public GameObject simonControlPanel;
    public GameObject homeControlPanel;
    public string miniGameId = "simon";

    private Vector3 homePosition;
    private Vector3 simonPlayPosition;
    private Vector3 homePortalEntrance;
    private Vector3 homePortalExit;
    private Vector3 simonPortalEntrance;
    private Vector3 simonPortalExit;
    private Camera mainCamera;

    // Use this for initialization
    void Start() {
        homePosition = player.transform.position;
        mainCamera = Camera.main;
        simonPlayPosition = new Vector3(
            simonControlPanel.transform.position.x,
            1.75f,
            simonControlPanel.transform.position.z - 3
        );
        homePortalEntrance = new Vector3(
            homePortal.transform.position.x,
            homePosition.y,
            homePortal.transform.position.z - 2
        );
        homePortalExit = new Vector3(
            homePortal.transform.position.x,
            homePosition.y,
            homePortal.transform.position.z + 1
        );
        simonPortalEntrance = new Vector3(
            simonPortal.transform.position.x,
            simonPortal.transform.position.y,
            simonPortal.transform.position.z + 1
        );
        simonPortalExit = new Vector3(
            simonPortal.transform.position.x,
            simonPortal.transform.position.y,
            simonPortal.transform.position.z - 1
        );
        if (PuzzlerMiniGameEventManager.instance != null) {
            PuzzlerMiniGameEventManager.instance.PuzzlerMiniGameRequested += TweenToSimon;
            PuzzlerMiniGameEventManager.instance.MiniGameRequestExit += TweenToHome;
        }
        Debug.Log("entrancePortal x:" + player.transform.position.x);
        Debug.Log("entrancePortal y:" + player.transform.position.y);
        Debug.Log("entrancePortal z:" + player.transform.position.z);
    }

    // Update is called once per frame
    void TweenToSimon(object source, PuzzlerMiniGameEventArgs args) {
        if (args.MiniGameId == miniGameId && homePortal != null && simonPortal != null && simonControlPanel != null) {
            Sequence goToSimonSequence = DOTween.Sequence();
            goToSimonSequence.Append(transform.DOMove(homePortalEntrance, 8f))
                .Append(transform.DOMove(simonPortalExit, .75f))
                .Append(transform.DOMove(simonPlayPosition, 5));
        }
    }

    void TweenToHome(object source, PuzzlerMiniGameEventArgs args) {
        if (args.MiniGameId == miniGameId && homePortal != null && simonPortal != null && simonControlPanel != null) {
            Sequence goToHome = DOTween.Sequence();
            goToHome
                .Append(transform.DOMove(simonPortalEntrance, 5))
                .Append(transform.DOMove(homePortalExit, .75f))
                .Append(transform.DOMove(homePosition, 6f));
        }
    }
}

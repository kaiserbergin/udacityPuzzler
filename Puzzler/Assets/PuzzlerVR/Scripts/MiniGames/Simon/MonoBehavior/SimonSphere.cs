using UnityEngine;
using System.Collections;
using Assets.PuzzlerVR.Scripts.MiniGames;

public class SimonSphere : MonoBehaviour {

    public GameObject ChildSphere;
    public int inputId;
    public SimonMiniGame simonMiniGame;
    public float InactiveIntensity = 0.1f;
    public float HoverIntensity = 0.6f;
    public float SelectedIntensity = 1.25f;
    public bool inputRestricted = true;
    public string queuedState = "inactive";

    private Renderer _renderer;
    private MaterialPropertyBlock _materialPropertyBlock;

    private void Awake() {
        _materialPropertyBlock = new MaterialPropertyBlock();
        _renderer = ChildSphere.GetComponent<Renderer>();
        ApplySphereIntensity("inactive");
    }

    private void Start() {
        if (PuzzlerMiniGameEventManager.instance != null) {
            PuzzlerMiniGameEventManager.instance.PuzzlerInputReceived += OnMiniGameInputReceived;
        }
    }

    private void OnMiniGameInputReceived(object source, PuzzlerMiniGameInputEventArgs args) {
        if (args.InputResult == InputResults.PASS && args.MiniGameId == simonMiniGame.miniGameId) {
            transform.GetComponent<AudioSource>().Play();
        }
    }

    public void OnInputSelected() {
        if (!inputRestricted) {
            simonMiniGame.simon.VerifyInput(inputId);
            ApplySphereIntensity("selected");
        }    
    }

    public void OnInputHover() {
        if (!inputRestricted) {
            ApplySphereIntensity("hover");
        } else {
            queuedState = "hover";
        }
    }

    public void OnInputExitHover() {
        if (!inputRestricted) {
            ApplySphereIntensity("inactive");
        } else {
            queuedState = "inactive";
        }
    }

    public void ApplySphereIntensity(string intensity) {
        float intensityFloat = 0.1f;
        switch(intensity) {
            case "inactive":
                intensityFloat = InactiveIntensity;
                break;
            case "hover":
                intensityFloat = HoverIntensity;
                break;
            case "selected":
                intensityFloat = SelectedIntensity;
                break;
        }
        _renderer.GetPropertyBlock(_materialPropertyBlock);
        _materialPropertyBlock.SetFloat("_Intensity", intensityFloat);
        _renderer.SetPropertyBlock(_materialPropertyBlock);
    }
}

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

    private Renderer _renderer;
    private MaterialPropertyBlock _materialPropertyBlock;

    private void Awake() {
        _materialPropertyBlock = new MaterialPropertyBlock();
        _renderer = ChildSphere.GetComponent<Renderer>();
        ApplySphereIntensity("inactive");
    }

    public void OnInputSelected() {
        simonMiniGame.simon.VerifyInput(inputId);
        ApplySphereIntensity("selected");
    }

    public void OnInputHover() {
        ApplySphereIntensity("hover");
    }

    public void OnInputExitHover() {
        ApplySphereIntensity("inactive");
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

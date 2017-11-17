using UnityEngine;
using System.Collections;
using Assets.PuzzlerVR.Scripts.MiniGames;
using System.Collections.Generic;

public interface IMonoMiniGame {
    string MiniGameId { get; }
}

public class SimonMiniGame : MonoBehaviour, IMonoMiniGame {
    public Simon simon;
    public string miniGameId = "simon";
    public string MiniGameId {
        get { return miniGameId; }
    }
    public int inputCount = 5;
    public int inputSequenceCount = 5;
    public int failureThreshold = 3;
    public bool inputRestricted;
    public bool sequencePlaying;
    public float modifiedAudioLength = 1.25f;
    public float pauseTimeBetweenSequencePlays = .5f;
    public float startUpBuffer = 1.5f;
    private int[] inputSequence;

    private bool isMiniGameSolved = false;

    public AudioClip failSound;
    public AudioClip startGameSound;
    public AudioClip gameWonClip;

    public SimonSphere[] simonSpheres;

    private void Awake() {
        inputRestricted = true;
        sequencePlaying = false;
    }

    private void Start() {
        simon = new Simon(PuzzlerMiniGameEventManager.instance, miniGameId, inputCount, inputSequenceCount, failureThreshold);
        inputSequence = simon.GetInputSequence();
        if (PuzzlerMiniGameEventManager.instance != null) {
            PuzzlerMiniGameEventManager.instance.PuzzlerInputReceived += OnMiniGameInputReceived;
            PuzzlerMiniGameEventManager.instance.MiniGameRequestPlay += PlaySequence;
            PuzzlerMiniGameEventManager.instance.PuzzlerMiniGameSolved += OnMiniGameSolved;
            //PuzzlerMiniGameEventManager.instance.PuzzlerMiniGameFailed += OnMiniGameFailed;
        }
    }

    public void OnMiniGameSolved(object source, PuzzlerMiniGameEventArgs args) {
        if (args.MiniGameId == miniGameId) {
            simon = new Simon(PuzzlerMiniGameEventManager.instance, miniGameId, inputCount, inputSequenceCount, failureThreshold);
            inputSequence = simon.GetInputSequence();
            if (gameWonClip) {
                AudioSource audioSource = transform.GetComponent<AudioSource>();
                if (audioSource) {
                    audioSource.clip = gameWonClip;
                    audioSource.Play();
                }
            }
        }
    }

    public void PlaySequence(object source, PuzzlerMiniGameEventArgs args) {
        if (args.MiniGameId == miniGameId && !sequencePlaying) {
            StartCoroutine(PlaySequence(pauseTimeBetweenSequencePlays));
        }
    }

    IEnumerator PlaySequence(float pauseTimeBetweenSequencePlays) {
        inputRestricted = true;
        sequencePlaying = true;
        SetSphereInputRestricted(true);
        simon.ClearInputs();
        int sequenceIndex = 0;
        if (startGameSound) {
            AudioSource parentAudioSource = transform.GetComponent<AudioSource>();
            if (parentAudioSource) {
                parentAudioSource.clip = startGameSound;
                parentAudioSource.Play();
            }
        }
        yield return new WaitForSeconds(startUpBuffer);
        while (sequenceIndex < inputSequence.Length) {
            int currentInputId = inputSequence[sequenceIndex];
            simonSpheres[currentInputId].ApplySphereIntensity("selected");

            AudioSource audioSource = transform.GetChild(currentInputId).GetComponent<AudioSource>();
            if (audioSource) audioSource.Play();

            yield return new WaitForSeconds(modifiedAudioLength);

            simonSpheres[currentInputId].ApplySphereIntensity("inactive");
            sequenceIndex++;
            yield return new WaitForSeconds(pauseTimeBetweenSequencePlays);
        }
        SetSphereInputRestricted(false);
        inputRestricted = false;
        sequencePlaying = false;
    }

    private void SetSphereInputRestricted(bool inputRestricted) {
        foreach (SimonSphere sphere in simonSpheres) {
            sphere.inputRestricted = inputRestricted;
        }
    }

    private void OnMiniGameInputReceived(object source, PuzzlerMiniGameInputEventArgs args) {
        if (args.MiniGameId == miniGameId && args.InputResult == InputResults.FAIL) {
            AudioSource audioSource = transform.GetComponent<AudioSource>();
            if (audioSource != null && failSound != null) {
                audioSource.clip = failSound;
                audioSource.Play();
            }
        }
    }
}

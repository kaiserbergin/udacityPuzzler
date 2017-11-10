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
    public bool inputRestricted;
    public bool sequencePlaying;
    public float modifiedAudioLength = 1.25f;
    public float pauseTimeBetweenSequencePlays = .5f;
    private int[] inputSequence;

    public AudioClip failSound;

    public SimonSphere[] simonSpheres;

    private void Awake() {
        inputRestricted = true;
        sequencePlaying = false;
        simon = new Simon(miniGameId, inputCount, inputSequenceCount, failureThreshold);
        inputSequence = simon.GetInputSequence();

        Debug.Log("Simon input sequence: " + string.Join("", new List<int>(simon.GetInputSequence()).ConvertAll(i => i.ToString()).ToArray()));
    }

    private void Start() {
        if (PuzzlerMiniGameEventManager.instance != null) {
            PuzzlerMiniGameEventManager.instance.PuzzlerInputReceived += OnMiniGameInputReceived;
            //PuzzlerMiniGameEventManager.instance.PuzzlerMiniGameSolved += OnMiniGameSolved;
            //PuzzlerMiniGameEventManager.instance.PuzzlerMiniGameFailed += OnMiniGameFailed;
        }
        PlaySequence();
    }

    public void PlaySequence() {
        StartCoroutine(PlaySequence(pauseTimeBetweenSequencePlays));
    }

    IEnumerator PlaySequence(float pauseTimeBetweenSequencePlays) {
        inputRestricted = true;
        sequencePlaying = true;
        SetSphereInputRestricted(true);
        int sequenceIndex = 0;
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
       if(args.MiniGameId == miniGameId && args.InputResult == InputResults.FAIL) {
            AudioSource audioSource = transform.GetComponent<AudioSource>();
            if(audioSource != null && failSound != null) {
                audioSource.clip = failSound;
                audioSource.Play();
            }
        }
    }
}

using UnityEngine;
using UnityEditor;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;
using Assets.PuzzlerVR.Scripts.MiniGames;

public class SimonTests {

    [TestCase("someId", 5, 5, 3)]
    [TestCase("", 5, 10, 8)]
    [TestCase("simon", 15, 10, 2)]
    public void ConstructSimonTest(string miniGameId, int inputCount, int inputSequenceCount, int failureThreshold) {
        Simon simon = new Simon(miniGameId, inputCount, inputSequenceCount, failureThreshold);
        Assert.AreEqual(simon.GetMiniGameId(), miniGameId);
        Assert.AreEqual(simon.GetInputIds().Length, inputCount);
        Assert.AreEqual(simon.GetInputSequence().Length, inputSequenceCount);
        Assert.AreEqual(simon.GetFailureThreshold(), failureThreshold);
    }
}
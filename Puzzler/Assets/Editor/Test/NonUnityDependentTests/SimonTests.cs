using UnityEngine;
using UnityEditor;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;
using Assets.PuzzlerVR.Scripts.MiniGames;
using NSubstitute;

public class SimonTests {

    static object[] SimonConstructorArgCases = new object[] {
        new object[] { Substitute.For<IMiniGameEventManager>(), "someId", 5, 5, 3 },
        new object[] { Substitute.For<IMiniGameEventManager>(), "", 5, 10, 8 },
        new object[] { Substitute.For<IMiniGameEventManager>(), "simon", 15, 10, 2 }
    };

    [Test, TestCaseSource("SimonConstructorArgCases")]
    public void ConstructSimonTest(IMiniGameEventManager miniGameEventManager, string miniGameId, int inputCount, int inputSequenceCount, int failureThreshold) {
        Simon simon = new Simon(miniGameEventManager, miniGameId, inputCount, inputSequenceCount, failureThreshold);
        Assert.AreEqual(simon.GetMiniGameId(), miniGameId);
        Assert.AreEqual(simon.GetInputIds().Length, inputCount);
        Assert.AreEqual(simon.GetInputSequence().Length, inputSequenceCount);
        Assert.AreEqual(simon.GetFailureThreshold(), failureThreshold);
    }

    [Test, TestCaseSource("SimonConstructorArgCases")]
    public void SimonSolved(IMiniGameEventManager miniGameEventManager, string miniGameId, int inputCount, int inputSequenceCount, int failureThreshold) {
        bool miniGameSolved = false;
        int wrongAnswerCount = 0;
        int correctAnswerCount = 0;
        bool miniGameFailed = false;
        miniGameEventManager.OnMiniGameInputReceived(
            Arg.Any<string>(),
            Arg.Do<InputResults>(x => {
                if (x.Equals(InputResults.FAIL)) wrongAnswerCount++;
                if (x.Equals(InputResults.PASS)) correctAnswerCount++;
            }),
            Arg.Any<int>()
        );
        miniGameEventManager.When(x => x.OnMiniGameFailed(miniGameId)).Do(x => miniGameFailed = true);
        miniGameEventManager.When(x => x.OnMiniGameSolved(miniGameId)).Do(x => miniGameSolved = true);

        Simon simon = new Simon(miniGameEventManager, miniGameId, inputCount, inputSequenceCount, failureThreshold);
        int[] inputSequence = simon.GetInputSequence();
        foreach(int input in inputSequence) {
            simon.VerifyInput(input);
        }

        Assert.IsTrue(miniGameSolved);
        Assert.IsTrue(!miniGameFailed);
        Assert.AreEqual(simon.GetInputSequence().Length, correctAnswerCount);
    }

    [Test, TestCaseSource("SimonConstructorArgCases")]
    public void SimonSolvedWithWrongAnswersTest(IMiniGameEventManager miniGameEventManager, string miniGameId, int inputCount, int inputSequenceCount, int failureThreshold) {
        bool miniGameSolved = false;
        int wrongAnswerCount = 0;
        int correctAnswerCount = 0;
        bool miniGameFailed = false;
        miniGameEventManager.OnMiniGameInputReceived(
            Arg.Any<string>(),
            Arg.Do<InputResults>(x => {
                if (x.Equals(InputResults.FAIL)) wrongAnswerCount++;
                if (x.Equals(InputResults.PASS)) correctAnswerCount++;
            }),
            Arg.Any<int>()
        );
        miniGameEventManager.When(x => x.OnMiniGameFailed(miniGameId)).Do(x => miniGameFailed = true);
        miniGameEventManager.When(x => x.OnMiniGameSolved(miniGameId)).Do(x => miniGameSolved = true);

        Simon simon = new Simon(miniGameEventManager, miniGameId, inputCount, inputSequenceCount, failureThreshold);
        int[] inputSequence = simon.GetInputSequence();

        simon.VerifyInput(simon.GetInputIds().Length + 1);

        foreach (int input in inputSequence) {
            simon.VerifyInput(input);
        }

        Assert.IsTrue(miniGameSolved);
        Assert.IsTrue(!miniGameFailed);
        Assert.AreEqual(simon.GetInputSequence().Length, correctAnswerCount);
        Assert.AreEqual(simon.GetFailureCount(), wrongAnswerCount);
    }

    [Test, TestCaseSource("SimonConstructorArgCases")]
    public void SimonFailedTest(IMiniGameEventManager miniGameEventManager, string miniGameId, int inputCount, int inputSequenceCount, int failureThreshold) {
        bool miniGameSolved = false;
        int wrongAnswerCount = 0;
        int correctAnswerCount = 0;
        bool miniGameFailed = false;
        miniGameEventManager.OnMiniGameInputReceived(
            Arg.Any<string>(),
            Arg.Do<InputResults>(x => {
                if (x.Equals(InputResults.FAIL)) wrongAnswerCount++;
                if (x.Equals(InputResults.PASS)) correctAnswerCount++;
            }),
            Arg.Any<int>()
        );
        miniGameEventManager.When(x => x.OnMiniGameFailed(miniGameId)).Do(x => miniGameFailed = true);
        miniGameEventManager.When(x => x.OnMiniGameSolved(miniGameId)).Do(x => miniGameSolved = true);

        Simon simon = new Simon(miniGameEventManager, miniGameId, inputCount, inputSequenceCount, failureThreshold);
        int[] inputSequence = simon.GetInputSequence();

        for(int i = 0; i < failureThreshold; i++) {
            simon.VerifyInput(simon.GetInputIds().Length + 1);
        }        

        Assert.IsTrue(!miniGameSolved);
        Assert.IsTrue(miniGameFailed);
        Assert.AreEqual(0, correctAnswerCount);
        Assert.AreEqual(simon.GetFailureCount(), wrongAnswerCount);
    }
}
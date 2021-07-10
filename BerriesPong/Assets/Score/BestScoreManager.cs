using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BestScoreManager : MonoBehaviour {
    [SerializeField] private BestScoreData topBestScoreRef;
    [SerializeField] private IntVariable topCurrentScoreRef; 
    [SerializeField] private BestScoreData bottomBestScoreRef;
    [SerializeField] private IntVariable bottomCurrentScoreRef;

    private void OnEnable() {
        Player.GoalEvent += OnPlayerGoaled;
    }
    
    private void OnDisable() {
        Player.GoalEvent -= OnPlayerGoaled;
    }

    private void OnPlayerGoaled() {
        CheckBestScore();
    }

    private void CheckBestScore() {
        var betterCurrentScore = bottomCurrentScoreRef.value;
        if (topCurrentScoreRef.value > betterCurrentScore) {
            betterCurrentScore = topCurrentScoreRef.value;
        }

        var betterBestScore = bottomBestScoreRef.BestScore;
        if (topBestScoreRef.BestScore > betterBestScore) {
            betterBestScore = topBestScoreRef.BestScore;
        }

        if (betterCurrentScore > betterBestScore) {
            topBestScoreRef.SetNewBestScore(topCurrentScoreRef.value);
            bottomBestScoreRef.SetNewBestScore(bottomCurrentScoreRef.value);
        }
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Best Score Data", menuName = "L/Best Score Data")]
public class BestScoreData : ScriptableObject {
    private string key => $"best-score-{name}";

    public int BestScore {
        get => PlayerPrefs.GetInt(key, 0);
        private set => PlayerPrefs.SetInt(key, value);
    }

    public void SetNewBestScore(int score) {
        BestScore = score;
        Debug.Log($"New Best Score ({BestScore}) for {name}");
    }
}
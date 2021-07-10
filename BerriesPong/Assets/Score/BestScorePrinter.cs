using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BestScorePrinter : MonoBehaviour {
    [SerializeField] private BestScoreData scoreRef;
    
    private TMP_Text scoreTxt;

    private void Awake() {
        scoreTxt = GetComponent<TMP_Text>();
        scoreTxt.text = "0";
    }

    private void Update() {
        scoreTxt.text = scoreRef.BestScore.ToString();
    }
}
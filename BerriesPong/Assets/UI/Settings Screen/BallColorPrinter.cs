using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BallColorPrinter : MonoBehaviour {
    [SerializeField] private MaterialVariable paletteMaterialRef;

    private Image ballImg;

    private void Awake() {
        ballImg = GetComponent<Image>();
    }

    private void Update() {
        ballImg.material = paletteMaterialRef.value;
    }
}
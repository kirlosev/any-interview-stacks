using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallColorPopulator : MonoBehaviour {
    [SerializeField] private SelectBallColorButton selectButtonInstance;
    
    private Material[] ballColorMaterials;

    private void Awake() {
        ballColorMaterials = Resources.LoadAll<Material>("Ball Palettes/Materials/");
    }

    private void Start() {
        foreach (Transform t in transform) {
            Destroy(t.gameObject);
        }
        
        foreach (var m in ballColorMaterials) {
            var bb = Instantiate(selectButtonInstance, transform);
            bb.Init(m);
        }
    }
}
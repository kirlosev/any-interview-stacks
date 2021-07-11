using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayersSeparator : MonoBehaviour {
    [SerializeField] private FloatVariable widthOffsetRef;
    
    private SpriteRenderer rend;

    private void Awake() {
        rend = GetComponent<SpriteRenderer>();
    }

    private void Start() {
        rend.size = new Vector2(Cam.Instance.Width * 2f + -widthOffsetRef.value * 2f, rend.size.y);
    }
}
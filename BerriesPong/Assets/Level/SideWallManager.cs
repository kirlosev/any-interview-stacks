using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SideWallManager : MonoBehaviour {
    [SerializeField] private FloatVariable heightOffsetRef;
    [SerializeField] private FloatVariable widthOffsetRef;
    
    private BoxCollider2D col;
    private SpriteRenderer rend;
    
    private void Awake() {
        col = GetComponent<BoxCollider2D>();
        rend = GetComponentInChildren<SpriteRenderer>();
    }

    private void Start() {
        rend.size = new Vector2(rend.size.x, Cam.Instance.Height * 2f + -heightOffsetRef.value * 2f);
        col.size = new Vector2(1f, Cam.Instance.Height * 2f + -heightOffsetRef.value * 2f);
        transform.position = Vector3.right * (Mathf.Sign(transform.position.x) * (Cam.Instance.RightEdge + 0.5f - widthOffsetRef.value));
    }
}
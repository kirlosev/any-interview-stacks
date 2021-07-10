using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SideWallManager : MonoBehaviour {
    private BoxCollider2D col;
    
    private void Awake() {
        col = GetComponent<BoxCollider2D>();
    }

    private void Start() {
        col.size = new Vector2(1f, Cam.Instance.Height * 2f + 6f);
        transform.position = Vector3.right * (Mathf.Sign(transform.position.x) * (Cam.Instance.RightEdge + 0.5f));
    }
}
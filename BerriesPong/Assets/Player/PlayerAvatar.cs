using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerSide {
    Top,
    Bottom
}

public class PlayerAvatar : MonoBehaviour {
    [SerializeField] private float moveSpeed = 1f;
    [SerializeField] private PlayerSide side = PlayerSide.Bottom;
    [SerializeField] private FloatVariable widthOffsetRef;

    private float horzTarget;
    private Vector3 velocity;
    private BoxCollider2D racketCollider;
    private float moveDelta;

    private void Awake() {
        racketCollider = GetComponent<BoxCollider2D>();
    }

    private void OnEnable() {
        if (side.Equals(PlayerSide.Bottom)) {
            InputManager.BottomPlayerMoveToPositionEvent += OnMoveToPositionInput;
            InputManager.BottomPlayerMoveDeltaEvent += OnMoveDeltaInput;
        } else if (side.Equals(PlayerSide.Top)) {
            InputManager.TopPlayerMoveToPositionEvent += OnMoveToPositionInput;
            InputManager.TopPlayerMoveDeltaEvent += OnMoveDeltaInput;
        }
    }
    
    private void OnDisable() {
        if (side.Equals(PlayerSide.Bottom)) {
            InputManager.BottomPlayerMoveToPositionEvent -= OnMoveToPositionInput;
            InputManager.BottomPlayerMoveDeltaEvent -= OnMoveDeltaInput;
        }
        else if (side.Equals(PlayerSide.Top)) {
            InputManager.TopPlayerMoveToPositionEvent -= OnMoveToPositionInput;
            InputManager.TopPlayerMoveDeltaEvent -= OnMoveDeltaInput;
        }
    }

    private void OnMoveToPositionInput(float targetPosX) {
        horzTarget = targetPosX;
        moveDelta = horzTarget - transform.position.x;
    }

    private void OnMoveDeltaInput(float delta) {
        moveDelta = delta;
    }

    private void Update() {
        velocity.x = moveDelta * moveSpeed;
        transform.position += velocity * Time.deltaTime;

        if (Mathf.Abs(transform.position.x) > HorzMaxPos) {
            transform.position = new Vector3(Mathf.Sign(transform.position.x) * HorzMaxPos, transform.position.y);
        }
    }

    private float HorzMaxPos => Cam.Instance.RightEdge - widthOffsetRef.value - racketCollider.size.x/2f;
}
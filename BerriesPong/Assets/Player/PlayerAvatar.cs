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

    private float horzTarget;
    private Vector3 velocity;

    private void OnEnable() {
        if (side.Equals(PlayerSide.Bottom)) {
            InputManager.BottomPlayerStartMoveEvent += OnStartMoveInput;
            InputManager.BottomPlayerDeltaMoveEvent += OnMoveInput;
            InputManager.BottomPlayerEndMoveEvent += OnEndMoveInput;
        } else if (side.Equals(PlayerSide.Top)) {
            InputManager.TopPlayerStartMoveEvent += OnStartMoveInput;
            InputManager.TopPlayerDeltaMoveEvent += OnMoveInput;
            InputManager.TopPlayerEndMoveEvent += OnEndMoveInput;
        }
    }
    
    private void OnDisable() {
        if (side.Equals(PlayerSide.Bottom)) {
            InputManager.BottomPlayerStartMoveEvent -= OnStartMoveInput;
            InputManager.BottomPlayerDeltaMoveEvent -= OnMoveInput;
            InputManager.BottomPlayerEndMoveEvent -= OnEndMoveInput;
        }
        else if (side.Equals(PlayerSide.Top)) {
            InputManager.TopPlayerStartMoveEvent -= OnStartMoveInput;
            InputManager.TopPlayerDeltaMoveEvent -= OnMoveInput;
            InputManager.TopPlayerEndMoveEvent -= OnEndMoveInput;
        }
        else {
            Debug.LogError($"The side for {gameObject.name} is not selected correctly");
        }
    }

    private void OnStartMoveInput() {
    }

    private void OnMoveInput(float targetPosX) {
        horzTarget = targetPosX;
    }

    private void OnEndMoveInput() {
        velocity = Vector3.zero;
    }

    private void Update() {
        velocity.x = (horzTarget - transform.position.x) * moveSpeed;
        transform.position += velocity * Time.deltaTime;
    }
}
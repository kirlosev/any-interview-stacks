using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour {
    public static event System.Action TopPlayerStartMoveEvent;
    public static event System.Action<float> TopPlayerDeltaMoveEvent;
    public static event System.Action TopPlayerEndMoveEvent;
    
    public static event System.Action BottomPlayerStartMoveEvent;
    public static event System.Action<float> BottomPlayerDeltaMoveEvent;
    public static event System.Action BottomPlayerEndMoveEvent;
    
    private PlayerInputActions playerInputActions;

    private bool isTopDown;
    private float topDownPosX;
    private bool isBottomDown;
    private float bottomDownPosX;
    private bool isActive;

    private void Awake() {
        playerInputActions = new PlayerInputActions();
    }

    private void OnEnable() {
        playerInputActions.Enable();
        MainMenuScreen.PlayButtonClickEvent += OnPlayButtonClick;
    }

    private void OnDisable() {
        playerInputActions.Disable();
        MainMenuScreen.PlayButtonClickEvent -= OnPlayButtonClick;
    }

    private void OnPlayButtonClick() {
        isActive = true;
    }

    private void Start() {
        playerInputActions.PlayerControlsTop.TouchPress.started += ctx => TopStartTouch(ctx);
        playerInputActions.PlayerControlsTop.TouchPress.canceled += ctx => TopEndTouch(ctx);
        playerInputActions.PlayerControlsTop.TouchDelta.performed += ctx => TopMoveTouch(ctx);
        
        playerInputActions.PlayerControlsBottom.TouchPress.started += ctx => BottomStartTouch(ctx);
        playerInputActions.PlayerControlsBottom.TouchPress.canceled += ctx => BottomEndTouch(ctx);
        playerInputActions.PlayerControlsBottom.TouchDelta.performed += ctx => BottomMoveTouch(ctx);
    }

    private void TopStartTouch(InputAction.CallbackContext context) {
        if (IsOverUI || isTopDown || !isActive) return;

        var worldPos = Cam.Instance.GetWorldPoint(playerInputActions.PlayerControlsTop.TouchPosition.ReadValue<Vector2>());
        if (worldPos.y > 0f) {
            isTopDown = true;
            topDownPosX = worldPos.x;
            TopPlayerStartMoveEvent?.Invoke();
        }
    }
    
    private void TopEndTouch(InputAction.CallbackContext context) {
        if (!isTopDown || !isActive) return;
        
        var worldPos = Cam.Instance.GetWorldPoint(playerInputActions.PlayerControlsTop.TouchPosition.ReadValue<Vector2>());
        // if (worldPos.y > 0f) {
            isTopDown = false;
            TopPlayerEndMoveEvent?.Invoke();
        // }
    }
    
    private void TopMoveTouch(InputAction.CallbackContext context) {
        if (!isActive) return;
        
        if (isTopDown) {
            var worldPos = Cam.Instance.GetWorldPoint(playerInputActions.PlayerControlsTop.TouchPosition.ReadValue<Vector2>());
            TopPlayerDeltaMoveEvent?.Invoke(worldPos.x);
        }
    }
    
    private void BottomStartTouch(InputAction.CallbackContext context) {
        if (IsOverUI || isBottomDown || !isActive) return;
        
        var worldPos = Cam.Instance.GetWorldPoint(playerInputActions.PlayerControlsBottom.TouchPosition.ReadValue<Vector2>());
        if (worldPos.y < 0f) {
            isBottomDown = true;
            bottomDownPosX = worldPos.x;
            BottomPlayerStartMoveEvent?.Invoke();
        }
    }
    
    private void BottomEndTouch(InputAction.CallbackContext context) {
        if (!isBottomDown || !isActive) return;
        
        var worldPos = Cam.Instance.GetWorldPoint(playerInputActions.PlayerControlsBottom.TouchPosition.ReadValue<Vector2>());
        // if (worldPos.y < 0f) {
            isBottomDown = false;
            BottomPlayerEndMoveEvent?.Invoke();
        // }
    }
    
    private void BottomMoveTouch(InputAction.CallbackContext context) {
        if (!isActive) return;
        
        if (isBottomDown) {
            var worldPos = Cam.Instance.GetWorldPoint(playerInputActions.PlayerControlsBottom.TouchPosition.ReadValue<Vector2>());
            BottomPlayerDeltaMoveEvent?.Invoke(worldPos.x);
            
            // Different approach: to move the player by the distance that finger moves. Needs some changes in PlayerAvatar class. 
            // The approach above where the platforms just follows the players just feels better to me personally.
            // var delta = playerInputActions.PlayerControlsBottom.TouchDelta.ReadValue<Vector2>();
            // var deltaWorld = Cam.Instance.GetWorldPoint(delta);
            // var moveResult = deltaWorld.x + Mathf.Abs(Cam.Instance.BottomLeftScreenCornerInWorld.x);
            // Debug.Log($"Delta World: {deltaWorld}, bottom left corner: {Cam.Instance.BottomLeftScreenCornerInWorld}, move result: {moveResult}");
            // BottomPlayerDeltaMoveEvent?.Invoke(moveResult);
        }
    }

    private bool IsOverUI => EventSystem.current.IsPointerOverGameObject(0) || EventSystem.current.IsPointerOverGameObject(1);
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.EnhancedTouch;
using Touch = UnityEngine.InputSystem.EnhancedTouch.Touch;

public class InputManager : MonoBehaviour {
    public static event System.Action<float> TopPlayerMoveToPositionEvent;
    public static event System.Action<float> BottomPlayerMoveToPositionEvent;
    
    public static event System.Action<float> TopPlayerMoveDeltaEvent;
    public static event System.Action<float> BottomPlayerMoveDeltaEvent;
    
    private PlayerInputActions playerInputActions;

    private float topPosX;
    private float bottomPosX;
    private bool isActive;

    private void Awake() {
        playerInputActions = new PlayerInputActions();
        EnhancedTouchSupport.Enable();
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
        playerInputActions.PlayerControlsTop.Move.performed += ctx => TopMove(ctx);
        playerInputActions.PlayerControlsBottom.Move.performed += ctx => BottomMove(ctx);
    }

    private void Update() {
        if (!isActive) return;

        var touches = Touch.activeTouches;
        foreach (var t in touches) {
            var startWorldPos = Cam.Instance.GetWorldPoint(t.startScreenPosition);
            var isBottomTouch = startWorldPos.y < 0f;
            if (isBottomTouch) {
                var worldPos = Cam.Instance.GetWorldPoint(t.screenPosition);
                BottomPlayerMoveToPositionEvent?.Invoke(worldPos.x);
            }
            else {
                var worldPos = Cam.Instance.GetWorldPoint(t.screenPosition);
                TopPlayerMoveToPositionEvent?.Invoke(worldPos.x);
            }
        }
    }
    
    private void TopMove(InputAction.CallbackContext context) {
        if (!isActive) return;
        TopPlayerMoveDeltaEvent?.Invoke(context.ReadValue<float>());
    }
    
    private void BottomMove(InputAction.CallbackContext context) {
        if (!isActive) return;
        BottomPlayerMoveDeltaEvent?.Invoke(context.ReadValue<float>());
    }

    private bool IsOverUI => EventSystem.current.IsPointerOverGameObject(0) || EventSystem.current.IsPointerOverGameObject(1);
}
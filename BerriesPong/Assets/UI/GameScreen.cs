using UnityEngine;
using UnityEngine.SceneManagement;

public class GameScreen : BaseUIScreen {
    protected override void TurnOnOffByDefault() {
        TurnOff();
    }

    public void BTN_RESET() {
        SceneManager.LoadScene("Game");
    }
    
    private void OnEnable() {
        MainMenuScreen.PlayButtonClickEvent += OnPlayClick;
    }

    private void OnDisable() {
        MainMenuScreen.PlayButtonClickEvent -= OnPlayClick;
    }

    private void OnPlayClick() {
        TurnOn();
    }
}
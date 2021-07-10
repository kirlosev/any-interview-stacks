using UnityEngine;

public class MainMenuScreen : BaseUIScreen {
    public static event System.Action PlayButtonClickEvent;
    public static event System.Action SettingsButtonClickEvent;
    
    protected override void TurnOnOffByDefault() {
        TurnOn();
    }

    public void BTN_PLAY() {
        PlayButtonClickEvent?.Invoke();
        TurnOff();
    }

    public void BTN_SETTINGS() {
        SettingsButtonClickEvent?.Invoke();
    }
}

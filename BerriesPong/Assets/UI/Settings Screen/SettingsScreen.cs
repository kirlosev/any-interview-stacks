using System.Security.Cryptography.X509Certificates;
using UnityEngine;

public class SettingsScreen : BaseUIScreen {
    protected override void TurnOnOffByDefault() {
        TurnOff();
    }

    public void BTN_CLOSE() {
        TurnOff();
    }

    private void OnEnable() {
        MainMenuScreen.SettingsButtonClickEvent += OnSettingsClick;
    }

    private void OnDisable() {
        MainMenuScreen.SettingsButtonClickEvent -= OnSettingsClick;
    }

    private void OnSettingsClick() {
        TurnOn();
    }
}
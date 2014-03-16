using UnityEngine;
using System.Collections;

public class SettingPopup : Popup {
  
  public UIAnchor btnCloseAnchor;
  public UIStretch backgroundStretch;
  public UIEventTrigger btnCredits;
  public UIEventTrigger btnClose;
  public UIEventTrigger btnMoreGame;
  public UIEventTrigger btnSound;
  public UIEventTrigger btnBack;
  public UISprite btnSoundBackground;
  public GameObject settingPanel;
  public GameObject creditsPanel;
  
  public override void Init() {
    base.Init();
    EventDelegate.Set (btnClose.onClick, Close);
		EventDelegate.Set (btnCredits.onClick, OpenCreditsPanel);
		EventDelegate.Set (btnMoreGame.onClick, OpenMoreGame);
		EventDelegate.Set (btnSound.onClick, SwitchSound);
		EventDelegate.Set (btnBack.onClick, OpenSettingPanel);
		backgroundStretch.Reset();
    btnCloseAnchor.Init();
    GameManager.paused = true;
    settingPanel.SetActive(true);
    creditsPanel.SetActive(false);
    if (AudioManager.Instance != null) {
      UpdateSoundButton(AudioManager.Instance.IsSoundOn());
    }
  }
  
  public override void HandleClosePopupCallback() {
    PopupManager.Instance.settingPopupScript = null;
    base.HandleClosePopupCallback();
  }
  
  private void OpenCreditsPanel() {
    settingPanel.SetActive(false);
    creditsPanel.SetActive(true);
  }
  
  private void OpenSettingPanel() {
    settingPanel.SetActive(true);
    creditsPanel.SetActive(false);
  }
  
  private void OpenMoreGame() {
  }
  
  private void SwitchSound() {
    bool isSoundOn = AudioManager.Instance.SwitchSound();
    UpdateSoundButton(isSoundOn);
  }
  
  private void UpdateSoundButton(bool isSoundOn) {
    if (isSoundOn) {
      btnSoundBackground.spriteName = "btn_soundON";
    } else {
      btnSoundBackground.spriteName = "btn_soundOFF";
    }
  }
}

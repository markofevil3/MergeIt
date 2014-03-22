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
  
  private string ANDROID_LINK = "https://play.google.com/store/apps/developer?id=CanvasGames";
  private string IOS_LINK = "https://itunes.apple.com/us/artist/bui-p-quan/id718172156";
  
  public override void Init() {
    base.Init();
    EventDelegate.Set (btnClose.onClick, Close);
		EventDelegate.Set (btnCredits.onClick, OpenCreditsPanel);
		EventDelegate.Set (btnMoreGame.onClick, OpenMoreGame);
		EventDelegate.Set (btnSound.onClick, SwitchSound);
		EventDelegate.Set (btnBack.onClick, OpenSettingPanel);
		EventDelegate.Set (btnMoreGame.onClick, OpenMoreGameLink);
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
  
  private void OpenMoreGameLink() {
    #if UNITY_IPHONE
      Application.OpenURL(IOS_LINK);
    #endif
		#if UNITY_ANDROID
      Application.OpenURL(ANDROID_LINK);
		#endif
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

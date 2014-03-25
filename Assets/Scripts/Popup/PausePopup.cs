using UnityEngine;
using System.Collections;

public class PausePopup : Popup {
  
  public UIAnchor btnCloseAnchor;
  public UIEventTrigger btnClose;
  public UIEventTrigger btnQuit;
  public UIEventTrigger btnSound;
  public UISprite btnSoundBackground;
  
  public override void Init() {
    base.Init();
		EventDelegate.Set (btnClose.onClick, Close);
		EventDelegate.Set (btnQuit.onClick, Quit);
		EventDelegate.Set (btnSound.onClick, SwitchSound);
    btnCloseAnchor.Init();
    GameManager.paused = true;
    if (AudioManager.Instance != null) {
      UpdateSoundButton(AudioManager.Instance.IsSoundOn());
    }
  }
  
  public override void HandleClosePopupCallback() {
    GameManager.paused = false;
    PopupManager.Instance.pausePopupScript = null;
    base.HandleClosePopupCallback();
  }
  
  private void Quit() {
    AdsManager.Instance.HideAds();
    GameManager.Instance.Close();
    CloseNoAnimation();
    ScreenManager.Instance.OpenMainScreen();
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

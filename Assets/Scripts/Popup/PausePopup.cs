using UnityEngine;
using System.Collections;

public class PausePopup : Popup {
  
  public UIAnchor btnCloseAnchor;
  public UIStretch backgroundStretch;
  public UIEventTrigger btnClose;
  public UIEventTrigger btnQuit;
  public UIEventTrigger btnSound;
  
  public override void Init() {
    base.Init();
		EventDelegate.Set (btnClose.onClick, Close);
		EventDelegate.Set (btnQuit.onClick, Quit);
		backgroundStretch.Reset();
    btnCloseAnchor.Init();
    GameManager.paused = true;
  }
  
  public override void HandleClosePopupCallback() {
    GameManager.paused = false;
    PopupManager.Instance.pausePopupScript = null;
    base.HandleClosePopupCallback();
  }
  
  private void Quit() {
    GameManager.Instance.Close();
    CloseNoAnimation();
    ScreenManager.Instance.OpenMainScreen();
  }
}

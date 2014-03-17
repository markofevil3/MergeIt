using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MainScreen : BaseScreen {

	public UIEventTrigger btnPlay;
	public UIEventTrigger btnNext;
	public UIEventTrigger btnPrev;
	public UIEventTrigger btnSetting;
	public UIEventTrigger btnLeaderboard;
	public UIEventTrigger btnHelp;
	public UIEventTrigger btnFacebook;
	public Transform[] themes;
	public UIPanel dragPanel;
	public UIScrollView scrollView;
	
	private int currentTheme = 0;

	public override void Init() {
		EventDelegate.Set (btnPlay.onClick, OpenGameScreen);
		EventDelegate.Set (btnNext.onClick, NextTheme);
		EventDelegate.Set (btnPrev.onClick, PrevTheme);
		EventDelegate.Set (btnSetting.onClick, OpenSettingPopup);
		EventDelegate.Set (btnLeaderboard.onClick, OpenLeaderboard);
		EventDelegate.Set (btnHelp.onClick, OpenHelp);
		EventDelegate.Set (btnFacebook.onClick, ShareFacebook);
	}
	
	private void ShareFacebook() {
	  #if UNITY_IPHONE
	    FacebookBinding.login();
	    FacebookBinding.showFacebookComposer("Test");
      // Facebook.instance.postMessageWithLinkAndLinkToImage("Get 5000 score in Power Of 2", "www.google.com", "Power of 2", "https://dl.dropboxusercontent.com/u/86872228/PowerOf2/logo.png", null, null);
		#endif
		#if UNITY_ANDROID
		  
		#endif
	}
	
	private void NextTheme() {
	  if (currentTheme < themes.Length - 1) {
	    Vector3 offset = -dragPanel.cachedTransform.InverseTransformPoint(themes[currentTheme + 1].position);
  		if (!scrollView.canMoveHorizontally) offset.x = dragPanel.cachedTransform.localPosition.x;
  		if (!scrollView.canMoveVertically) offset.y = dragPanel.cachedTransform.localPosition.y;
  		SpringPanel.Begin(dragPanel.cachedGameObject, offset, 6f);
  		currentTheme += 1;
	  }
	}
	
	private void PrevTheme() {
	  if (currentTheme > 0) {
	    Vector3 offset = -dragPanel.cachedTransform.InverseTransformPoint(themes[currentTheme - 1].transform.position);
  		if (!scrollView.canMoveHorizontally) offset.x = dragPanel.cachedTransform.localPosition.x;
  		if (!scrollView.canMoveVertically) offset.y = dragPanel.cachedTransform.localPosition.y;
  		SpringPanel.Begin(dragPanel.cachedGameObject, offset, 6f);
  		currentTheme -= 1;
	  }
	}
	
	void OpenGameScreen() {
		Close();
		ScreenManager.Instance.mainScreenScript = null;
		ScreenManager.Instance.OpenGameScreen();
	}
	
	void OpenSettingPopup() {
	  PopupManager.Instance.OpenPopup(PopupManager.Type.SETTING);
	}
	
	void OpenLeaderboard() {
	  GameCenterManager.Instance.ShowLeaderboard();
	}
	
	void OpenHelp() {
	  
	}
}

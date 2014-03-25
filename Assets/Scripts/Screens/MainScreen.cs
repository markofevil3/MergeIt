using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MainScreen : BaseScreen {

  public static MainScreen Instance { get; private set; }

	public UIEventTrigger btnNext;
	public UIEventTrigger btnPrev;
	public UIButton btnNextButton;
	public UIButton btnPrevButton;
	public UIEventTrigger btnSetting;
	public UIEventTrigger btnLeaderboard;
	public UIEventTrigger btnHelp;
	public Transform[] themes;
	public MainScreenThemeScript[] themeScripts;
	public UIPanel dragPanel;
	public UIStretch dragPanelStretch;
	public UIScrollView scrollView;
	
	private int achievementScore = 10000;
	private int currentTheme = 0;

	public override void Init() {
	  Instance = this;
    // EventDelegate.Set (btnPlay.onClick, OpenGameScreen);
		EventDelegate.Set (btnNext.onClick, NextTheme);
		EventDelegate.Set (btnPrev.onClick, PrevTheme);
		EventDelegate.Set (btnSetting.onClick, OpenSettingPopup);
		EventDelegate.Set (btnLeaderboard.onClick, OpenLeaderboard);
		EventDelegate.Set (btnHelp.onClick, OpenHelp);
    if (PlayerPrefs.HasKey("theme")) {
      currentTheme = PlayerPrefs.GetInt("theme");
    } else {
      PlayerPrefs.SetInt("theme", 0);
      currentTheme = 0;
    }
    dragPanelStretch.Reset();
    SetCurrentTheme(currentTheme);
	}
	
	private void SetCurrentTheme(int index) {
	  Vector3 offset = -dragPanel.cachedTransform.InverseTransformPoint(themes[index].position);
    Vector2 cr = dragPanel.clipOffset;
    cr.x -= offset.x - dragPanel.cachedTransform.localPosition.x;
    cr.y -= offset.y - - dragPanel.cachedTransform.localPosition.y;
    dragPanel.clipOffset = cr;
    dragPanel.cachedTransform.localPosition = offset;
		currentTheme = index;
		UpdateThemeButton(currentTheme);
	}
	
	private void NextTheme() {
	  if (!InAppPurchase.isPurchasing) {
	    if (currentTheme < themes.Length - 1) {
  	    Vector3 offset = -dragPanel.cachedTransform.InverseTransformPoint(themes[currentTheme + 1].position);
    		if (!scrollView.canMoveHorizontally) offset.x = dragPanel.cachedTransform.localPosition.x;
    		if (!scrollView.canMoveVertically) offset.y = dragPanel.cachedTransform.localPosition.y;
    		SpringPanel.Begin(dragPanel.cachedGameObject, offset, 6f);
    		currentTheme += 1;
    		UpdateThemeButton(currentTheme);
  	  }
	  }
	}
	
	private void UpdateThemeButton(int index) {
	  switch(index) {
	    case 0:
	      themeScripts[index].playText.SetActive(true);
        themeScripts[index].themeAchievement.SetActive(false);
        themeScripts[index].themePurchase.SetActive(false);
	      EventDelegate.Set(themeScripts[index].btnPlay.onClick, OpenGameScreen);
	      btnPrevButton.isEnabled = false;
	      btnNextButton.isEnabled = true;
	    break;
	    case 1:
	      if (PlayerPrefs.HasKey("totalScore") && PlayerPrefs.GetInt("totalScore") >= achievementScore) {
	        themeScripts[index].playText.SetActive(true);
	        themeScripts[index].themeAchievement.SetActive(false);
	        themeScripts[index].themePurchase.SetActive(false);
	        EventDelegate.Set (themeScripts[index].btnPlay.onClick, OpenGameScreen);
	      } else {							
	        themeScripts[index].playText.SetActive(false);
	        themeScripts[index].themeAchievement.SetActive(true);
	        themeScripts[index].themePurchase.SetActive(false);
	        EventDelegate.Remove(themeScripts[index].btnPlay.onClick, OpenGameScreen);
	        EventDelegate.Remove(themeScripts[index].btnPlay.onClick, PurchaseTheme);
	        int score = PlayerPrefs.HasKey("totalScore") ? PlayerPrefs.GetInt("totalScore") : 0;
	        themeScripts[index].achievementProgress.value = (float)score / achievementScore;
	      }
	      btnPrevButton.isEnabled = true;
	      btnNextButton.isEnabled = true;
	    break;
	    case 2:
        if (PlayerPrefs.HasKey("jeweltheme")) {
    		  themeScripts[index].playText.SetActive(true);
          themeScripts[index].themeAchievement.SetActive(false);
          themeScripts[index].themePurchase.SetActive(false);
          EventDelegate.Set(themeScripts[index].btnPlay.onClick, OpenGameScreen);
        } else {
          themeScripts[index].playText.SetActive(false);
          themeScripts[index].themeAchievement.SetActive(false);
          themeScripts[index].themePurchase.SetActive(true);
          EventDelegate.Set(themeScripts[index].btnPlay.onClick, PurchaseTheme);
        }
    		btnPrevButton.isEnabled = true;
	      btnNextButton.isEnabled = false;
	    break;
      // case 3:
      //   if (PlayerPrefs.HasKey("candytheme")) {
      //          themeScripts[index].playText.SetActive(true);
      //           themeScripts[index].themeAchievement.SetActive(false);
      //           themeScripts[index].themePurchase.SetActive(false);
      //           EventDelegate.Set(themeScripts[index].btnPlay.onClick, OpenGameScreen);
      //        } else {
      //          themeScripts[index].playText.SetActive(false);
      //           themeScripts[index].themeAchievement.SetActive(false);
      //           themeScripts[index].themePurchase.SetActive(true);
      //           EventDelegate.Set(themeScripts[index].btnPlay.onClick, PurchaseTheme);
      //        }
      //        btnPrevButton.isEnabled = true;
      //   btnNextButton.isEnabled = false;
      // break;
	  }
	}
	
	public void UpdatePurchasedTheme(int themeIndex) {
	  UpdateThemeButton(themeIndex);
	}
  
	
	private void PurchaseTheme() {
    //    GAEvent myEvent = new GAEvent("Purchase", "Click Purchase");
    // GoogleAnalytics.instance.Add(myEvent);
    // GoogleAnalytics.instance.Dispatch();
		ScreenManager.Instance.TrackEvent("InAppPurchase", "Click Purchase", "Click Purchase", 1);
		
		#if UNITY_IPHONE
	  if (InAppPurchase.Instance != null) {
	      InAppPurchase.Instance.PurchaseProduct(currentTheme);
	  }
    #endif
	  #if UNITY_ANDROID
	  if (InAppPurchaseAndroid.Instance != null) {
	    InAppPurchaseAndroid.Instance.PurchaseProduct(currentTheme);
	  }
		#endif
	}
	
	private void PrevTheme() {
	  if (!InAppPurchase.isPurchasing) {
	    if (currentTheme > 0) {
  	    Vector3 offset = -dragPanel.cachedTransform.InverseTransformPoint(themes[currentTheme - 1].transform.position);
    		if (!scrollView.canMoveHorizontally) offset.x = dragPanel.cachedTransform.localPosition.x;
    		if (!scrollView.canMoveVertically) offset.y = dragPanel.cachedTransform.localPosition.y;
    		SpringPanel.Begin(dragPanel.cachedGameObject, offset, 6f);
    		currentTheme -= 1;
    		UpdateThemeButton(currentTheme);
  	  }
  	}
	}
	
	void OpenGameScreen() {
	  if (!InAppPurchase.isPurchasing) {
	    Close();
  		ScreenManager.Instance.mainScreenScript = null;
  		ScreenManager.Instance.OpenGameScreen(currentTheme);
	  }
	}
	
	void OpenSettingPopup() {
	  if (!InAppPurchase.isPurchasing) {
	    PopupManager.Instance.OpenPopup(PopupManager.Type.SETTING);
	  }
	}
	
	void OpenLeaderboard() {
	  ScreenManager.Instance.TrackEvent("Leaderboard", "OpenLeaderboard", "OpenLeaderboard", 1);
		
    // GAEvent myEvent = new GAEvent("Leaderboard", "OpenLeaderboard");
    // GoogleAnalytics.instance.Add(myEvent);
    // GoogleAnalytics.instance.Dispatch();
	  if (!InAppPurchase.isPurchasing) {
	    GameCenterManager.Instance.ShowLeaderboard();
	  }
	}
	
	void OpenHelp() {
	  if (!InAppPurchase.isPurchasing) {
	    PopupManager.Instance.OpenPopupNoAnimation(PopupManager.Type.TUTORIAL);
	  }
	}
}

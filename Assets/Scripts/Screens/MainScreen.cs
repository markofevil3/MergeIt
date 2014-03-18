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
	public Transform[] themes;
	public UIPanel dragPanel;
	public UIScrollView scrollView;
	public GameObject playText;
	public GameObject themeAchievement;
	public GameObject themePurchase;
	public UIProgressBar achievementProgress;
	
	private int achievementScore = 0;
	private int currentTheme = 0;

	public override void Init() {
		EventDelegate.Set (btnPlay.onClick, OpenGameScreen);
		EventDelegate.Set (btnNext.onClick, NextTheme);
		EventDelegate.Set (btnPrev.onClick, PrevTheme);
		EventDelegate.Set (btnSetting.onClick, OpenSettingPopup);
		EventDelegate.Set (btnLeaderboard.onClick, OpenLeaderboard);
		EventDelegate.Set (btnHelp.onClick, OpenHelp);
		playText.SetActive(true);
    themeAchievement.SetActive(false);
    themePurchase.SetActive(false);
    if (PlayerPrefs.HasKey("theme")) {
      currentTheme = PlayerPrefs.GetInt("theme");
    } else {
      PlayerPrefs.SetInt("theme", 0);
      currentTheme = 0;
    }
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
	  if (currentTheme < themes.Length - 1) {
	    Vector3 offset = -dragPanel.cachedTransform.InverseTransformPoint(themes[currentTheme + 1].position);
  		if (!scrollView.canMoveHorizontally) offset.x = dragPanel.cachedTransform.localPosition.x;
  		if (!scrollView.canMoveVertically) offset.y = dragPanel.cachedTransform.localPosition.y;
  		SpringPanel.Begin(dragPanel.cachedGameObject, offset, 6f);
  		currentTheme += 1;
  		UpdateThemeButton(currentTheme);
	  }
	}
	
	private void UpdateThemeButton(int index) {
	  switch(index) {
	    case 0:
	      playText.SetActive(true);
        themeAchievement.SetActive(false);
        themePurchase.SetActive(false);
	      EventDelegate.Set(btnPlay.onClick, OpenGameScreen);
	    break;
	    case 1:
	      if (PlayerPrefs.HasKey("totalScore") && PlayerPrefs.GetInt("totalScore") >= achievementScore) {
	        playText.SetActive(true);
	        themeAchievement.SetActive(false);
	        themePurchase.SetActive(false);
	        EventDelegate.Set (btnPlay.onClick, OpenGameScreen);
	      } else {
	        playText.SetActive(false);
	        themeAchievement.SetActive(true);
	        themePurchase.SetActive(false);
	        EventDelegate.Remove(btnPlay.onClick, OpenGameScreen);
	        EventDelegate.Remove(btnPlay.onClick, PurchaseTheme);
	        int score = PlayerPrefs.GetInt("totalScore");
	        achievementProgress.value = (float)score / achievementScore;
	      }
	    break;
	    case 2:
	      playText.SetActive(false);
        themeAchievement.SetActive(false);
        themePurchase.SetActive(true);
        EventDelegate.Set(btnPlay.onClick, PurchaseTheme);
	    break;
	    case 3:
	    break;
	  }
	}
	
	private void PurchaseTheme() {
	  Debug.Log("PurchaseTheme-" + currentTheme);
	}
	
	private void PrevTheme() {
	  if (currentTheme > 0) {
	    Vector3 offset = -dragPanel.cachedTransform.InverseTransformPoint(themes[currentTheme - 1].transform.position);
  		if (!scrollView.canMoveHorizontally) offset.x = dragPanel.cachedTransform.localPosition.x;
  		if (!scrollView.canMoveVertically) offset.y = dragPanel.cachedTransform.localPosition.y;
  		SpringPanel.Begin(dragPanel.cachedGameObject, offset, 6f);
  		currentTheme -= 1;
  		UpdateThemeButton(currentTheme);
	  }
	}
	
	void OpenGameScreen() {
		Close();
		ScreenManager.Instance.mainScreenScript = null;
		ScreenManager.Instance.OpenGameScreen(currentTheme);
	}
	
	void OpenSettingPopup() {
	  PopupManager.Instance.OpenPopup(PopupManager.Type.SETTING);
	}
	
	void OpenLeaderboard() {
	  GameCenterManager.Instance.ShowLeaderboard();
	}
	
	void OpenHelp() {
	  PopupManager.Instance.OpenPopupNoAnimation(PopupManager.Type.TUTORIAL);
	}
}

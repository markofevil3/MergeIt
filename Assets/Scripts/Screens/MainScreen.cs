using UnityEngine;
using System.Collections;

public class MainScreen : BaseScreen {

	public UIEventTrigger btnPlay;
	public UIEventTrigger btnNext;
	public UIEventTrigger btnPrev;
	public Transform[] themes;
	public UIPanel dragPanel;
	public UIScrollView scrollView;
	
	private int currentTheme = 0;

	public override void Init() {
		EventDelegate.Set (btnPlay.onClick, OpenGameScreen);
		EventDelegate.Set (btnNext.onClick, NextTheme);
		EventDelegate.Set (btnPrev.onClick, PrevTheme);
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
	
}

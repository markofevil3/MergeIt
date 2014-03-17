﻿using UnityEngine;
using System.Collections;

public class TutorialPopup : Popup {

  public UIEventTrigger btnClose;
  public UITable table;
	public Transform[] tutorials;
	public UIEventTrigger[] tutorialEvents;
	public UIPanel dragPanel;
	public UIScrollView scrollView;
	
	public UIAnchor tableAnchor;
	public UIStretch[] tutorialStretch;
	public UIStretch panelStretch;
	public UIStretch titleBgStretch;
	
	private int currentTut = 0;

  public override void Init() {
    base.Init();
 		EventDelegate.Set (btnClose.onClick, CloseNoAnimation);
 		EventDelegate.Set (tutorialEvents[0].onClick, ChangeTutorial);
 		EventDelegate.Set (tutorialEvents[1].onClick, ChangeTutorial);
 		panelStretch.Reset();
 		titleBgStretch.Reset();
 		for (int i = 0; i < tutorialStretch.Length; i++) {
 		  tutorialStretch[i].Reset();
 		}
 		tableAnchor.Init();
 		table.Reset();
    GameManager.paused = true;
  }

  private void ChangeTutorial() {
    int nextTut = (currentTut + 1) % tutorials.Length;    
    Vector3 offset = -dragPanel.cachedTransform.InverseTransformPoint(tutorials[nextTut].position);
		if (!scrollView.canMoveHorizontally) offset.x = dragPanel.cachedTransform.localPosition.x;
		if (!scrollView.canMoveVertically) offset.y = dragPanel.cachedTransform.localPosition.y;
		SpringPanel.Begin(dragPanel.cachedGameObject, offset, 6f);
		currentTut = nextTut;
  }
  
  public override void HandleClosePopupCallback() {
    GameManager.paused = false;
    PopupManager.Instance.tutorialPopupScript = null;
    base.HandleClosePopupCallback();
  }
}
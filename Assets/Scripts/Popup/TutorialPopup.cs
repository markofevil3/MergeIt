using UnityEngine;
using System.Collections;

public class TutorialPopup : Popup {

  public UIEventTrigger btnClose;
  public UIEventTrigger tapToContinueEvent;
  public UIGrid table;
	public Transform[] tutorials;
	public UIEventTrigger[] tutorialEvents;
	public UIPanel dragPanel;
	public UIScrollView scrollView;
	
  // public UIStretch[] tutorialStretch;
	public UIStretch panelStretch;
	public UIStretch titleBgStretch;
	
	private int currentTut = 0;

  public override void Init() {
    base.Init();
 		EventDelegate.Set (btnClose.onClick, CloseNoAnimation);
 		EventDelegate.Set (tutorialEvents[0].onClick, ChangeTutorial);
    // if (GameManager.Instance != null && GameManager.Instance.IsFirstPlay()) {
    //  btnClose.gameObject.SetActive(false);
    //  EventDelegate.Set (tutorialEvents[1].onClick, CloseNoAnimation);
    // } else {
		EventDelegate.Set (tutorialEvents[1].onClick, CloseNoAnimation);
    // }
		EventDelegate.Set (tapToContinueEvent.onClick, ChangeTutorial);
 		panelStretch.Reset();
 		titleBgStretch.Reset();
    // for (int i = 0; i < tutorialStretch.Length; i++) {
    //   tutorialStretch[i].Reset();
    // }
 		table.Reset();
 		
 		Vector3 offset = -dragPanel.cachedTransform.InverseTransformPoint(tutorials[0].position);
    Vector2 cr = dragPanel.clipOffset;
    cr.x -= offset.x - dragPanel.cachedTransform.localPosition.x;
    cr.y -= offset.y - - dragPanel.cachedTransform.localPosition.y;
    dragPanel.clipOffset = cr;
    dragPanel.cachedTransform.localPosition = offset;
    
    GameManager.paused = true;
  }

  private void ChangeTutorial() {
    int nextTut = (currentTut + 1) % tutorials.Length;    
    Vector3 offset = -dragPanel.cachedTransform.InverseTransformPoint(tutorials[nextTut].position);
		if (!scrollView.canMoveHorizontally) offset.x = dragPanel.cachedTransform.localPosition.x;
		if (!scrollView.canMoveVertically) offset.y = dragPanel.cachedTransform.localPosition.y;
		SpringPanel.Begin(dragPanel.cachedGameObject, offset, 6f);
		currentTut = nextTut;
		if (GameManager.Instance != null && GameManager.Instance.IsFirstPlay() && nextTut == 0) {
			EventDelegate.Set (tapToContinueEvent.onClick, CloseNoAnimation);
		}
  }
  
  public override void HandleClosePopupCallback() {
		if (GameManager.Instance != null && GameManager.Instance.IsFirstPlay()) {
			AdsManager.Instance.ShowAds();
		}
    GameManager.paused = false;
    PopupManager.Instance.tutorialPopupScript = null;
    base.HandleClosePopupCallback();
  }
}

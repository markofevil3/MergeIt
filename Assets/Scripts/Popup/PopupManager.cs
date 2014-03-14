﻿using UnityEngine;
using System.Collections;

public class PopupManager : MonoBehaviour {

  public enum Type {
    PAUSE
  }
  private float dimAnimateSpeed = 0.1f;
  public static PopupManager Instance { get; private set; }
  
  public GameObject dimBackground;
  public UISprite dimBackgroundSprite;
  public GameObject pausePopupPrefab;
	
	public PausePopup pausePopupScript;
	
	// Use this for initialization
	void Awake () {
	  Instance = this;
	  dimBackground.GetComponent<UIWidget>().ResizeCollider();
	  dimBackground.SetActive(false);
	}
  
  public void ShowDim() {
    dimBackground.SetActive(true);
    TweenAlpha tween = TweenAlpha.Begin(dimBackground, dimAnimateSpeed, 1.0f);
		tween.from = 0;
  }
  
  public void HideDim() {
		LeanTween.cancel(dimBackground);
		LeanTween.value(dimBackground, DoUpdateDimAlpha, dimBackgroundSprite.alpha, 0, dimAnimateSpeed, new Hashtable() {
			{"onComplete", "HideDimCallback"},
			{"onCompleteTarget", gameObject}
		});
	}
  
  public void HideDimNoAnimation() {
    dimBackgroundSprite.alpha = 0;
		dimBackground.SetActive(false);
	}
  
  private void DoUpdateDimAlpha(float updateVal) {
		dimBackgroundSprite.alpha = updateVal;
	}
  
  private void HideDimCallback() {
    dimBackground.SetActive(false);
  }
  
  public void OpenPopup(Type type) {
    switch(type) {
      case Type.PAUSE:
        if (pausePopupScript == null) {
    			GameObject tempGameObject = NGUITools.AddChild(gameObject, pausePopupPrefab);
    			tempGameObject.name = "PausePopup";
    			pausePopupScript = tempGameObject.GetComponent<PausePopup>();
    			pausePopupScript.Init();
    			pausePopupScript.Open();
    		}
      break;
    }
  }
  
}

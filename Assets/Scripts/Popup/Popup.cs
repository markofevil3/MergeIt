using UnityEngine;
using System.Collections;

public class Popup : MonoBehaviour {
  
  private Vector3 maxScale = new Vector3(1.02f, 1.02f, 1.0f);
  private Vector3 minScale = new Vector3(0.97f, 0.97f, 1.0f);
  
  public Transform thisTransform;
  public UIPanel thisPanel;
  private bool isAnimating = false;
  
  public virtual void Init() {
    thisTransform = transform;
    thisPanel = gameObject.GetComponent<UIPanel>();
  }
  
  public virtual void Init(object[] data) {
    thisTransform = transform;
    thisPanel = gameObject.GetComponent<UIPanel>();
  }
  
  public virtual void Open() {
    if (!isAnimating) {
      if (thisPanel.alpha > 0) {
  			thisPanel.alpha = 0;
  		}
  		thisTransform.localScale = minScale;
  		gameObject.SetActive(true);
  		BeforeOpen();
  		BounceUpAnimation();
  		PopupManager.Instance.ShowDim();
  		isAnimating = true;
    }
  }
  
  public virtual void BeforeOpen() {}
  
  void BounceUpAnimation() {
	  LeanTween.cancel(gameObject);
		LeanTween.scale(gameObject, maxScale, 0.15f, new Hashtable() {
			{"onComplete", "BounceUpAnimationCallback"},
			{"onCompleteTarget", gameObject}
		});
		LeanTween.value(thisPanel.gameObject, DoUpdatePanelAlpha, thisPanel.alpha, 1, 0.25f);
	}
  
  void DoUpdatePanelAlpha(float updateVal) {
		thisPanel.alpha = updateVal;
	}
	
	void BounceUpAnimationCallback() {
		LeanTween.scale(gameObject, Vector3.one, 0.1f, new Hashtable() {
			{"onComplete", "HandleOpenPopupCallback"},
			{"onCompleteTarget", gameObject}
		});
	}
  
  void BounceDownAnimation() {
		LeanTween.scale(gameObject, maxScale, 0.1f, new Hashtable() {
			{"onComplete", "BounceDownAnimationCallback"},
			{"onCompleteTarget", gameObject}
		});
		LeanTween.value(thisPanel.gameObject, DoUpdatePanelAlpha, thisPanel.alpha, 0, 0.25f);
	}
	
	void BounceDownAnimationCallback() {
		LeanTween.scale(gameObject, minScale, 0.15f, new Hashtable() {
			{"onComplete", "HandleClosePopupCallback"},
			{"onCompleteTarget", gameObject}
		});
	}
  
  public virtual void HandleOpenPopupCallback() {
    isAnimating = false;
  }
  public virtual void HandleClosePopupCallback() {
    isAnimating = false;
    Destroy(gameObject);
  }
  
  public virtual void Close() {
    if (!isAnimating) {
      isAnimating = true;
      BounceDownAnimation();
      PopupManager.Instance.HideDim();
    }
  }
  
  public virtual void CloseNoAnimation() {
    isAnimating = false;
    Destroy(gameObject);
    PopupManager.Instance.HideDimNoAnimation();
  }
}

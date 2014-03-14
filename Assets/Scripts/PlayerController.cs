using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

	Vector2 firstPressPos;
	Vector2 secondPressPos;
	Vector2 currentSwipe;
	float dragThreshold = 0.3f;

	public void TouchSwipe() {
		if (Input.touches.Length > 0) {
			Touch t = Input.GetTouch(0);
			if(t.phase == TouchPhase.Began) {
			//save began touch 2d point
				firstPressPos = new Vector2(t.position.x,t.position.y);
			}
			if (t.phase == TouchPhase.Ended) {
			//save ended touch 2d point
				secondPressPos = new Vector2(t.position.x,t.position.y);
				 //create vector from the two points
				currentSwipe = new Vector3(secondPressPos.x - firstPressPos.x, secondPressPos.y - firstPressPos.y);
				//normalize the 2d vector
				currentSwipe.Normalize();
				//swipe upwards
				if (currentSwipe.y > 0 && currentSwipe.x > -dragThreshold && currentSwipe.x < dragThreshold) {
				  GameManager.Instance.Move(GameManager.Direction.UP);
				}
				//swipe down
				if (currentSwipe.y < 0 && currentSwipe.x > -dragThreshold && currentSwipe.x < dragThreshold) {
				  GameManager.Instance.Move(GameManager.Direction.DOWN);
				}
				//swipe left
				if (currentSwipe.x < 0 && currentSwipe.y > -dragThreshold && currentSwipe.y < dragThreshold) {
				  GameManager.Instance.Move(GameManager.Direction.LEFT);
				}
				//swipe right
				if(currentSwipe.x > 0 && currentSwipe.y > -dragThreshold && currentSwipe.y < dragThreshold) {
				  GameManager.Instance.Move(GameManager.Direction.RIGHT);
				}
			}
		}
	}

	public void MouseSwipe() {
		if (Input.GetMouseButtonDown(0)) {
		    //save began touch 2d point
			firstPressPos = new Vector2(Input.mousePosition.x,Input.mousePosition.y);
		}
		if (Input.GetMouseButtonUp(0)) {
      //save ended touch 2d point
		  secondPressPos = new Vector2(Input.mousePosition.x,Input.mousePosition.y);
      //create vector from the two points
		  currentSwipe = new Vector2(secondPressPos.x - firstPressPos.x, secondPressPos.y - firstPressPos.y); 
		  //normalize the 2d vector
		  currentSwipe.Normalize();
		  //swipe upwards
		  if (currentSwipe.y > 0 && currentSwipe.x > -dragThreshold && currentSwipe.x < dragThreshold) {
	      GameManager.Instance.Move(GameManager.Direction.UP);
		  }
		  //swipe down
		  if (currentSwipe.y < 0 && currentSwipe.x > -dragThreshold && currentSwipe.x < dragThreshold){
	      GameManager.Instance.Move(GameManager.Direction.DOWN);
		  }
		  //swipe left
		  if (currentSwipe.x < 0 && currentSwipe.y > -dragThreshold && currentSwipe.y < dragThreshold) {
			  GameManager.Instance.Move(GameManager.Direction.LEFT);
		  }
		  //swipe right
		  if (currentSwipe.x > 0 && currentSwipe.y > -dragThreshold && currentSwipe.y < dragThreshold) {
			  GameManager.Instance.Move(GameManager.Direction.RIGHT);
		  }
		}
	}

  void Update() {
		#if UNITY_EDITOR
			MouseSwipe();
			if(Input.GetButtonDown("up")){
			  GameManager.Instance.Move(GameManager.Direction.UP);
			}
			if(Input.GetButtonDown("down")){
			  GameManager.Instance.Move(GameManager.Direction.DOWN);
			}
			if(Input.GetButtonDown("left")){
			  GameManager.Instance.Move(GameManager.Direction.LEFT);
			}
			if(Input.GetButtonDown("right")){
			  GameManager.Instance.Move(GameManager.Direction.RIGHT);
			}
		#else
			TouchSwipe();
		#endif
  }
}
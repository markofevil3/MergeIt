using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

	Vector2 firstPressPos;
	Vector2 secondPressPos;
	Vector2 currentSwipe;
	float dragThreshold = 0.5f;
	Touch t;

  void Awake() {
  }

  private float fingerStartTime;
  private Vector2 fingerStartPos = Vector2.zero;

  private bool isSwipe = false;
  private float minSwipeDist = 50.0f;
  private float maxSwipeTime = 0.5f;
  float gestureTime;
  float gestureDist;

  // main function:
  void TouchSwipe() {
    if (Input.touches.Length > 0) {
      t = Input.GetTouch(0);
      switch (t.phase){
        case TouchPhase.Began :
          /* this is a new touch */
          isSwipe = true;
          fingerStartTime = Time.time;
          fingerStartPos = t.position;
        break;
        case TouchPhase.Canceled :
          /* The touch is being canceled */
          isSwipe = false;
        break;
        case TouchPhase.Ended :
          gestureTime = Time.time - fingerStartTime;
          gestureDist = (t.position - fingerStartPos).magnitude;
          if (isSwipe && gestureTime < maxSwipeTime && gestureDist > minSwipeDist) {
            Vector2 direction = t.position - fingerStartPos;
            Vector2 swipeType = Vector2.zero; 
            if (Mathf.Abs(direction.x) > Mathf.Abs(direction.y)){
              // the swipe is horizontal:
              swipeType = Vector2.right * Mathf.Sign(direction.x);
              if (swipeType.x == 1) {
                //LEFT
                GameManager.Instance.Move(GameManager.Direction.LEFT);
              } else {
                //RIGHT
                GameManager.Instance.Move(GameManager.Direction.RIGHT);
              }
            } else {
              // the swipe is vertical:
              swipeType = Vector2.up * Mathf.Sign(direction.y);
              if (swipeType.y == 1) {
                GameManager.Instance.Move(GameManager.Direction.UP);
                // UP
              } else {
                // DOWN
                GameManager.Instance.Move(GameManager.Direction.DOWN);
              }
            }
          }
        break;
      }
    }
  }

  

  // public void TouchSwipe() {
  //  if (Input.touches.Length > 0) {
  //    t = Input.GetTouch(0);
  //    if(t.phase == TouchPhase.Began) {
  //    //save began touch 2d point
  //      firstPressPos = new Vector2(t.position.x,t.position.y);
  //    }
  //    if (t.phase == TouchPhase.Ended) {
  //    //save ended touch 2d point
  //      secondPressPos = new Vector2(t.position.x,t.position.y);
  //       //create vector from the two points
  //      currentSwipe = new Vector3(secondPressPos.x - firstPressPos.x, secondPressPos.y - firstPressPos.y);
  //      //normalize the 2d vector
  //      currentSwipe.Normalize();
  //      //swipe upwards
  //      if (currentSwipe.y > 0 && currentSwipe.x > -dragThreshold && currentSwipe.x < dragThreshold) {
  //        GameManager.Instance.Move(GameManager.Direction.UP);
  //      }
  //      //swipe down
  //      if (currentSwipe.y < 0 && currentSwipe.x > -dragThreshold && currentSwipe.x < dragThreshold) {
  //        GameManager.Instance.Move(GameManager.Direction.DOWN);
  //      }
  //      //swipe left
  //      if (currentSwipe.x < 0 && currentSwipe.y > -dragThreshold && currentSwipe.y < dragThreshold) {
  //        GameManager.Instance.Move(GameManager.Direction.LEFT);
  //      }
  //      //swipe right
  //      if(currentSwipe.x > 0 && currentSwipe.y > -dragThreshold && currentSwipe.y < dragThreshold) {
  //        GameManager.Instance.Move(GameManager.Direction.RIGHT);
  //      }
  //    }
  //  }
  // }

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
			  GameManager.Instance.Move(GameManager.Direction.RIGHT);
		  }
		  //swipe right
		  if (currentSwipe.x > 0 && currentSwipe.y > -dragThreshold && currentSwipe.y < dragThreshold) {
			  GameManager.Instance.Move(GameManager.Direction.LEFT);
		  }
		}
	}

  void Update() {
    if (GameManager.started && !GameManager.paused && !GameManager.isMoving) {
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
}
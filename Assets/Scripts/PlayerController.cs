using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

  public GameManager gameManager;
  // public Transform player; // Drag your player here
  // private Vector2 fp; // first finger position
  // private Vector2 lp; // last finger position
  // 
  // void Update () {
  //   foreach(Touch touch in Input.touches) {
  //     if (touch.phase == TouchPhase.Began) {
  //       fp = touch.position;
  //       lp = touch.position;
  //     }
  //     if (touch.phase == TouchPhase.Moved ) {
  //       lp = touch.position;
  //     }
  //     if(touch.phase == TouchPhase.Ended) {
  //       if((fp.x - lp.x) > 80) { // left swipe
  //         Debug.Log("Left");
  //       } else if((fp.x - lp.x) < -80) { // right swipe 
  //         Debug.Log("Right");
  //       } else if((fp.y - lp.y) < -80 ) {  // up swipe
  //         Debug.Log("Up");
  //       } else {
  //         Debug.Log("Down");
  //       }
  //     }
  //   }
  // }
  
  void Update() {
    
    if(Input.GetButtonDown("up")){
		  gameManager.Move(GameManager.Direction.UP);
		}
		if(Input.GetButtonDown("down")){
		  gameManager.Move(GameManager.Direction.DOWN);
		}
		if(Input.GetButtonDown("left")){
		  gameManager.Move(GameManager.Direction.LEFT);
		}
		if(Input.GetButtonDown("right")){
		  gameManager.Move(GameManager.Direction.RIGHT);
		}
  }
  
}


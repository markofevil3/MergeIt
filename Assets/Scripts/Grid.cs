using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Grid : MonoBehaviour {
  public int x;
  public int y;
  public int gridValue;
  public Transform thisTransform;
	public UISprite background;
  
  public void Init(Position pos, int gridValue) {
    thisTransform = transform;
    this.x = pos.x;
    this.y = pos.y;
    this.gridValue = gridValue;
  }
  
  public bool IsAvailable() {
    return gridValue == 0;
  }
}

// grid position data
public struct Position {
	public int x;
	public int y;

 	public Position(int x, int y){
  	this.x = x;
    this.y = y;
 	}
}

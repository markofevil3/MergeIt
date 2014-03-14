using UnityEngine;
using System.Collections;

public class Tile : MonoBehaviour {
  
  public int x;
  public int y;
  public int tileValue;
  public Tile mergeFromTile;
  public Position previousPosition;
  
  public UILabel tileValueLabel;

  public void Init(Position pos, int value, Vector3 transPos) {
    this.x = pos.x;
    this.y = pos.y;
    this.tileValue = value;
    tileValueLabel.text = value.ToString();
    transform.position = transPos;
  }
  
  public void SavePosition() {
    previousPosition = new Position(x, y);
  }

  public void UpdatePosition(Position pos) {
    x = pos.x;
    y = pos.y;
    LeanTween.move(gameObject, GridManager.Instance.GetCell(pos).thisTransform.position, 0.1f);
    // transform.position = GameManager.Instance.gridManager.GetCell(pos).thisTransform.position;
  }
  
  public void GetMerge(Position pos, int value, Tile fromTile) {
    mergeFromTile = fromTile;
    x = pos.x;
    y = pos.y;
    tileValue = value;
    tileValueLabel.text = value.ToString();
    LeanTween.move(gameObject, GridManager.Instance.GetCell(pos).thisTransform.position, 0.1f);
    
    // transform.position = GameManager.Instance.gridManager.GetCell(pos).thisTransform.position;
  }
  
  public void Remove() {
    Destroy(gameObject);
  }
  
  public void Reset() {
    mergeFromTile = null;
    SavePosition();
  }
}
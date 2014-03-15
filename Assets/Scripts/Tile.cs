using UnityEngine;
using System.Collections;

public class Tile : MonoBehaviour {
  
  public int x;
  public int y;
  public int tileValue;
  public Tile mergeFromTile;
  public Position previousPosition;
  
  public UILabel tileValueLabel;
  public UISprite tileBackground;
  public UIPlayTween playTween;

  public void Init(Position pos, int value, Vector3 transPos) {
    this.x = pos.x;
    this.y = pos.y;
    this.tileValue = value;
    tileBackground.spriteName = "tileTheme1_" + value;
    tileBackground.width = tileBackground.height = GameManager.Instance.cellSize;
    // tileValueLabel.text = value.ToString();
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
  
  void FinishMovingMerge() {
    tileBackground.spriteName = "tileTheme1_" + tileValue;
    playTween.Play(true);
  }
  
  public void GetMerge(Position pos, int value, Tile fromTile) {
    mergeFromTile = fromTile;
    x = pos.x;
    y = pos.y;
    tileValue = mergeFromTile.tileValue * 2;
    LeanTween.move(gameObject, GridManager.Instance.GetCell(pos).thisTransform.position, 0.1f).setOnComplete(FinishMovingMerge);
  }
  
  public void Remove() {
    Invoke("DeleteTile", 0.1f);
  }
  
  private void DeleteTile() {
    Destroy(gameObject);
  }
  
  public void Reset() {
    mergeFromTile = null;
    SavePosition();
  }
}
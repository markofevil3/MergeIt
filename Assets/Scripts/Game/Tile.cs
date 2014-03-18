using UnityEngine;
using System.Collections;

public class Tile : MonoBehaviour {
  
  public int x;
  public int y;
  public int tileValue;
  public Tile mergeFromTile;
  public Position previousPosition;

	private int glowingValue = 512;
  
  public UISprite tileBackground;
  public UISprite glowingBackground;
  public UIPlayTween playTween;

  public void Init(Position pos, int value, Vector3 transPos) {
    this.x = pos.x;
    this.y = pos.y;
    this.tileValue = value;
    tileBackground.atlas = GameManager.Instance.currentAtlas;
    tileBackground.spriteName = "tileTheme1_" + value;
    tileBackground.width = tileBackground.height = GameManager.Instance.cellSize;
    glowingBackground.width = glowingBackground.height = (int)(GameManager.Instance.cellSize * 1.15f);
    // tileValueLabel.text = value.ToString();
    transform.position = transPos;
		if (GameManager.Instance.currentTheme == 0 && tileValue >= glowingValue) {
			glowingBackground.gameObject.SetActive(true);
		} else {
			glowingBackground.gameObject.SetActive(false);
		}
  }
  
  public void SavePosition() {
    previousPosition = new Position(x, y);
  }

  public void UpdatePosition(Position pos) {
    x = pos.x;
    y = pos.y;
    tileBackground.depth = 5;
    LeanTween.move(gameObject, GridManager.Instance.GetCell(pos).thisTransform.position, 0.1f).setOnComplete(SetDepthToNormal);
    // transform.position = GameManager.Instance.gridManager.GetCell(pos).thisTransform.position;
  }
  
  void SetDepthToNormal() {
    tileBackground.depth = 4;
  }
  
  void FinishMovingMerge() {
    tileBackground.spriteName = "tileTheme1_" + tileValue;
    playTween.Play(true);
    SetDepthToNormal();
  }
  
  public void GetMerge(Position pos, int value, Tile fromTile) {
    mergeFromTile = fromTile;
    x = pos.x;
    y = pos.y;
    tileValue = mergeFromTile.tileValue * 2;
    tileBackground.depth = 5;
		if (GameManager.Instance.currentTheme == 0 && tileValue >= glowingValue) {
			glowingBackground.gameObject.SetActive(true);
		} else {
			glowingBackground.gameObject.SetActive(false);
		}
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
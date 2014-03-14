using UnityEngine;
using System;
using System.Collections;
using System.Globalization;

public class GameManager : BaseScreen {
  
  private int gameSize = 4;
  private int startTiles = 2;
	private int wonTileValue = 2048;
	private int score;
  
  // public GridManager gridManager;
  // public TileManager tileManager;
	public UISprite cellBackground;
	public UILabel scoreLabel;
  public static GameManager Instance { get; private set; }
  
	public enum Direction {
    UP,
    DOWN,
    LEFT,
    RIGHT
  }
  
	public override void Init() {
		Instance = this;
		transform.GetComponent<GridManager>().Init(gameSize);
		transform.GetComponent<TileManager>().Init();
    // GridManager.Instance.Init(gameSize);
    AddStartTiles();
		score = 0;
	}
  
	// void Start() {
	// 
	//   }
  
  void AddStartTiles() {
    for(int i = 0; i < startTiles; i++) {
      AddRandomTile();
    }
  }
  
  void AddRandomTile() {
    if (GridManager.Instance.IsCellsAvailable()) {
      int tileValue = UnityEngine.Random.value < 0.9 ? 2 : 4;
      Grid tempGrid = GridManager.Instance.RandomAvailableCell();
      TileManager.Instance.CreateNewTile(new Position(tempGrid.x, tempGrid.y), tileValue, tempGrid.thisTransform.position);
      // var tile = new Tile(this.grid.randomAvailableCell(), value);
      GridManager.Instance.InsertTile(tempGrid, tileValue);
    }
  }
  
  public void Move(Direction direction) {
    DirectionVector vector = MapDirectionToVector(direction);
    int[][] traversals = BuildTraversals(vector);
    bool moved      = false;
    
    TileManager.Instance.ResetAllTilesData();
    foreach (int x in traversals[0]) {
      foreach (int y in traversals[1]) {
        Tile tile = GridManager.Instance.GetCellContent(new Position(x, y));
        if (tile != null) {
          Position[] positions = FindFarthestPosition(new Position(tile.x, tile.y), vector);
          Tile nextTile = GridManager.Instance.GetCellContent(new Position(positions[1].x, positions[1].y));
          if (nextTile != null && nextTile.tileValue == tile.tileValue && nextTile.mergeFromTile == null) {
            GridManager.Instance.InsertTile(GridManager.Instance.GetCell(new Position(nextTile.x, nextTile.y)), nextTile.tileValue * 2);
            GridManager.Instance.RemoveTile(GridManager.Instance.GetCell(new Position(tile.x, tile.y)));
            tile.GetMerge(new Position(nextTile.x, nextTile.y), nextTile.tileValue * 2, nextTile);
            TileManager.Instance.RemoveTile(nextTile);
						score += tile.tileValue;
						scoreLabel.text = score.ToString();
						if (tile.tileValue == wonTileValue) {
							Debug.Log("WON");
						}
          } else {
            MoveTile(tile, positions[0]);
          }
          if (!PositionsEqual(new Position(x, y), new Position(tile.x, tile.y))) {
            moved = true;
          }
        }
      }
    }
    if (moved) {
			Invoke("AddRandomTile", 0.1f);
      // AddRandomTile();
    }
  }
  
  private void MoveTile(Tile tile, Position pos) {
    GridManager.Instance.SetCellValue(new Position(tile.x, tile.y), 0);
    GridManager.Instance.SetCellValue(pos, tile.tileValue);
    tile.UpdatePosition(pos);
  }
  
  private bool PositionsEqual(Position cellPos, Position tilePos) {
    return cellPos.x == tilePos.x && cellPos.y == tilePos.y;
  }
  
  private Position[] FindFarthestPosition(Position cellPos, DirectionVector vector) {
    Position previous;

    // Progress towards the vector direction until an obstacle is found
    do {
      previous = cellPos;
      cellPos.x = previous.x + vector.x;
      cellPos.y = previous.y + vector.y;
    } while (GridManager.Instance.WithinBounds(cellPos) &&
             GridManager.Instance.CellAvailable(cellPos));
    return new Position[2] {previous, cellPos};
  }
  
  private int[][] BuildTraversals(DirectionVector vector) {
    int[][] traversals = new int[2][];
    int[] x = new int[gameSize];
    int[] y = new int[gameSize];
    for (int pos = 0; pos < gameSize; pos++) {
      x[pos] = pos;
      y[pos] = pos;
    }

    // Always traverse from the farthest cell in the chosen direction
    if (vector.x == 1) Array.Reverse(x);
    if (vector.y == 1) Array.Reverse(y);
    traversals[0] = x;
    traversals[1] = y;
    return traversals;
  }
  
  private DirectionVector MapDirectionToVector(Direction direction) {
    switch(direction) {
      case Direction.UP:
        return new DirectionVector(0, -1);
      break;
      case Direction.DOWN:
        return new DirectionVector(0, 1);
      break;
      case Direction.LEFT:
        return new DirectionVector(-1, 0);
      break;
      case Direction.RIGHT:
        return new DirectionVector(1, 0);
      break;
    }
    Debug.Log("Cant map direction");
    return new DirectionVector(0, 0);
  }
}

// grid position data
public struct DirectionVector {
	public int x;
	public int y;

 	public DirectionVector(int x, int y){
  	this.x = x;
    this.y = y;
 	}
}

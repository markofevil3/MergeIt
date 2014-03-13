using UnityEngine;
using System;
using System.Collections;
using System.Globalization;

public class GameManager : MonoBehaviour {
  
  private int gameSize = 4;
  private int startTiles = 2;
  
  public GridManager gridManager;
  public TileManager tileManager;
  public static GameManager Instance { get; private set; }
  
	public enum Direction {
    UP,
    DOWN,
    LEFT,
    RIGHT
  }
  
  void Start() {
    Instance = this;
    gridManager.Init(gameSize);
    AddStartTiles();
  }
  
  void AddStartTiles() {
    for(int i = 0; i < startTiles; i++) {
      AddRandomTile();
    }
  }
  
  void AddRandomTile() {
    if (gridManager.IsCellsAvailable()) {
      int tileValue = UnityEngine.Random.value < 0.9 ? 2 : 4;
      Grid tempGrid = gridManager.RandomAvailableCell();
      tileManager.CreateNewTile(new Position(tempGrid.x, tempGrid.y), tileValue, tempGrid.thisTransform.position);
      // var tile = new Tile(this.grid.randomAvailableCell(), value);
      gridManager.InsertTile(tempGrid, tileValue);
    }
  }
  
  public void Move(Direction direction) {
    Debug.Log("move " + direction);
    
    DirectionVector vector = MapDirectionToVector(direction);
    int[][] traversals = BuildTraversals(vector);
    bool moved      = false;
    
    tileManager.ResetAllTilesData();
    foreach (int x in traversals[0]) {
      foreach (int y in traversals[1]) {
        Tile tile = gridManager.GetCellContent(new Position(x, y));
        if (tile != null) {
          Position[] positions = FindFarthestPosition(new Position(tile.x, tile.y), vector);
          Tile nextTile = gridManager.GetCellContent(new Position(positions[1].x, positions[1].y));
          if (nextTile != null && nextTile.tileValue == tile.tileValue && nextTile.mergeFromTile == null) {
            Debug.Log("Merge " + tile.x + " " + tile.y);
            gridManager.InsertTile(gridManager.GetCell(new Position(nextTile.x, nextTile.y)), nextTile.tileValue * 2);
            gridManager.RemoveTile(gridManager.GetCell(new Position(tile.x, tile.y)));
            tile.GetMerge(new Position(nextTile.x, nextTile.y), nextTile.tileValue * 2, nextTile);
            tileManager.RemoveTile(nextTile);
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
      AddRandomTile();
    }
  }
  
  private void MoveTile(Tile tile, Position pos) {
    gridManager.SetCellValue(new Position(tile.x, tile.y), 0);
    gridManager.SetCellValue(pos, tile.tileValue);
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
    } while (gridManager.WithinBounds(cellPos) &&
             gridManager.CellAvailable(cellPos));
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

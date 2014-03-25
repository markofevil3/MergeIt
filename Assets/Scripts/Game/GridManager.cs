using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GridManager : MonoBehaviour {

  public UIGrid cellTable;
  public GameObject cellPrefab;

  public static GridManager Instance { get; private set; }
  
  private int size;
  private Grid[,] cells;
  
  public void Init(int size) {
		Instance = this;
    this.size = size;
    cells = new Grid[size, size];
    for (int i = 0; i < size; i++) {
      for (int j = 0; j < size; j++) {
        GameObject tempCell = NGUITools.AddChild(cellTable.gameObject, cellPrefab);
        tempCell.name = i.ToString() + j.ToString();
        Grid tempGrid = tempCell.GetComponent<Grid>();
        tempGrid.Init(new Position(i, j), 0);
        cells[i, j] = tempGrid;
      }
    }
    cellTable.Reset();
  }

  public void Restart() {
    cells = new Grid[size, size];
  }

  public Grid RandomAvailableCell() {
    List<Grid> availableCells = AvailableCells();
    return availableCells[(int)Random.Range(0, availableCells.Count - 1)];
  }
  
  private List<Grid> AvailableCells() {
    List<Grid> availableCells = new List<Grid>();
    // availableCells.Clear();
    for (int i = 0; i < size; i++) {
      for (int j = 0; j < size; j++) {
        if (cells[i, j].gridValue == 0) {
          availableCells.Add(cells[i, j]);
        }
      }
    }
    return availableCells;
  }
  
  public bool IsCellsAvailable() {
    return AvailableCells().Count > 0;
  }
  
  public void InsertTile(Grid grid, int tileValue) {
    grid.gridValue = tileValue;
  }
  
  public void RemoveTile(Grid grid) {
    grid.gridValue = 0;
  }
  
  public bool CellAvailable(Position pos) {
    return cells[pos.x, pos.y].IsAvailable();
    // return !this.cellOccupied(cell);
  }
  
  public Tile GetCellContent(Position pos) {
    return TileManager.Instance.GetTileAtPos(pos);
  }
  
  public void SetCellValue(Position pos, int value) {
    cells[pos.x, pos.y].gridValue = value;
  }
  
  public Grid GetCell(Position pos) {
    return cells[pos.x, pos.y];
  }
  
  public bool WithinBounds(Position pos) {
    return pos.x >= 0 && pos.x < size &&
           pos.y >= 0 && pos.y < size;
  }
}

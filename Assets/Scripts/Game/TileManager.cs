using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TileManager : MonoBehaviour {
  
  public GameObject tilePrefab;
  public GameObject listTilesPrefab;
  public List<Tile> allTiles = new List<Tile>();
  
  public static TileManager Instance { get; private set; }

	public void Init() {
		Instance = this;
	}

  public void Restart() {
    allTiles.Clear();
  }

  public Tile GetCreateNewTile(Position pos, int tileValue, Vector3 transPos) {
    GameObject tempTileGameObject = NGUITools.AddChild(listTilesPrefab, tilePrefab);
    Tile tempTile = tempTileGameObject.GetComponent<Tile>();
    tempTile.Init(pos, tileValue, transPos);
    allTiles.Add(tempTile);
    return tempTile;
  }
  
  public void CreateNewTile(Position pos, int tileValue, Vector3 transPos) {
    GameObject tempTileGameObject = NGUITools.AddChild(listTilesPrefab, tilePrefab);
    Tile tempTile = tempTileGameObject.GetComponent<Tile>();
    tempTile.Init(pos, tileValue, transPos);
    allTiles.Add(tempTile);
    // tempCell.name = i.ToString() + j.ToString();
    // Grid tempGrid = tempCell.GetComponent<Grid>();
    // tempGrid.Init(new Position(i, j), 0);
    // cells[i, j] = tempGrid;
  }
  
  public Tile GetTileAtPos(Position pos) {
    for (int i = 0; i < allTiles.Count; i++) {
      if (allTiles[i].x == pos.x && allTiles[i].y == pos.y) {
        return allTiles[i];
      }
    }
    return null;
  }
  
  public void RemoveTile(Tile tile) {
    allTiles.Remove(tile);
    tile.Remove();
  }
  
  public void ResetAllTilesData() {
    for (int i = 0; i < allTiles.Count; i++) {
      allTiles[i].Reset();
    }
  }
}

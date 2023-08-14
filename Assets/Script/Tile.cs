using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    public Vector2Int TileCordinate { get; set; }
    public TileObject TileObject { get; set; }
    public Dictionary<TileNeighbour, Tile> TileNeighbours { get; private set; }

    private GameObject _tileGameObject;

    private void Awake()
    {
        GridSystem.OnGridCreated += InitializeTileNeighbours;
    }
    public void SetTileObject<T>() where T : TileObject
    {
        if (TileObject != null)
            Destroy(_tileGameObject);

        _tileGameObject = Instantiate(GameManager.Instance.RoadCreationManager.TileGameObjectPrefab, transform.position, Quaternion.identity);
        _tileGameObject.transform.parent = transform;
        TileObject = _tileGameObject.AddComponent(typeof(T)) as TileObject;

        TileObject.Initialize(this,null);
    }
    public void SetTileObjectWithConnectedTiles<T>(Dictionary<TileNeighbour,Tile> connectedTiles) where T : TileObject
    {
        if (TileObject != null)
            Destroy(_tileGameObject);

        _tileGameObject = Instantiate(GameManager.Instance.RoadCreationManager.TileGameObjectPrefab, transform.position, Quaternion.identity);
        _tileGameObject.transform.parent = transform;
        TileObject = _tileGameObject.AddComponent(typeof(T)) as TileObject;

        TileObject.Initialize(this,connectedTiles);
    }
    public void ClearTileObject()
    {
        if (TileObject != null)
            Destroy(_tileGameObject);

        TileObject = null;
    }
    public bool IsExistNeighbourInDictionary(Dictionary<TileNeighbour, Tile> neighbourDictionary, TileNeighbour targetNeighbour)
    {
        if (neighbourDictionary.ContainsKey(targetNeighbour))
        {
            return true;
        }
        return false;
    }
    public void OnDeselect()
    {

    }
    public void OnSelect()
    {

    }
    public bool IsTileObjectExist(out TileObject tileObj)
    {
        tileObj = TileObject;
        return TileObject != null;
    }
    public enum TileNeighbour
    {
        up,down,right,left,upleft,upright,downleft,downright,none
    }
    private void InitializeTileNeighbours()
    {
        GridSystem gridSystem = GameManager.Instance.GridSystem;
        int x = TileCordinate.x;
        int y = TileCordinate.y;
        TileNeighbours = new Dictionary<TileNeighbour, Tile>()
        {
            { TileNeighbour.up, gridSystem.GetTileWithCordinate(x,y + 1) },
            { TileNeighbour.down, gridSystem.GetTileWithCordinate(x,y - 1) },
            { TileNeighbour.right, gridSystem.GetTileWithCordinate(x + 1,y) },
            { TileNeighbour.left, gridSystem.GetTileWithCordinate(x - 1,y) },
            { TileNeighbour.upright, gridSystem.GetTileWithCordinate(x + 1,y + 1) },
            { TileNeighbour.upleft, gridSystem.GetTileWithCordinate(x - 1,y + 1) },
            { TileNeighbour.downright, gridSystem.GetTileWithCordinate(x + 1,y - 1) },
            { TileNeighbour.downleft, gridSystem.GetTileWithCordinate(x - 1,y - 1) },
        };
    }
}

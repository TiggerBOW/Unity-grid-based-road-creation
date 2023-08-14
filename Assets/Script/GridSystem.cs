using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;

public class GridSystem : MonoBehaviour
{
    public UnityEvent<Tile> OnTileWhichMouseOverChanged;
    public static Action OnGridCreated;
    public Tile TileWhichMouseOver
    {
        get
        {
            return _tileWhichMouseOver;
        }
        set
        {
            _tileWhichMouseOver = value;
            OnTileWhichMouseOverChanged?.Invoke(_tileWhichMouseOver);
        }
    }
    private Tile _tileWhichMouseOver;

    [SerializeField] private GameObject _tilePrefab;
    [SerializeField] private int _gridHeight;
    [SerializeField] private int _gridWeight;

    private Dictionary<Vector2Int, Tile> _grid;

    private void Start()
    {
        _grid = new Dictionary<Vector2Int, Tile>();

        CreateGrid();
    }
    private void Update()
    {
        if (TryGetTileWhichMouseOver(out Tile tile))
        {
            TileWhichMouseOver = tile;
        }
    }
    private void CreateGrid()
    {
        int x = _gridWeight;
        int y = _gridHeight;

        for (int i = 0;i < x; i++)
        {
            for (int j = 0;j < y; j++)
            {
                Vector2Int tileCord = new Vector2Int(i, j);
                Tile tile = Instantiate(_tilePrefab, (Vector2)tileCord,Quaternion.identity).GetComponent<Tile>();
                tile.TileCordinate = tileCord;
                _grid.Add(tileCord,tile);
            }
        }
        Debug.Log("aaaaaaa");
        OnGridCreated?.Invoke();
    }
    public Tile GetTileWithCordinate(int xPos, int yPos)
    {
        try
        {
            return _grid[new Vector2Int(xPos, yPos)];
        }
        catch 
        {
            return null;
        }
    }
    public bool TryGetTileWhichMouseOver(out Tile tile)
    {
        Vector2 inputWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        int x = Mathf.RoundToInt(inputWorldPos.x);
        int y = Mathf.RoundToInt(inputWorldPos.y);
        Vector2Int tileCord = new Vector2Int(x, y);

        Tile triedTile = GetTileWithCordinate(tileCord.x,tileCord.y);
        tile = triedTile;
        return triedTile != null;
    }
}

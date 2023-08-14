using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;

[RequireComponent(typeof(GridSystem))]
public class TileSelectionSystem : MonoBehaviour
{
    public Tile SelectedTile { get; private set; }
    public List<Tile> SelectedTiles { get; private set; }
    public static UnityEvent<Tile> OnTileSelected;
    public static event Action OnTileObjectDeleted;

    private GridSystem _gridSystem;
    private void Awake()
    {
        _gridSystem = GetComponent<GridSystem>();
        SelectedTiles = new List<Tile>();
    }
    private void Update()
    {
        SetSelection();
    }
    private void SetSelection()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0)) // Tek tile se�erken
        {
            SelectedTiles.Clear(); // Se�ilenler art�k se�ili de�il.(garanti k�s�m)
            if (_gridSystem.TryGetTileWhichMouseOver(out Tile tile))
            {
                SelectedTile?.OnDeselect(); // Tile de�i�meden �nce �nceki tile i�in deselect fonksiyonu.
                SelectedTile = tile;  
                SelectedTile.OnSelect();
                OnTileSelected?.Invoke(tile);
                
            }
        }
        else if (Input.GetKey(KeyCode.Mouse0)) // �oklu tile se�erken
        {
            _gridSystem.OnTileWhichMouseOverChanged.AddListener((Tile tile) => { // Se�ili tile'yi dinle.
                if (!SelectedTiles.Contains(tile))
                {
                    SelectedTiles.Add(tile); // E�er de�i�en tile se�ilenlerde yoksa bunu ekle.
                }
            });
        }
        else if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            GameManager.Instance.RoadCreationManager.CreateRoad(SelectedTiles);
          
            SelectedTiles.Clear(); // Se�ilenler art�k se�ili de�il.
        }


        if (Input.GetKeyDown(KeyCode.Mouse1)) //Sa� t�kla siler �imdilik
        {
            if (_gridSystem.TryGetTileWhichMouseOver(out Tile tile))
            {
                if (tile.TileObject != null)
                {
                    tile.ClearTileObject();
                    OnTileObjectDeleted?.Invoke();
                }
            }
        }
    }
}

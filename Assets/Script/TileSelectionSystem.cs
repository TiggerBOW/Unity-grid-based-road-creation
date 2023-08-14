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
        if (Input.GetKeyDown(KeyCode.Mouse0)) // Tek tile seçerken
        {
            SelectedTiles.Clear(); // Seçilenler artýk seçili deðil.(garanti kýsým)
            if (_gridSystem.TryGetTileWhichMouseOver(out Tile tile))
            {
                SelectedTile?.OnDeselect(); // Tile deðiþmeden önce önceki tile için deselect fonksiyonu.
                SelectedTile = tile;  
                SelectedTile.OnSelect();
                OnTileSelected?.Invoke(tile);
                
            }
        }
        else if (Input.GetKey(KeyCode.Mouse0)) // Çoklu tile seçerken
        {
            _gridSystem.OnTileWhichMouseOverChanged.AddListener((Tile tile) => { // Seçili tile'yi dinle.
                if (!SelectedTiles.Contains(tile))
                {
                    SelectedTiles.Add(tile); // Eðer deðiþen tile seçilenlerde yoksa bunu ekle.
                }
            });
        }
        else if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            GameManager.Instance.RoadCreationManager.CreateRoad(SelectedTiles);
          
            SelectedTiles.Clear(); // Seçilenler artýk seçili deðil.
        }


        if (Input.GetKeyDown(KeyCode.Mouse1)) //Sað týkla siler þimdilik
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

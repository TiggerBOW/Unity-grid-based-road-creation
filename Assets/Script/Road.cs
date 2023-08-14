using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class Road : TileObject
{
    public Sprite RoadSprite { get; set; }
    public Dictionary<Tile.TileNeighbour,Tile> ConnectedTiles { get; set; }

    public override Tile Tile { get; set; }

    private void Awake()
    {
        ConnectedTiles = new Dictionary<Tile.TileNeighbour, Tile>();
    }
    public override void Initialize(Tile tile,ICollection collection)
    {
        SetSprite(tile,collection); //Sprite ayarlanýr baðlantýlara göre.
        Tile = tile;
        RoadCreationManager.OnRoadChanged += UpdateSprite; // Bir yerde yol deðiþirse yeni baðlantýlar için kendini günceller.
        TileSelectionSystem.OnTileObjectDeleted += UpdateSprite;
    }
    private void OnDestroy() // Yok olduðunda abonelik kalkmalý.
    {
        RoadCreationManager.OnRoadChanged -= UpdateSprite;
        TileSelectionSystem.OnTileObjectDeleted -= UpdateSprite;
    }
    public void SetSprite(Tile tile,ICollection collection)
    {
        ConnectedTiles = (Dictionary<Tile.TileNeighbour, Tile>)collection;
        Debug.Log(tile.TileCordinate + " : " + ConnectedTiles.Count);
        if (ConnectedTiles.Count == 0)
        {
            RoadSprite = GameManager.Instance.RoadCreationManager.GetRoadSpriteWithName("kapalý");
        }
        else if (ConnectedTiles.Count == 1)
        {
            if (tile.IsExistNeighbourInDictionary(ConnectedTiles, Tile.TileNeighbour.up))
            {
                RoadSprite = GameManager.Instance.RoadCreationManager.GetRoadSpriteWithName("düz_üst");
            }
            else if (tile.IsExistNeighbourInDictionary(ConnectedTiles, Tile.TileNeighbour.down))
            {
                RoadSprite = GameManager.Instance.RoadCreationManager.GetRoadSpriteWithName("düz_alt");
            }
            else if (tile.IsExistNeighbourInDictionary(ConnectedTiles, Tile.TileNeighbour.right))
            {
                RoadSprite = GameManager.Instance.RoadCreationManager.GetRoadSpriteWithName("düz_sað");
            }
            else if (tile.IsExistNeighbourInDictionary(ConnectedTiles, Tile.TileNeighbour.left))
            {
                RoadSprite = GameManager.Instance.RoadCreationManager.GetRoadSpriteWithName("düz_sol");
            }
        }
        else if (ConnectedTiles.Count == 2)
        {
            if (tile.IsExistNeighbourInDictionary(ConnectedTiles, Tile.TileNeighbour.down) &&
                tile.IsExistNeighbourInDictionary(ConnectedTiles, Tile.TileNeighbour.right))
            {
                RoadSprite = GameManager.Instance.RoadCreationManager.GetRoadSpriteWithName("dönüþ_üst_sað");
            }
            else if (tile.IsExistNeighbourInDictionary(ConnectedTiles, Tile.TileNeighbour.down) &&
                tile.IsExistNeighbourInDictionary(ConnectedTiles, Tile.TileNeighbour.left))
            {
                RoadSprite = GameManager.Instance.RoadCreationManager.GetRoadSpriteWithName("dönüþ_üst_sol");
            }
            else if (tile.IsExistNeighbourInDictionary(ConnectedTiles, Tile.TileNeighbour.up) &&
               tile.IsExistNeighbourInDictionary(ConnectedTiles, Tile.TileNeighbour.right))
            {
                RoadSprite = GameManager.Instance.RoadCreationManager.GetRoadSpriteWithName("dönüþ_alt_sað");
            }
            else if (tile.IsExistNeighbourInDictionary(ConnectedTiles, Tile.TileNeighbour.up) &&
               tile.IsExistNeighbourInDictionary(ConnectedTiles, Tile.TileNeighbour.left))
            {
                RoadSprite = GameManager.Instance.RoadCreationManager.GetRoadSpriteWithName("dönüþ_alt_sol");
            }
            else if (tile.IsExistNeighbourInDictionary(ConnectedTiles, Tile.TileNeighbour.up) &&
               tile.IsExistNeighbourInDictionary(ConnectedTiles, Tile.TileNeighbour.down))
            {
                RoadSprite = GameManager.Instance.RoadCreationManager.GetRoadSpriteWithName("düz_üst_alt");
            }
            else if (tile.IsExistNeighbourInDictionary(ConnectedTiles, Tile.TileNeighbour.right) &&
               tile.IsExistNeighbourInDictionary(ConnectedTiles, Tile.TileNeighbour.left))
            {
                RoadSprite = GameManager.Instance.RoadCreationManager.GetRoadSpriteWithName("düz_sað_sol");
            }
        }
        else if (ConnectedTiles.Count == 3)
        {
            if (tile.IsExistNeighbourInDictionary(ConnectedTiles, Tile.TileNeighbour.up) &&
                tile.IsExistNeighbourInDictionary(ConnectedTiles, Tile.TileNeighbour.right) &&
                tile.IsExistNeighbourInDictionary(ConnectedTiles, Tile.TileNeighbour.left))
            {
                RoadSprite = GameManager.Instance.RoadCreationManager.GetRoadSpriteWithName("kavþak_üst_sað_sol");
            }
            if (tile.IsExistNeighbourInDictionary(ConnectedTiles, Tile.TileNeighbour.down) &&
                tile.IsExistNeighbourInDictionary(ConnectedTiles, Tile.TileNeighbour.right) &&
                tile.IsExistNeighbourInDictionary(ConnectedTiles, Tile.TileNeighbour.left))
            {
                RoadSprite = GameManager.Instance.RoadCreationManager.GetRoadSpriteWithName("kavþak_alt_sað_sol");
            }
            if (tile.IsExistNeighbourInDictionary(ConnectedTiles, Tile.TileNeighbour.up) &&
                tile.IsExistNeighbourInDictionary(ConnectedTiles, Tile.TileNeighbour.down) &&
                tile.IsExistNeighbourInDictionary(ConnectedTiles, Tile.TileNeighbour.left))
            {
                RoadSprite = GameManager.Instance.RoadCreationManager.GetRoadSpriteWithName("kavþak_üst_alt_sol");
            }
            if (tile.IsExistNeighbourInDictionary(ConnectedTiles, Tile.TileNeighbour.up) &&
                tile.IsExistNeighbourInDictionary(ConnectedTiles, Tile.TileNeighbour.down) &&
                tile.IsExistNeighbourInDictionary(ConnectedTiles, Tile.TileNeighbour.right))
            {
                RoadSprite = GameManager.Instance.RoadCreationManager.GetRoadSpriteWithName("kavþak_üst_alt_sað");
            }
        }
        else if (ConnectedTiles.Count == 4)
        {
            RoadSprite = GameManager.Instance.RoadCreationManager.GetRoadSpriteWithName("kavþak");
        }

        if (TryGetComponent<SpriteRenderer>(out SpriteRenderer spi))
        {
            spi.sprite = RoadSprite;
        }
    }
    private void UpdateSprite()
    {
        RoadCreationManager rcm = GameManager.Instance.RoadCreationManager;

        Dictionary<Tile.TileNeighbour, Tile> connectedTiles = rcm.GetConnectedTileWith(Tile);

        SetSprite(Tile, connectedTiles);
    }
}

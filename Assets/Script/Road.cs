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
        SetSprite(tile,collection); //Sprite ayarlan�r ba�lant�lara g�re.
        Tile = tile;
        RoadCreationManager.OnRoadChanged += UpdateSprite; // Bir yerde yol de�i�irse yeni ba�lant�lar i�in kendini g�nceller.
        TileSelectionSystem.OnTileObjectDeleted += UpdateSprite;
    }
    private void OnDestroy() // Yok oldu�unda abonelik kalkmal�.
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
            RoadSprite = GameManager.Instance.RoadCreationManager.GetRoadSpriteWithName("kapal�");
        }
        else if (ConnectedTiles.Count == 1)
        {
            if (tile.IsExistNeighbourInDictionary(ConnectedTiles, Tile.TileNeighbour.up))
            {
                RoadSprite = GameManager.Instance.RoadCreationManager.GetRoadSpriteWithName("d�z_�st");
            }
            else if (tile.IsExistNeighbourInDictionary(ConnectedTiles, Tile.TileNeighbour.down))
            {
                RoadSprite = GameManager.Instance.RoadCreationManager.GetRoadSpriteWithName("d�z_alt");
            }
            else if (tile.IsExistNeighbourInDictionary(ConnectedTiles, Tile.TileNeighbour.right))
            {
                RoadSprite = GameManager.Instance.RoadCreationManager.GetRoadSpriteWithName("d�z_sa�");
            }
            else if (tile.IsExistNeighbourInDictionary(ConnectedTiles, Tile.TileNeighbour.left))
            {
                RoadSprite = GameManager.Instance.RoadCreationManager.GetRoadSpriteWithName("d�z_sol");
            }
        }
        else if (ConnectedTiles.Count == 2)
        {
            if (tile.IsExistNeighbourInDictionary(ConnectedTiles, Tile.TileNeighbour.down) &&
                tile.IsExistNeighbourInDictionary(ConnectedTiles, Tile.TileNeighbour.right))
            {
                RoadSprite = GameManager.Instance.RoadCreationManager.GetRoadSpriteWithName("d�n��_�st_sa�");
            }
            else if (tile.IsExistNeighbourInDictionary(ConnectedTiles, Tile.TileNeighbour.down) &&
                tile.IsExistNeighbourInDictionary(ConnectedTiles, Tile.TileNeighbour.left))
            {
                RoadSprite = GameManager.Instance.RoadCreationManager.GetRoadSpriteWithName("d�n��_�st_sol");
            }
            else if (tile.IsExistNeighbourInDictionary(ConnectedTiles, Tile.TileNeighbour.up) &&
               tile.IsExistNeighbourInDictionary(ConnectedTiles, Tile.TileNeighbour.right))
            {
                RoadSprite = GameManager.Instance.RoadCreationManager.GetRoadSpriteWithName("d�n��_alt_sa�");
            }
            else if (tile.IsExistNeighbourInDictionary(ConnectedTiles, Tile.TileNeighbour.up) &&
               tile.IsExistNeighbourInDictionary(ConnectedTiles, Tile.TileNeighbour.left))
            {
                RoadSprite = GameManager.Instance.RoadCreationManager.GetRoadSpriteWithName("d�n��_alt_sol");
            }
            else if (tile.IsExistNeighbourInDictionary(ConnectedTiles, Tile.TileNeighbour.up) &&
               tile.IsExistNeighbourInDictionary(ConnectedTiles, Tile.TileNeighbour.down))
            {
                RoadSprite = GameManager.Instance.RoadCreationManager.GetRoadSpriteWithName("d�z_�st_alt");
            }
            else if (tile.IsExistNeighbourInDictionary(ConnectedTiles, Tile.TileNeighbour.right) &&
               tile.IsExistNeighbourInDictionary(ConnectedTiles, Tile.TileNeighbour.left))
            {
                RoadSprite = GameManager.Instance.RoadCreationManager.GetRoadSpriteWithName("d�z_sa�_sol");
            }
        }
        else if (ConnectedTiles.Count == 3)
        {
            if (tile.IsExistNeighbourInDictionary(ConnectedTiles, Tile.TileNeighbour.up) &&
                tile.IsExistNeighbourInDictionary(ConnectedTiles, Tile.TileNeighbour.right) &&
                tile.IsExistNeighbourInDictionary(ConnectedTiles, Tile.TileNeighbour.left))
            {
                RoadSprite = GameManager.Instance.RoadCreationManager.GetRoadSpriteWithName("kav�ak_�st_sa�_sol");
            }
            if (tile.IsExistNeighbourInDictionary(ConnectedTiles, Tile.TileNeighbour.down) &&
                tile.IsExistNeighbourInDictionary(ConnectedTiles, Tile.TileNeighbour.right) &&
                tile.IsExistNeighbourInDictionary(ConnectedTiles, Tile.TileNeighbour.left))
            {
                RoadSprite = GameManager.Instance.RoadCreationManager.GetRoadSpriteWithName("kav�ak_alt_sa�_sol");
            }
            if (tile.IsExistNeighbourInDictionary(ConnectedTiles, Tile.TileNeighbour.up) &&
                tile.IsExistNeighbourInDictionary(ConnectedTiles, Tile.TileNeighbour.down) &&
                tile.IsExistNeighbourInDictionary(ConnectedTiles, Tile.TileNeighbour.left))
            {
                RoadSprite = GameManager.Instance.RoadCreationManager.GetRoadSpriteWithName("kav�ak_�st_alt_sol");
            }
            if (tile.IsExistNeighbourInDictionary(ConnectedTiles, Tile.TileNeighbour.up) &&
                tile.IsExistNeighbourInDictionary(ConnectedTiles, Tile.TileNeighbour.down) &&
                tile.IsExistNeighbourInDictionary(ConnectedTiles, Tile.TileNeighbour.right))
            {
                RoadSprite = GameManager.Instance.RoadCreationManager.GetRoadSpriteWithName("kav�ak_�st_alt_sa�");
            }
        }
        else if (ConnectedTiles.Count == 4)
        {
            RoadSprite = GameManager.Instance.RoadCreationManager.GetRoadSpriteWithName("kav�ak");
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

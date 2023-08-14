using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class RoadCreationManager : MonoBehaviour
{
    [System.Serializable]
    public class RoadSprite
    {
        public string roadName;
        public Sprite roadSprite;
    }

    public static event Action OnRoadChanged;

    public List<RoadSprite> RoadSprites = new List<RoadSprite>();
    public GameObject TileGameObjectPrefab;

    public Sprite GetRoadSpriteWithName(string name)
    {
        for (int i = 0;i< RoadSprites.Count; i++)
        {
            if (name == RoadSprites[i].roadName)
            {
                return RoadSprites[i].roadSprite;
            }
        }
        Debug.LogError("Yol sprite'ý bulunamadý");
        return null;
    }
    // Seçili tile'lar için yol oluþturan metod
    public void CreateRoad(List<Tile> selectedTiles)
    {
        foreach (Tile selectedTile in selectedTiles)
        {
            Dictionary<Tile.TileNeighbour, Tile> connectedTiles = new Dictionary<Tile.TileNeighbour, Tile>();

            // Doðrudan eklenebilecek komþularý bul
            foreach (Tile.TileNeighbour neighbour in selectedTile.TileNeighbours.Keys)
            {
                if (CanAddDirectNeighbour(neighbour, selectedTile, selectedTiles, connectedTiles))
                {
                    connectedTiles.Add(neighbour, selectedTile.TileNeighbours[neighbour]);
                }
            }

            // Komþu tile'larda zaten yol var mý kontrolü yap
            AddExistingRoadNeighbours(selectedTile, connectedTiles);

            selectedTile.SetTileObjectWithConnectedTiles<Road>(connectedTiles);
        }
        OnRoadChanged?.Invoke();
    }
    public void AddExistingRoadNeighbours(Tile selectedTile, Dictionary<Tile.TileNeighbour, Tile> connectedTiles)
    {
        foreach (Tile.TileNeighbour neighbour in selectedTile.TileNeighbours.Keys)
        {
            Tile neighbourTile = selectedTile.TileNeighbours[neighbour];
            if (IsNeighbourRoad(neighbourTile, neighbour) && !connectedTiles.ContainsKey(neighbour))
            {
                connectedTiles.Add(neighbour, neighbourTile);
            }
        }
    }
    public Dictionary<Tile.TileNeighbour, Tile> GetConnectedTileWith(Tile tile) //Verilen tile ile baðlantýlý komþu yollarý döner.
    {
        Dictionary<Tile.TileNeighbour, Tile> connectedTiles = new Dictionary<Tile.TileNeighbour, Tile>();
        foreach (Tile.TileNeighbour neighbour in tile.TileNeighbours.Keys)
        {
            Tile neighbourTile = tile.TileNeighbours[neighbour];
            if (IsNeighbourRoad(neighbourTile, neighbour) && !connectedTiles.ContainsKey(neighbour))
            {
                connectedTiles.Add(neighbour, neighbourTile);
            }
        }
        return connectedTiles;
    }

    // Doðrudan komþu eklenmeli mi kontrolü yapan metod
    public bool CanAddDirectNeighbour(Tile.TileNeighbour neighbour, Tile selectedTile, List<Tile> selectedTiles, Dictionary<Tile.TileNeighbour, Tile> connectedTiles)
    {
        Tile neighbourTile = selectedTile.TileNeighbours[neighbour];
        return neighbourTile != null &&
               (selectedTiles.Contains(neighbourTile)
               && !connectedTiles.ContainsKey(neighbour)) &&
               IsStraightNeighbour(neighbour);
    }

    // Komþu bir yol mu kontrolü yapan metod
    public bool IsNeighbourRoad(Tile tile, Tile.TileNeighbour direction)
    {
        return tile != null && tile.TileObject is Road && IsStraightNeighbour(direction);
    }

    // Doðrudan komþu mu kontrolü yapan metod
    public bool IsStraightNeighbour(Tile.TileNeighbour direction)
    {
        return direction == Tile.TileNeighbour.up ||
               direction == Tile.TileNeighbour.down ||
               direction == Tile.TileNeighbour.right ||
               direction == Tile.TileNeighbour.left;
    }
}



using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class TileObject : MonoBehaviour
{
    public abstract void Initialize(Tile tile,ICollection data);
    public abstract Tile Tile { get; set; }
}

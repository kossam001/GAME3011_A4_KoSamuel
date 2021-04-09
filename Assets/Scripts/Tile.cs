using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TileType
{
    None,
    Straight,
    Angle,
    T,
    Quad,
}

public class Tile : MonoBehaviour
{
    private TileType type;
    private float tileAngle;

    public void Awake()
    {
        type = TileType.None;
        tileAngle = 0;
    }
}

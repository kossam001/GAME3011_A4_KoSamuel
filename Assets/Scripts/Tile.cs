using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
    public TileType type { get; private set; }
    private float tileAngle;

    [HideInInspector] public bool isSet;

    public void Awake()
    {
        type = TileType.None;
        tileAngle = 0;
    }

    public void Set(Sprite sprite, TileType tileType)
    {
        GetComponent<Image>().sprite = sprite;
        type = tileType;
    }

    public Sprite GetSprite()
    {
        return GetComponent<Image>().sprite;
    }
}

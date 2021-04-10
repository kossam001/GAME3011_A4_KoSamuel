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

    public TileData data;

    public Vector3[] connectionDirections;

    public void Awake()
    {
        type = TileType.None;
        tileAngle = 0;
    }

    public void Set(Sprite sprite, TileType tileType, Vector3[] connections)
    {
        GetComponent<Image>().sprite = sprite;
        type = tileType;
        connectionDirections = connections;

        isSet = true;
    }

    public Sprite GetSprite()
    {
        return GetComponent<Image>().sprite;
    }

    private void Update()
    {
        foreach (Vector3 direction in connectionDirections)
        {
            RectTransform rect = GetComponent<RectTransform>();
            Vector3 parentScale = transform.parent.localScale;
            Vector3 worldScale = transform.parent.parent.localScale;

            Vector2 origin = new Vector2(rect.position.x + parentScale.x * worldScale.x * rect.sizeDelta.x * 0.5f, 
                                         rect.position.y + parentScale.y * worldScale.y * rect.sizeDelta.y * 0.5f);

            Debug.DrawRay(origin, direction * rect.sizeDelta.x * worldScale.x * parentScale.x, Color.green);
        }
    }
}

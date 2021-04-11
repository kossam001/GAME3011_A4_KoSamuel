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
    Node,
}

public class Tile : MonoBehaviour
{
    public TileType type { get; private set; }
    private float tileAngle;

    public bool isSet;
    public bool isActivated;

    public TileData data;

    public Vector3[] connectionDirections;

    public void Awake()
    {
        type = TileType.None;
        tileAngle = 0;
    }

    public void Set(Sprite sprite, TileType tileType, Vector3[] connections, Quaternion rotation)
    {
        GetComponent<Image>().sprite = sprite;
        type = tileType;
        connectionDirections = connections;
        transform.rotation = rotation;

        isSet = true;
    }

    public Sprite GetSprite()
    {
        return GetComponent<Image>().sprite;
    }

    public void ActivateTile()
    {
        GetComponent<Image>().color = Color.green;
        isActivated = true;
    }

    public void CheckConnection()
    {
        foreach (Vector3 direction in connectionDirections)
        {
            RectTransform rect = GetComponent<RectTransform>();
            Vector3 parentScale = transform.parent.localScale;
            Vector3 worldScale = transform.parent.parent.localScale;

            Vector2 origin = new Vector2(rect.position.x, rect.position.y);

            RaycastHit2D[] hits = Physics2D.RaycastAll(origin, transform.rotation * direction, rect.sizeDelta.x * worldScale.x * parentScale.x);

            foreach (RaycastHit2D hit in hits)
            {
                GameObject hitObject = hit.collider.gameObject;

                if (!ReferenceEquals(hitObject, gameObject) && hitObject.CompareTag("Tile") && hitObject.GetComponent<Tile>().isSet)
                {
                    Tile tile = hitObject.GetComponent<Tile>();

                    // Adjacent tile is activated
                    if (tile.isActivated && !isActivated)
                    {
                        ActivateTile();
                        CheckConnection();
                    }
                    else if (!tile.isActivated && isActivated)
                    {
                        tile.ActivateTile();
                        tile.CheckConnection();
                    }
                }
            }
        }
    }

    private void Update()
    {
        foreach (Vector3 direction in connectionDirections)
        {
            RectTransform rect = GetComponent<RectTransform>();
            Vector3 parentScale = transform.parent.localScale;
            Vector3 worldScale = transform.parent.parent.localScale;

            Vector2 origin = new Vector2(rect.position.x, rect.position.y);

            Debug.DrawRay(origin, transform.rotation * direction * rect.sizeDelta.x * worldScale.x * parentScale.x, Color.green);
        }
    }
}

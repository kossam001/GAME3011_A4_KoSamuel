using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class Game : MonoBehaviour
{
    private static Game instance;
    public static Game Instance { get { return instance; } }

    public List<Sprite> tileSprites;
    public Dictionary<TileType, Sprite> typeToSprite = new Dictionary<TileType, Sprite>();
    public Dictionary<TileType, Vector3[]> typeToConnections = new Dictionary<TileType, Vector3[]>();

    [Header("Tile Selection")]
    public Image selectionImage;
    [HideInInspector] public Sprite selectionSprite;
    [HideInInspector] public TileType selectionType;
    [HideInInspector] public Quaternion selectionAngle;

    public Tile cursorTile;

    private void Awake()
    {
        if (instance != null && instance != this)
            Destroy(this.gameObject);

        else
            instance = this;

        TileType[] tileTypes = (TileType[]) Enum.GetValues(typeof(TileType));

        for (int i = 0; i < tileSprites.Count; i++)
        {
            typeToSprite[tileTypes[i]] = tileSprites[i];
        }

        InitializeConnections();
    }

    private void InitializeConnections()
    {
        typeToConnections[TileType.Quad] = new Vector3[] { new Vector3(1, 0, 0), new Vector3(-1, 0, 0), new Vector3(0, 1, 0), new Vector3(0, -1, 0) };
        typeToConnections[TileType.Node] = new Vector3[] { new Vector3(1, 0, 0), new Vector3(-1, 0, 0), new Vector3(0, 1, 0), new Vector3(0, -1, 0) };
        typeToConnections[TileType.Angle] = new Vector3[] {new Vector3(-1, 0, 0), new Vector3(0, -1, 0) };
        typeToConnections[TileType.Straight] = new Vector3[] {new Vector3(0, 1, 0), new Vector3(0, -1, 0) };
        typeToConnections[TileType.T] = new Vector3[] { new Vector3(1, 0, 0), new Vector3(-1, 0, 0), new Vector3(0, -1, 0) };
    }

    private void Start()
    {
        selectionType = (TileType)UnityEngine.Random.Range(1, Game.Instance.typeToSprite.Count - 1);
        selectionSprite = Game.Instance.typeToSprite[selectionType];
        selectionImage.sprite = selectionSprite;
        selectionAngle = Quaternion.Euler(0, 0, 90 * UnityEngine.Random.Range(0, 3));

        SelectRandomTile();
    }

    public void SelectRandomTile()
    {
        cursorTile.Set(selectionSprite, selectionType, typeToConnections[selectionType], selectionAngle);

        // Last one is a fixed node
        selectionType = (TileType)UnityEngine.Random.Range(1, Game.Instance.typeToSprite.Count-1);
        selectionSprite = Game.Instance.typeToSprite[selectionType];
        selectionImage.sprite = selectionSprite;
        selectionAngle = Quaternion.Euler(0, 0, 90 * UnityEngine.Random.Range(0, 3));

        selectionImage.gameObject.transform.rotation = selectionAngle;
    }

    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void SetEasy()
    {

    }

    public void SetMedium()
    {

    }

    public void SetHard()
    {

    }
}

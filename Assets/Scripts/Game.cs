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
        typeToConnections[TileType.Angle] = new Vector3[] {new Vector3(-1, 0, 0), new Vector3(0, -1, 0) };
        typeToConnections[TileType.Straight] = new Vector3[] {new Vector3(0, 1, 0), new Vector3(0, -1, 0) };
        typeToConnections[TileType.T] = new Vector3[] { new Vector3(1, 0, 0), new Vector3(-1, 0, 0), new Vector3(0, -1, 0) };
    }

    private void Start()
    {
        SelectRandomTile();
    }

    public void SelectRandomTile()
    {
        selectionType = (TileType)UnityEngine.Random.Range(1, Game.Instance.typeToSprite.Count);
        selectionSprite = Game.Instance.typeToSprite[selectionType];
        selectionImage.sprite = selectionSprite;

        cursorTile.GetComponent<Tile>().Set(selectionSprite, selectionType, typeToConnections[selectionType]);
    }

    private void Update()
    {

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

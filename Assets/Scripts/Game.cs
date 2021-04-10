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

        cursorTile.GetComponent<Tile>().Set(selectionSprite, selectionType);
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

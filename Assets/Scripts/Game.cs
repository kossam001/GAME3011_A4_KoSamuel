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
    public Dictionary<TileType, List<Vector3>> typeToConnections = new Dictionary<TileType, List<Vector3>>();

    [Header("Tile Selection")]
    public List<Tile> selectionTiles;

    public Board gameBoard;
    public GameObject startingPanel;

    public int numNodes = 2;
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
        typeToConnections[TileType.Quad] = new List<Vector3>(new Vector3[] { new Vector3(1, 0, 0), new Vector3(-1, 0, 0), new Vector3(0, 1, 0), new Vector3(0, -1, 0) });
        typeToConnections[TileType.Node] = new List<Vector3>(new Vector3[] { new Vector3(1, 0, 0), new Vector3(-1, 0, 0), new Vector3(0, 1, 0), new Vector3(0, -1, 0) });
        typeToConnections[TileType.Angle] = new List<Vector3>(new Vector3[] {new Vector3(-1, 0, 0), new Vector3(0, -1, 0) });
        typeToConnections[TileType.Straight] = new List<Vector3>(new Vector3[] {new Vector3(0, 1, 0), new Vector3(0, -1, 0) });
        typeToConnections[TileType.T] = new List<Vector3>(new Vector3[] { new Vector3(1, 0, 0), new Vector3(-1, 0, 0), new Vector3(0, -1, 0) });
    }

    public void SelectRandomTile()
    {
        cursorTile.Copy(selectionTiles[0]);

        TileType selectionType = (TileType)UnityEngine.Random.Range(1, Game.Instance.typeToSprite.Count - 1);
        Sprite selectionSprite = Game.Instance.typeToSprite[selectionType];

        for (int i = 0; i < selectionTiles.Count-1; i++)
        {
            selectionTiles[i].Copy(selectionTiles[i + 1]);
        }

        // Randomize the last panel
        Quaternion randomRotation = Quaternion.Euler(0, 0, 90 * UnityEngine.Random.Range(0, 3));
        List<Vector3> rotatedDirections = new List<Vector3>();
        foreach (Vector3 directions in typeToConnections[selectionType])
            rotatedDirections.Add(randomRotation * directions);

        selectionTiles[selectionTiles.Count - 1].Set(selectionSprite, selectionType, rotatedDirections, randomRotation);
    }

    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void SetDifficulty(int difficultyLevel)
    {
        switch (difficultyLevel)
        {
            case 0:
                numNodes = 2;
                break;
            case 1:
                numNodes = 3;
                break;
            case 2:
                numNodes = 4;
                break;
        }
    }

    public void SetPlayerSkill(int playerLevel)
    {
        switch (playerLevel)
        {
            case 0:
                for (int i = selectionTiles.Count - 1; i > 0; i--)
                {
                    selectionTiles[i].gameObject.SetActive(false);
                    selectionTiles.RemoveAt(selectionTiles.Count - i);
                }
                break;
            case 1:
                selectionTiles[selectionTiles.Count - 1].gameObject.SetActive(false);
                selectionTiles.RemoveAt(selectionTiles.Count - 1);
                break;
            case 2:
                break;
        }
    }

    public void StartGame()
    {
        for (int i = 0; i < selectionTiles.Count; i++)
        {
            TileType selectionType = (TileType)UnityEngine.Random.Range(1, Game.Instance.typeToSprite.Count - 1);
            Sprite selectionSprite = Game.Instance.typeToSprite[selectionType];

            Quaternion randomRotation = Quaternion.Euler(0, 0, 90 * UnityEngine.Random.Range(0, 3));
            List<Vector3> rotatedDirections = new List<Vector3>();
            foreach (Vector3 directions in typeToConnections[selectionType])
                rotatedDirections.Add(randomRotation * directions);

            selectionTiles[i].Set(selectionSprite, selectionType, rotatedDirections, randomRotation);
        }

        SelectRandomTile();

        startingPanel.SetActive(false);
        gameBoard.InitializeBoard();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Board : MonoBehaviour
{
    private static Board instance;
    public static Board Instance { get { return instance; } }

    public int rows;
    public int columns;

    public GameObject tilePrefab;
    public Transform boardTransform;
    public Tile[,] board;

    private void Awake()
    {
        if (instance != null && instance != this)
            Destroy(this.gameObject);

        else
            instance = this;

        board = new Tile[columns, rows];

        RectTransform boardRect = boardTransform.gameObject.GetComponent<RectTransform>();
        RectTransform tileRect = tilePrefab.gameObject.GetComponent<RectTransform>();

        boardRect.sizeDelta = new Vector2(tileRect.rect.width * columns, tileRect.rect.height * rows);

        Rect viewportDimension = GetComponent<RectTransform>().rect;

        float boardScale = viewportDimension.height / boardRect.rect.height;

        boardRect.localScale = new Vector3(boardScale, boardScale, boardScale);

        for (int y = 0; y < rows; y++)
        {
            for (int x = 0; x < columns; x++)
            {
                board[x, y] = Instantiate(tilePrefab).GetComponent<Tile>();
                board[x, y].transform.SetParent(boardTransform);

                RectTransform rectTransform = board[x, y].GetComponent<RectTransform>();
                rectTransform.localPosition = new Vector2(x * rectTransform.rect.width - boardRect.rect.width * 0.5f,
                                                          y * rectTransform.rect.height - boardRect.rect.height * 0.5f);
                rectTransform.localScale = new Vector3(1, 1, 1);
            }
        }
    }


}

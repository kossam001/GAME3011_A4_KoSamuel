using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TileSelection : MonoBehaviour
{
    public Image selectionImage;
    [HideInInspector] public Sprite selectionSprite;
    [HideInInspector] public TileType selectionType;

    public Image cursorImage;

    // Start is called before the first frame update
    void Start()
    {
        selectionSprite = Game.Instance.typeToSprite[(TileType)Random.Range(1, Game.Instance.typeToSprite.Count)];
        selectionImage.sprite = selectionSprite;

        cursorImage.sprite = selectionSprite;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

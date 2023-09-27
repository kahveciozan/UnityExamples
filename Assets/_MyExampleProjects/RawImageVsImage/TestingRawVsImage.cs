using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TestingRawVsImage : MonoBehaviour
{
    [Space]
    [Header("Game Objects")]
    [SerializeField] GameObject imageObj;
    [SerializeField] GameObject rawImageObj;

    [Space]
    [Header("Images")]
    [SerializeField] private Texture2D texture2D;
    [SerializeField] private Sprite sprite;

    private void Awake()
    {
        // Assign a Texture to Image
        Sprite newSprite =  Sprite.Create(texture2D, new Rect(0, 0, texture2D.width, texture2D.height), new Vector2(0.5f, 0.5f));
        imageObj.GetComponent<Image>().sprite = newSprite;


        // Assign a Sprite to Raw Image
        rawImageObj.GetComponent<RawImage>().texture = sprite.texture;
    }
}

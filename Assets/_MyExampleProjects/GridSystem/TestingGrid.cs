using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeMonkey.Utils;

public class TestingGrid : MonoBehaviour
{
    private Grid grid;
    [SerializeField] Texture2D texture;

    private void Start()
    {

        int tempX = 40, tempY = 20;
        int cellS = 4;

        grid = new Grid(tempX, tempY, cellS, new Vector3(-tempX * cellS / 2, -tempY * cellS / 2), TextureColor(texture));

    }


    private void Update()
    {
        if (Input.GetMouseButton(0))
        {
            grid.SetValue(UtilsClass.GetMouseWorldPosition(), 69);
        }
        if (Input.GetMouseButton(1))
        {
            Debug.Log(grid.GetValue(UtilsClass.GetMouseWorldPosition()));
        }
    }


    private Color[,] TextureColor(Texture2D texture)
    {

        int width = texture.width;
        int height = texture.height;

        Color[,] pixelColors = new Color[width, height];

        for (int x =0; x< width; x++)
        {
            for (int y = 0; y< height; y++)
            {
                pixelColors[x,y] = texture.GetPixel(x, y);
            }
        }

        return pixelColors;
    }

}

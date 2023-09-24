using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeMonkey.Utils;
using Unity.VisualScripting.Antlr3.Runtime;
using Unity.VisualScripting;

public class Grid
{
    private int width;
    private int height;
    private float cellSize;
    private Vector3 originPosition;
    private int[,] gridArray;
    private TextMesh[,] debugTextArray;

    public Grid(int width,int height, int cellSize, Vector3 originPosition, Color[,] textureColor)
    {
        this.width = width;
        this.height = height;
        this.cellSize = cellSize;
        this.originPosition = originPosition;

        gridArray = new int[width,height];
        debugTextArray = new TextMesh[width,height];

        for(int x =0; x < gridArray.GetLength(0); x++)
        {
            for(int y =0; y < gridArray.GetLength(1); y++)
            {
                debugTextArray[x,y] =  UtilsClass.CreateWorldText(gridArray[x,y].ToString(), null, GetWorldPosition(x,y) + new Vector3(cellSize,cellSize) * 0.5f , 30 , Color.white, TextAnchor.MiddleCenter);
                debugTextArray[x, y].color = textureColor[x, y];
                CreateNewTexture(cellSize, textureColor[x,y], GetWorldPosition(x, y) + new Vector3(cellSize, cellSize) * 0.5f);
                Debug.DrawLine(GetWorldPosition(x,y), GetWorldPosition(x, y+1),Color.white ,100f ) ;
                Debug.DrawLine(GetWorldPosition(x,y), GetWorldPosition(x+1, y), Color.white, 100f );
            }
        }
        Debug.DrawLine(GetWorldPosition(0, height), GetWorldPosition(width, height), Color.white, 100f);
        Debug.DrawLine(GetWorldPosition(width, 0), GetWorldPosition(width, height), Color.white, 100f);

        SetValue(2, 1, 56);
    }

    public int GetWidth()
    {
        return width;
    }

    public int GetHeight()
    {
        return height;
    }

    private Vector3 GetWorldPosition(int x, int y)
    {
            return new Vector3(x, y) * cellSize + originPosition; 
    }

    private void GetXY(Vector3 worldPosition, out int x , out int y)
    {
        x = Mathf.FloorToInt((worldPosition - originPosition).x / cellSize);
        y = Mathf.FloorToInt((worldPosition - originPosition).y / cellSize);
    }

    private void SetValue(int x, int y, int value)
    {
        if (x >= 0 && y >= 0 && x < width && y < height)
        {
            gridArray[x, y] = value;
            debugTextArray[x, y].text = gridArray[x, y].ToString();
        }
    }

    public void SetValue(Vector3 worldPosition, int value)
    {
        int x, y;
        GetXY(worldPosition, out x, out y);
        SetValue(x, y, value);
    }

    private int GetValue(int x, int y)
    {
        if (x >= 0 && y >= 0 && x < width && y < height)
        {
            return gridArray[x, y];
        }
        else
        {
            return 0;
        }
    }

    public int GetValue(Vector3 worldPosition)
    {
        int x, y;
        GetXY(worldPosition, out x, out y);
        return GetValue(x, y);
    }

    private void CreateNewTexture(int cellSize, Color color, Vector3 worldPosition)
    {



        GameObject gameObject = new GameObject("SubSprite", typeof(SpriteRenderer));
        Transform transform = gameObject.transform;
        transform.localPosition = worldPosition;
        
        SpriteRenderer spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        Texture2D newTexture = new Texture2D(cellSize*100, cellSize*100);
        Sprite newSprite = Sprite.Create(newTexture, new Rect(0, 0, newTexture.width, newTexture.height), Vector2.one * 0.5f);
        spriteRenderer.sprite = newSprite;

        spriteRenderer.color = color;
    }


}

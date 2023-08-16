using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Noise : MonoBehaviour
{
    #region Example 1
    /*
    private MeshRenderer m_Renderer;
    private Texture2D m_Texture;

    public float scale = 0.1f;
    public float xoff = 0;
    public float yoff = 0;
    private float _xoff = 0;
    private float _yoff = 0;
    public float step = 0.005f;


    void Start()
    {
        m_Renderer = GetComponent<MeshRenderer>();
        m_Texture = new Texture2D(128, 128);
        m_Renderer.material.mainTexture = m_Texture;
        //RandomTextureGenerator();
       
    }

    private void RandomTextureGenerator()
    {
        for (int y = 0; y < m_Texture.height; y++)
        {
            for (int x = 0; x < m_Texture.width; x++)
            {
                Color color = Random.Range(0.0f, 1.0f) > 0.5f ? Color.black : Color.white;              // Tamamen rastgele sayilar urettik
                m_Texture.SetPixel(x, y, color);
            }

        }
        m_Texture.Apply();
    }

    private void PerlinNoiseTextureGenerator()
    {
        for(int y = 0; y < m_Texture.height; y++)
        {
            for(int x = 0; x< m_Texture.width; x++)
            {
                Color color = Mathf.PerlinNoise((x + xoff)* scale, (y + yoff)* scale) > 0.5f ? Color.black : Color.white;                // Perlin Noise ile doðal rastgele sayilar urettik
                m_Texture.SetPixel(x, y, color);

                Debug.Log("x=" + x + ", y=" + y + " için Perlin noise deðeri " + Mathf.PerlinNoise(x, y));
            }
        }
        m_Texture.Apply();
    }

    // Update is called once per frame
    void Update()
    {
        _xoff = _xoff + Input.GetAxis("Horizontal");
        _yoff = _yoff + Input.GetAxis("Vertical");

        if (Input.mouseScrollDelta.y != 0.0f)
            scale += Input.mouseScrollDelta.y > 0 ? step : -step;

        PerlinNoiseTextureGenerator();
    }
    */
    #endregion

    #region Example 2
    private MeshRenderer _meshRenderer;
    private Texture2D _texture;
    private bool _mode = false;
    public float step = 0.005f;
    public float scale = 5f;
    private float _xoff = 0;
    private float _yoff = 0;
    void Start()
    {
        _meshRenderer = gameObject.GetComponent<MeshRenderer>();
        _texture = new Texture2D(128, 128);
        _meshRenderer.material.mainTexture = _texture;
        RandomTextureGenerator();
    }
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RandomTextureGenerator();
            _mode = false;
        }
        else if (Input.GetMouseButtonDown(1))
        {
            _mode = true;
        }

        _xoff = _xoff + Input.GetAxis("Horizontal");
        _yoff = _yoff + Input.GetAxis("Vertical");

        if (_mode)
        {
            if (Input.mouseScrollDelta.y != 0.0f)
                scale += Input.mouseScrollDelta.y > 0 ? step : -step;
            PerlinNoiseTextureGenerator();
        }
    }

    void RandomTextureGenerator()
    {
        for (int y = 0; y < _texture.height; y++)
        {
            for (int x = 0; x < _texture.width; x++)
            {
                Color color = Random.Range(0.0f, 1.0f) > 0.5f ? Color.black : Color.white;
                _texture.SetPixel(x, y, color);
            }
        }
        _texture.Apply();
    }

    void PerlinNoiseTextureGenerator()
    {
        for (int y = 0; y < _texture.height; y++)
        {
            for (int x = 0; x < _texture.width; x++)
            {
                Color color = Mathf.PerlinNoise((x + _xoff) * scale, (y + _yoff) * scale) > 0.5f ? Color.black : Color.white;
                _texture.SetPixel(x, y, color);
            }
        }
        _texture.Apply();
    }
    #endregion
}

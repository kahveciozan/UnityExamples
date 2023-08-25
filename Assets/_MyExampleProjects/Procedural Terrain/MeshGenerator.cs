using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
public class MeshGenerator : MonoBehaviour
{
    Mesh mesh;
    Vector3[] vertices;
    int[] triangles;
    Color[] colors;

    public int xSize = 20;
    public int zSize = 20;

    /*
    public int textureWidth = 1024;
    public int textureHeight = 1024;

    public float noise01Scale = 2f;
    public float noise01Amp = 2f;

    public float noise02Scale = 4f;
    public float noise02Amp = 4f;


    public float noise03Scale = 6f;
    public float noise03Amp = 6f;
    */
    public Gradient gradient;

    private float minTerrainHeight;
    private float maxTerrainHeight;
    // Start is called before the first frame update
    void Start()
    {
        mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;

        //StartCoroutine(CreateShape());


    }

    private void Update()
    {
        CreateShape();
        UpdateMesh();
    }

    private void CreateShape()
    {
        // --- Create vertices
        vertices = new Vector3[(xSize + 1) * (zSize + 1)];

        for (int i = 0, z = 0; z <= zSize; z++)
        {
            for (int x = 0; x <= xSize; x++)
            {
                float y = Mathf.PerlinNoise(x * .3f, z * .3f) * 2f;
                vertices[i] = new Vector3(x, y, z);

                if (y > maxTerrainHeight)
                    maxTerrainHeight = y;
                if(y < minTerrainHeight)
                    minTerrainHeight = y;

                i++;
            }
        }

        //  --- Create Triangles
        triangles = new int[xSize * zSize * 6];

        int vert = 0;
        int tris = 0;

        for (int z = 0; z < zSize; z++)
        {
            for (int x = 0; x < xSize; x++)
            {
                triangles[tris + 0] = vert + 0;
                triangles[tris + 1] = vert + xSize + 1;
                triangles[tris + 2] = vert + 1;
                triangles[tris + 3] = vert + 1;
                triangles[tris + 4] = vert + xSize + 1;
                triangles[tris + 5] = vert + xSize + 2;

                vert++;
                tris += 6;

                //yield return new WaitForSeconds(0.01f);
            }
            vert++;
        }

        // --- Create UVs
        colors = new Color[vertices.Length];

        for (int i = 0, z = 0; z <= zSize; z++)
        {
            for (int x = 0; x <= xSize; x++)
            {
                float height = Mathf.InverseLerp(minTerrainHeight,maxTerrainHeight, vertices[i].y) ;
                colors[i] = gradient.Evaluate(height);

                i++;
            }
        }




        /*
        vertices = new Vector3[]
        {
            new Vector3 (0f, 0f, 0f),
            new Vector3 (0f, 0f, 1f),
            new Vector3 (1f, 0f, 0f),
            new Vector3 (1f, 0f, 1f)
        };

        triangles = new int[]
        {
            0, 1, 2,
            1, 3, 2
        };
        */
    }

    private void UpdateMesh()
    {
        mesh.Clear();

        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.colors = colors;

        mesh.RecalculateNormals();
    }
}

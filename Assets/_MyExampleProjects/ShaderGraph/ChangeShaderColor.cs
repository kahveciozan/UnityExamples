using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeShaderColor : MonoBehaviour
{
    private Material material;

    // Start is called before the first frame update
    void Start()
    {
        material = GetComponent<MeshRenderer>().material;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            material.SetColor("_Color", Color.red);
        }
    }
}

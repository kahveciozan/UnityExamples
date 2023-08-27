using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RenderTextureTest : MonoBehaviour
{

    void Start()
    {
        RenderTexture renderTexture =  new RenderTexture(256,256,8);
        Camera.main.targetTexture = renderTexture;
        Camera.main.targetTexture = null;
    }

}

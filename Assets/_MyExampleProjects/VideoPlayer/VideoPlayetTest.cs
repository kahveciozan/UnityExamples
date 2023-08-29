using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class VideoPlayetTest : MonoBehaviour
{
    [SerializeField]
    VideoPlayer videoPlayer;

    void Start()
    {
        videoPlayer.Prepare(); 

    }

    private void Update()
    {

        if (Input.GetKeyDown(KeyCode.B))
            videoPlayer.Play();
    }

}

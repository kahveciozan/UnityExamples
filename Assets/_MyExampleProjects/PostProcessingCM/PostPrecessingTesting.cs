using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class PostPrecessingTesting : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Volume volume = GetComponent<Volume>();

        if (volume.profile.TryGet<Bloom>(out Bloom bloom))
        {
            bloom.intensity.value = 10f;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

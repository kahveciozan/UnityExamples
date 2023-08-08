using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtTheCamera : MonoBehaviour
{
    private Transform cameraTransform;

    private void Start()
    {
        cameraTransform = Camera.main.transform; // Kameranýn Transform bileþenini alýyoruz
    }

    private void Update()
    {
        // Sphere objesini kameraya doðru yönlendirme
        transform.LookAt(cameraTransform.position);
    }
}

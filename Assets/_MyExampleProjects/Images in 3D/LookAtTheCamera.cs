using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtTheCamera : MonoBehaviour
{
    private Transform cameraTransform;

    private void Start()
    {
        cameraTransform = Camera.main.transform; // Kameran�n Transform bile�enini al�yoruz
    }

    private void Update()
    {
        // Sphere objesini kameraya do�ru y�nlendirme
        transform.LookAt(cameraTransform.position);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestLegacyAnimation1 : MonoBehaviour
{
    private void Awake()
    {
        Animation animation = GetComponent<Animation>();
        animation.Play("RightLeft");
    }
}

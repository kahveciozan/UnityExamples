using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallScript : MonoBehaviour
{
    public static event Action<Vector3> OnBallHit;


    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Ground")
        {
            OnBallHit?.Invoke(new Vector3(transform.position.x, transform.position.y, transform.position.z));
        }
    }
}

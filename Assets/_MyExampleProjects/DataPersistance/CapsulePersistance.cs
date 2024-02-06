using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CapsulePersistance : MonoBehaviour
{

    void Start()
    {
        GetComponent<Renderer>().material.color = MainManager.Instance.TeamColor;
    }

  
}

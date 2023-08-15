using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VariablesAndInspector : MonoBehaviour
{
    [SerializeField]
    private GameObject player;

    

   


    // Start is called before the first frame update
    void Start()
    {
        player = new GameObject("Player1");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnObject : MonoBehaviour
{
    [SerializeField] private GameObject prefab;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            Instantiate(prefab);
        }
    }
}



// Non-string methods for using addressables
// Entire folder as addressable, If you want load tons of assets at once you could make the folder itself addressable and load that

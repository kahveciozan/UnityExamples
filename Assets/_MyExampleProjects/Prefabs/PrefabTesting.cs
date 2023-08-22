using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrefabTesting : MonoBehaviour
{
    // We can use GameObject reference intead of Transform.
    // But I think transform is better because we can pass directly to Instantiate() function
    public Transform prefab;    

    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0; i < 10; i++)
        {
            Transform copy = Instantiate(prefab, new Vector3(0f, 0f, i * 5f), Quaternion.identity);  // Prefab - Position - Rotation
        }


    }

}

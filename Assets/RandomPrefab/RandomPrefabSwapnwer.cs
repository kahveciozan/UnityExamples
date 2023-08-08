using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomPrefabSwapnwer : MonoBehaviour
{
    [SerializeField] private List<GameObject> prefabList = new List<GameObject>();
    // Start is called before the first frame update
    void Start()
    {
        int prefabIndex = Random.Range(0, prefabList.Count);
        Instantiate(prefabList[prefabIndex]);
    }

}

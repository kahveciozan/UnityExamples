using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.SceneManagement;

[Serializable]
public class AssetReferenceAudioClip : AssetReferenceT<AudioClip>
{
    public AssetReferenceAudioClip(string guid) : base(guid){}
}

public class SpawnObjectsAddressables : MonoBehaviour
{
    [SerializeField] private AssetReferenceGameObject assetReferenceGameObject;

    private GameObject spawnedGameObject;

    public GameObject obje;
    // Update is called once per frame
    void Update()
    {
        Debug.Log("Build Path" + UnityEngine.AddressableAssets.Addressables.BuildPath);
        Debug.Log("Runtime Path" + UnityEngine.AddressableAssets.Addressables.RuntimePath);

        if (Input.GetKeyDown(KeyCode.T))
        {
           
            // This function is a generic so that means that we are going to return some type. For this time let's make it as a gameobject
            assetReferenceGameObject.LoadAssetAsync<GameObject>().Completed += 
                (asyncOperationHandle) =>
                {
                    if (asyncOperationHandle.Status == AsyncOperationStatus.Succeeded)
                    {
                        Instantiate(asyncOperationHandle.Result);
                    }
                    else
                    {
                        Debug.Log("Failed to load!");
                        obje.transform.localScale = Vector3.zero;
                    }
                };

            
        }

        if (Input.GetKeyDown(KeyCode.U))
        {
            assetReferenceGameObject.ReleaseInstance(spawnedGameObject);
            SceneManager.LoadScene(0);
        }

    }
}

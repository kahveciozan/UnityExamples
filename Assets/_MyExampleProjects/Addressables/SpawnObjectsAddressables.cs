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
    void Update()
    {
        Debug.Log("Build Path" + Addressables.BuildPath);
        Debug.Log("Runtime Path" + Addressables.RuntimePath);

        if (Input.GetKeyDown(KeyCode.T))
        {
            Method1();
        }

        if (Input.GetKeyDown(KeyCode.Y))
        {
            Method2();
        }

        if (Input.GetKeyDown(KeyCode.U))
        {
            Method3();
        }

        if (Input.GetKeyDown(KeyCode.I))
        {
            Method4();
        }

        if (Input.GetKeyDown(KeyCode.O))
        {
            Method5();
        }

        if (Input.GetKeyDown(KeyCode.A))
        {
            Method6_1();
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            Method6_2();
        }
    }

    #region Method1
    private void Method1()
    {
        AsyncOperationHandle<GameObject> asyncOperationHandle = Addressables.LoadAssetAsync<GameObject>("Assets/_MyExampleProjects/Addressables/Environment_Prefab.prefab");
        asyncOperationHandle.Completed += AsyncOperationHandle_Completed;
    }

    private void AsyncOperationHandle_Completed(AsyncOperationHandle<GameObject> asyncOperationHandle)
    {
        if (asyncOperationHandle.Status == AsyncOperationStatus.Succeeded)
        {
            Instantiate(asyncOperationHandle.Result);
        }
        else
        {
            Debug.Log("Yuklenirken bir hata olustu");
        }
    }

    #endregion

    #region Method2
    private void Method2()
    {
        Addressables.LoadAssetAsync<GameObject>("Assets/_MyExampleProjects/Addressables/Environment_Small.prefab").Completed += (asyncOperationHandle) =>
        {
            if (asyncOperationHandle.Status == AsyncOperationStatus.Succeeded)
            {
                Instantiate(asyncOperationHandle.Result);
            }
            else
            {
                Debug.Log("Yuklenirken bir hata olustu");
            }
        };
    }

    #endregion

    #region Method3
    [SerializeField] private AssetReference assetReferenceEnvironmentSmall2;

    private void Method3()
    {
        assetReferenceEnvironmentSmall2.LoadAssetAsync<GameObject>().Completed += (asyncOperationHandle) =>
        {
            if (asyncOperationHandle.Status == AsyncOperationStatus.Succeeded)
            {
                Instantiate(asyncOperationHandle.Result);
            }
            else
            {
                Debug.Log("Yuklenirken bir hata olustu");
            }
        };
    }
    #endregion

    #region Method4
    [SerializeField] AssetLabelReference assetLabelReferenceSimple;
    private void Method4()
    {
        Addressables.LoadAssetAsync<GameObject>(assetLabelReferenceSimple).Completed += (asyncOperationHandle) =>
        {
            if (asyncOperationHandle.Status == AsyncOperationStatus.Succeeded)
            {
                Instantiate(asyncOperationHandle.Result);
            }
            else
            {
                Debug.Log("Yuklenirken bir hata olustu");
            }
        };

    }
    #endregion

    #region Method5
    [SerializeField] AssetLabelReference assetLabelReferenceMultiple;       // Folder addressables'lari da label'lar ile yapmak mantikli
    private void Method5()
    {
        Addressables.LoadAssetsAsync<GameObject>(assetLabelReferenceMultiple, obj =>
        {
            Instantiate(obj);
            Debug.Log(obj.name);
        });

    }
    #endregion

    #region Method6
    [Header("SPECIFIC TYPES")]
    [SerializeField] AssetReferenceGameObject assetReferenceGameObject;

    private GameObject spawnedGameObject;
    private void Method6_1()
    {
        if (spawnedGameObject == null)
        {
            assetReferenceGameObject.InstantiateAsync().Completed += (asyncOperation) => spawnedGameObject = asyncOperation.Result;
        }
    }

    private void Method6_2()
    {
        if(spawnedGameObject != null)
        {
            assetReferenceGameObject.ReleaseInstance(spawnedGameObject);
        }
    }
    #endregion



}

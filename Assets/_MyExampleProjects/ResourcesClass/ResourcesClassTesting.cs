using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourcesClassTesting : MonoBehaviour
{

    void Start()
    {
        // Load Asset directly
        Transform cubeTransform = Resources.Load<Transform>("SubFolder/Cube");          // This folder name must be "Resources"
        Instantiate(cubeTransform);

        // Take count of assets in folder
        Debug.Log(Resources.LoadAll<Texture2D>("ItemIcons").Length);

        // Load async asset and take the name of asset
        Resources.LoadAsync<Transform>("SubFolder/Cube").completed += ResourcesClassTesting_completed;
        
    }

    private void ResourcesClassTesting_completed(AsyncOperation obj)
    {
        ResourceRequest resourceRequest = (ResourceRequest) obj;
        Debug.Log(resourceRequest.asset);
        
    }
}

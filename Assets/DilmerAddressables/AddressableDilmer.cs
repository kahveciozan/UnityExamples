using Cinemachine;
using System;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.AddressableAssets.ResourceLocators;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceProviders;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


[Serializable]
public class AssetReferenceAudioClip2 : AssetReferenceT<AudioClip>   
{
    public AssetReferenceAudioClip2(string guid) : base(guid) { }
}

public class AddressableDilmer : MonoBehaviour
{
    [SerializeField] private AssetReference playerArmatureReference;
    [SerializeField] private AssetReferenceAudioClip2 musicAssetReference;
    [SerializeField] private AssetReferenceTexture2D unityLogoAssetReference;
    [SerializeField] private CinemachineVirtualCamera cinemachineVirtualCamera;


    // UI Componenets
    [SerializeField] private RawImage rawImageUnityLogo;

    private GameObject playerController;

    // Level Loading
    private bool clearPreviousScene = false;
    private SceneInstance previousLoadedScene;

    // Start is called before the first frame update
    void Start()
    {
        // start the loader
        Loader.Instance.StartLoader();
        Debug.Log("Initializing Addressables"); Logger.Instance.LogInfo("Initializing Addressables");

        Addressables.InitializeAsync().Completed += AddressableDilmer_Completed;
    }

    private void AddressableDilmer_Completed(AsyncOperationHandle<IResourceLocator> obj)
    {
        Debug.Log("Loading the player armature"); Logger.Instance.LogInfo("Loading the player armature");

        playerArmatureReference.LoadAssetAsync<GameObject>().Completed += (playerArmatureAsset) =>
        {

            playerArmatureReference.InstantiateAsync().Completed += (playerArmatureGameObject) =>
            {
                Debug.Log("Intantiating the player controller..."); Logger.Instance.LogInfo("Intantiating the player controller...");

                // Loading Animation                

                playerController = playerArmatureGameObject.Result;
                cinemachineVirtualCamera.Follow = playerController.transform.Find("PlayerCameraRoot");

                
                
                Debug.Log("Intantiated the player controller..."); Logger.Instance.LogInfo("Intantiated the player controller...");
            };

            
        };

        musicAssetReference.LoadAssetAsync<AudioClip>().Completed += (clip) =>
        {
            var audioSource = gameObject.AddComponent<AudioSource>();
            audioSource.clip = clip.Result;
            audioSource.playOnAwake = false;
            audioSource.loop = true;
            audioSource.volume = 0.5f;
            audioSource.Play();

            Debug.Log("Loaded the audio clip... "); Logger.Instance.LogInfo("Loaded the audio clip... ");

        };

        unityLogoAssetReference.LoadAssetAsync<Texture2D>();

        Debug.Log("Loaded Asset.."); Logger.Instance.LogInfo("Loaded Asset..");

    }

    // Update is called once per frame
    void Update()
    {
        
        if (unityLogoAssetReference.Asset != null && rawImageUnityLogo.texture == null)
        {
            rawImageUnityLogo.texture = unityLogoAssetReference.Asset as Texture2D;
            Color currentColor = rawImageUnityLogo.color;
            currentColor.a = 1.0f;
            rawImageUnityLogo.color = currentColor;

            Debug.Log("Unity Logo Loaded as an asset"); Logger.Instance.LogInfo("Unity Logo Loaded as an asset");

        }

        // Checking for loadind content completion 
        if (playerArmatureReference.Asset != null 
            && musicAssetReference.Asset != null 
            && unityLogoAssetReference.Asset != null
            && Loader.Instance.IsLoading)
        {

            Loader.Instance.StopLoader();
        }

        #region Keyboard Input
        if (Input.GetKeyDown(KeyCode.Q))
        {
            ReloadScene();
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            LoadAddressableLevel("Assets/DilmerAddressables/Level1.unity");
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            LoadAddressableLevel("Assets/DilmerAddressables/Level2.unity");
        }
        #endregion

    }

    public void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void LoadAddressableLevel(string addressableKey)
    {
        if (clearPreviousScene)
        {
            Addressables.UnloadSceneAsync(previousLoadedScene).Completed += (asynHandle) =>
            {
                clearPreviousScene = false;
                previousLoadedScene = new SceneInstance();
                Debug.Log($"Unloaded scene {addressableKey} successfully");
            };
        }

        Addressables.LoadSceneAsync(addressableKey, LoadSceneMode.Additive).Completed += (asyncHandle) =>
        {
            clearPreviousScene = true;
            previousLoadedScene = asyncHandle.Result;
            Debug.Log($"Loaded scene {addressableKey} successfully");

        };
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;


// This sctipt for new Unity versions. Instead of Post use PostWwwForm.
// If you use Unity 2022 or newer, you can use this method.
// If you use Unity 2021 or older, you can use WebRequests.cs Sctipt in this project
public static class WebRequestsNew {

    private class WebRequestsMonoBehaviour : MonoBehaviour { }

    private static WebRequestsMonoBehaviour webRequestsMonoBehaviour;

    private static void Init()
    {
        if (webRequestsMonoBehaviour == null)
        {
            GameObject gameObject = new GameObject("WebRequests");
            webRequestsMonoBehaviour = gameObject.AddComponent<WebRequestsMonoBehaviour>();
        }
    }

    public static void Get(string url, Action<string> onError, Action<string> onSuccess)
    {
        Init();
        webRequestsMonoBehaviour.StartCoroutine(GetCoroutine(url, onError, onSuccess));
    }

    private static IEnumerator GetCoroutine(string url, Action<string> onError, Action<string> onSuccess)
    {
        using UnityWebRequest unityWebRequest = UnityWebRequest.Get(url);
        yield return unityWebRequest.SendWebRequest();

        if (unityWebRequest.result == UnityWebRequest.Result.ConnectionError ||
            unityWebRequest.result == UnityWebRequest.Result.DataProcessingError ||
            unityWebRequest.result == UnityWebRequest.Result.ProtocolError)
        {
            // Error
            onError(unityWebRequest.error);
        }
        else
        {
            onSuccess(unityWebRequest.downloadHandler.text);
        }
    }

    public static void Post(string url, Dictionary<string, string> formFields, Action<string> onError, Action<string> onSuccess)
    {
        Init();
        webRequestsMonoBehaviour.StartCoroutine(GetCoroutinePost(url, formFields, onError, onSuccess));
    }

    public static void Post(string url, string postData, Action<string> onError, Action<string> onSuccess)
    {
        Init();
        webRequestsMonoBehaviour.StartCoroutine(GetCoroutinePost(url, postData, onError, onSuccess));
    }

    public static void PostJson(string url, string jsonData, Action<string> onError, Action<string> onSuccess)
    {
        Init();
        webRequestsMonoBehaviour.StartCoroutine(GetCoroutinePostJson(url, jsonData, onError, onSuccess));
    }

    private static IEnumerator GetCoroutinePost(string url, Dictionary<string, string> formFields, Action<string> onError, Action<string> onSuccess)
    {
        using UnityWebRequest unityWebRequest = UnityWebRequest.Post(url, formFields);
        yield return unityWebRequest.SendWebRequest();

        if (unityWebRequest.result == UnityWebRequest.Result.ConnectionError ||
            unityWebRequest.result == UnityWebRequest.Result.DataProcessingError ||
            unityWebRequest.result == UnityWebRequest.Result.ProtocolError)
        {
            // Error
            onError(unityWebRequest.error);
        }
        else
        {
            onSuccess(unityWebRequest.downloadHandler.text);
        }
    }

    private static IEnumerator GetCoroutinePost(string url, string postData, Action<string> onError, Action<string> onSuccess)
    {
        using UnityWebRequest unityWebRequest = UnityWebRequest.PostWwwForm(url, postData);
        yield return unityWebRequest.SendWebRequest();

        if (unityWebRequest.result == UnityWebRequest.Result.ConnectionError ||
            unityWebRequest.result == UnityWebRequest.Result.DataProcessingError ||
            unityWebRequest.result == UnityWebRequest.Result.ProtocolError)
        {
            // Error
            onError(unityWebRequest.error);
        }
        else
        {
            onSuccess(unityWebRequest.downloadHandler.text);
        }
    }

    private static IEnumerator GetCoroutinePostJson(string url, string jsonData, Action<string> onError, Action<string> onSuccess)
    {
        using UnityWebRequest unityWebRequest = new UnityWebRequest(url, "POST");
        byte[] bodyRaw = Encoding.UTF8.GetBytes(jsonData);
        unityWebRequest.uploadHandler = new UploadHandlerRaw(bodyRaw);
        unityWebRequest.downloadHandler = new DownloadHandlerBuffer();
        unityWebRequest.SetRequestHeader("Content-Type", "application/json");

        yield return unityWebRequest.SendWebRequest();

        if (unityWebRequest.result == UnityWebRequest.Result.ConnectionError ||
            unityWebRequest.result == UnityWebRequest.Result.DataProcessingError ||
            unityWebRequest.result == UnityWebRequest.Result.ProtocolError)
        {
            // Error
            onError(unityWebRequest.error);
        }
        else
        {
            onSuccess(unityWebRequest.downloadHandler.text);
        }
    }

    public static void Put(string url, string bodyData, Action<string> onError, Action<string> onSuccess)
    {
        Init();
        webRequestsMonoBehaviour.StartCoroutine(GetCoroutinePut(url, bodyData, onError, onSuccess));
    }

    public static void Put(string url, string bodyData, string accessToken, Action<string> onError, Action<string> onSuccess)
    {
        Init();
        webRequestsMonoBehaviour.StartCoroutine(GetCoroutinePut(url, bodyData, accessToken, onError, onSuccess));
    }

    private static IEnumerator GetCoroutinePut(string url, string bodyData, Action<string> onError, Action<string> onSuccess)
    {
        using UnityWebRequest unityWebRequest = UnityWebRequest.Put(url, bodyData);
        yield return unityWebRequest.SendWebRequest();

        if (unityWebRequest.result == UnityWebRequest.Result.ConnectionError ||
            unityWebRequest.result == UnityWebRequest.Result.DataProcessingError ||
            unityWebRequest.result == UnityWebRequest.Result.ProtocolError)
        {
            // Error
            onError(unityWebRequest.error);
        }
        else
        {
            onSuccess(unityWebRequest.downloadHandler.text);
        }
    }

    public static void GetTexture(string url, Action<string> onError, Action<Texture2D> onSuccess)
    {
        Init();
        webRequestsMonoBehaviour.StartCoroutine(GetTextureCoroutine(url, onError, onSuccess));
    }

    private static IEnumerator GetTextureCoroutine(string url, Action<string> onError, Action<Texture2D> onSuccess)
    {
        using UnityWebRequest unityWebRequest = UnityWebRequestTexture.GetTexture(url);
        yield return unityWebRequest.SendWebRequest();

        if (unityWebRequest.result == UnityWebRequest.Result.ConnectionError ||
            unityWebRequest.result == UnityWebRequest.Result.DataProcessingError ||
            unityWebRequest.result == UnityWebRequest.Result.ProtocolError)
        {
            // Error
            onError(unityWebRequest.error);
        }
        else
        {
            DownloadHandlerTexture downloadHandlerTexture = unityWebRequest.downloadHandler as DownloadHandlerTexture;
            onSuccess(downloadHandlerTexture.texture);
        }
    }

    /* Post Request With header */
    public static void PostJson(string url, string jsonData, string accessToken, Action<string> onError, Action<string> onSuccess)
    {
        Init();
        webRequestsMonoBehaviour.StartCoroutine(GetCoroutinePostJson(url, jsonData, accessToken, onError, onSuccess));
    }

    /* Delete Request With header */
    public static void Delete(string url, string jsonData, string accessToken, Action<string> onError, Action<string> onSuccess)
    {
        Init();
        webRequestsMonoBehaviour.StartCoroutine(GetCoroutineDeleteJson(url, jsonData, accessToken, onError, onSuccess));
    }

    /* Put Request With header */
    public static void PutJson(string url, string jsonData, string accessToken, Action<string> onError, Action<string> onSuccess)
    {
        Init();
        webRequestsMonoBehaviour.StartCoroutine(GetCoroutinePutJson(url, jsonData, accessToken, onError, onSuccess));
    }

    private static IEnumerator GetCoroutinePostJson(string url, string jsonData, string accessToken, Action<string> onError, Action<string> onSuccess)
    {
        using UnityWebRequest unityWebRequest = new UnityWebRequest(url, "POST");

        if (accessToken == null)
        {
            Debug.Log("Access Token is null");
            onError(unityWebRequest.error);
            yield break;
        }

        unityWebRequest.SetRequestHeader("Authorization", accessToken);
        byte[] bodyRaw = Encoding.UTF8.GetBytes(jsonData);
        unityWebRequest.uploadHandler = new UploadHandlerRaw(bodyRaw);
        unityWebRequest.downloadHandler = new DownloadHandlerBuffer();
        unityWebRequest.SetRequestHeader("Content-Type", "application/json");

        yield return unityWebRequest.SendWebRequest();

        if (unityWebRequest.result == UnityWebRequest.Result.ConnectionError ||
            unityWebRequest.result == UnityWebRequest.Result.DataProcessingError ||
            unityWebRequest.result == UnityWebRequest.Result.ProtocolError)
        {
            // Error
            onError(unityWebRequest.error);
        }
        else
        {
            onSuccess(unityWebRequest.downloadHandler.text);
        }
    }

    private static IEnumerator GetCoroutinePutJson(string url, string jsonData, string accessToken, Action<string> onError, Action<string> onSuccess)
    {
        using UnityWebRequest unityWebRequest = new UnityWebRequest(url, "PUT");

        if (accessToken == null)
        {
            Debug.Log("Access Token is null");
            onError(unityWebRequest.error);
            yield break;
        }

        unityWebRequest.SetRequestHeader("Authorization", accessToken);
        byte[] bodyRaw = Encoding.UTF8.GetBytes(jsonData);
        unityWebRequest.uploadHandler = new UploadHandlerRaw(bodyRaw);
        unityWebRequest.downloadHandler = new DownloadHandlerBuffer();
        unityWebRequest.SetRequestHeader("Content-Type", "application/json");

        yield return unityWebRequest.SendWebRequest();

        if (unityWebRequest.result == UnityWebRequest.Result.ConnectionError ||
            unityWebRequest.result == UnityWebRequest.Result.DataProcessingError ||
            unityWebRequest.result == UnityWebRequest.Result.ProtocolError)
        {
            // Error
            onError(unityWebRequest.error);
        }
        else
        {
            onSuccess(unityWebRequest.downloadHandler.text);
        }
    }

    // Get Request With header 
    public static void Get(string url, string accessToken, Action<string> onError, Action<string> onSuccess)
    {
        Init();
        webRequestsMonoBehaviour.StartCoroutine(GetCoroutine(url, accessToken, onError, onSuccess));
    }

    private static IEnumerator GetCoroutine(string url, string accessToken, Action<string> onError, Action<string> onSuccess)
    {
        using UnityWebRequest unityWebRequest = UnityWebRequest.Get(url);

        if (accessToken == null)
        {
            Debug.LogWarning("Access Token is null");
            onError(unityWebRequest.error);
            yield break;
        }

        unityWebRequest.SetRequestHeader("Authorization", accessToken);
        yield return unityWebRequest.SendWebRequest();

        if (unityWebRequest.result == UnityWebRequest.Result.ConnectionError ||
            unityWebRequest.result == UnityWebRequest.Result.DataProcessingError ||
            unityWebRequest.result == UnityWebRequest.Result.ProtocolError)
        {
            onError(unityWebRequest.error);
        }
        else
        {
            onSuccess(unityWebRequest.downloadHandler.text);
        }
    }

    private static IEnumerator GetCoroutineDeleteJson(string url, string jsonData, Action<string> onError, Action<string> onSuccess)
    {
        using UnityWebRequest unityWebRequest = new UnityWebRequest(url, "Delete");
        byte[] bodyRaw = Encoding.UTF8.GetBytes(jsonData);
        unityWebRequest.uploadHandler = new UploadHandlerRaw(bodyRaw);
        unityWebRequest.downloadHandler = new DownloadHandlerBuffer();
        unityWebRequest.SetRequestHeader("Content-Type", "application/json");

        yield return unityWebRequest.SendWebRequest();

        if (unityWebRequest.result == UnityWebRequest.Result.ConnectionError ||
            unityWebRequest.result == UnityWebRequest.Result.DataProcessingError ||
            unityWebRequest.result == UnityWebRequest.Result.ProtocolError)
        {
            // Error
            onError(unityWebRequest.error);
        }
        else
        {
            onSuccess(unityWebRequest.downloadHandler.text);
        }
    }

    private static IEnumerator GetCoroutineDeleteJson(string url, string jsonData, string accessToken, Action<string> onError, Action<string> onSuccess)
    {

        using UnityWebRequest unityWebRequest = new UnityWebRequest(url, "Delete");
        if (accessToken == null)
        {
            Debug.Log("Access Token is null");
            onError(unityWebRequest.error);
            yield break;
        }
        unityWebRequest.SetRequestHeader("Authorization", accessToken);
        byte[] bodyRaw = Encoding.UTF8.GetBytes(jsonData);
        unityWebRequest.uploadHandler = new UploadHandlerRaw(bodyRaw);
        unityWebRequest.downloadHandler = new DownloadHandlerBuffer();
        unityWebRequest.SetRequestHeader("Content-Type", "application/json");

        yield return unityWebRequest.SendWebRequest();

        if (unityWebRequest.result == UnityWebRequest.Result.ConnectionError ||
            unityWebRequest.result == UnityWebRequest.Result.DataProcessingError ||
            unityWebRequest.result == UnityWebRequest.Result.ProtocolError)
        {
            // Error
            onError(unityWebRequest.error);
        }
        else
        {
            onSuccess(unityWebRequest.downloadHandler.text);
        }
    }

    private static IEnumerator GetCoroutinePut(string url, string bodyData, string accessToken, Action<string> onError, Action<string> onSuccess)
    {
        using UnityWebRequest unityWebRequest = UnityWebRequest.Put(url, bodyData);

        unityWebRequest.SetRequestHeader("Authorization", accessToken);
        unityWebRequest.SetRequestHeader("Content-Type", "application/json");

        yield return unityWebRequest.SendWebRequest();

        if (unityWebRequest.result == UnityWebRequest.Result.ConnectionError ||
            unityWebRequest.result == UnityWebRequest.Result.DataProcessingError ||
            unityWebRequest.result == UnityWebRequest.Result.ProtocolError)
        {
            // Error
            onError(unityWebRequest.error);
        }
        else
        {
            onSuccess(unityWebRequest.downloadHandler.text);
        }
    }
}

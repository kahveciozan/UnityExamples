using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;

public class WebRequest2 : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textMesh;


    void Start()
    {
        string url = "https://yapayzekaveteknolojiakademisi.com/";
        Get(url, (string error) =>
        {
            //Error
            Debug.Log("Error: " + error);
            textMesh.text ="Error: " + error;
        }, (string text) =>
        {
            // Success
            Debug.Log("Success: " + text);
            textMesh.text = "Success: " + text;
        });


    }

    private void Get(string url, Action<string> onError, Action<string> onSuccess)
    {
        StartCoroutine(GetRoutine(url, onError, onSuccess));
    }

    private IEnumerator GetRoutine(string url, Action<string> onError, Action<string> onSuccess)
    {
       
        using(UnityWebRequest request = UnityWebRequest.Get(url))
        {
            yield return request.SendWebRequest();

            if (request.result != UnityWebRequest.Result.Success)
            {
                onError(request.error);
            }
            else
            {
                onSuccess(request.downloadHandler.text);
            }
        }
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class ImageChooser : MonoBehaviour
{
    [SerializeField] private List<string> imageUrlList = new List<string>();
    [SerializeField] private List<Material> imageMaterialList = new List<Material>();
    [SerializeField] private List<Texture> defaultTextureList = new List<Texture>();

    // Start is called before the first frame update
    void Start()
    {
        // Randomaize the list
        ShuffleList(imageUrlList);
        ShuffleList(imageMaterialList);


        for (int i = 0; i< imageMaterialList.Count; i++)
        {
            StartCoroutine(DownloadImage(imageMaterialList[i], imageUrlList[i], defaultTextureList[i]));
        }
    }


    IEnumerator DownloadImage(Material imageMaterial, string imageUrl, Texture defaultTexture)
    {
        Debug.Log(imageUrl);

        UnityWebRequest request = UnityWebRequestTexture.GetTexture(imageUrl);
        yield return request.SendWebRequest();


        if (request.result != UnityWebRequest.Result.Success)
        {
            Debug.Log(request.error);
            imageMaterial.mainTexture = defaultTexture;
            Debug.Log("--- 1 ---");
        }
        else
        {
            imageMaterial.mainTexture = ((DownloadHandlerTexture)request.downloadHandler).texture;
            Debug.Log("--- 2 ---");
        }
    }


    public void ShuffleList<T>(List<T> list)
    {
        int n = list.Count;
        for (int i = n - 1; i > 0; i--)
        {
            int j = Random.Range(0, i + 1);
            var temp = list[i];
            list[i] = list[j];
            list[j] = temp;
        }
    }


}

using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class TestWWW : MonoBehaviour
{
    // GET
    // POST
    // UPLOAD FILE
    // DOWNLOAD FILE
    // ASSET DOWNLOAD
    // KISITLMALAR


    public Image CanvasImg;


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            // GET
            StartCoroutine(TestGet());
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            // POST
            StartCoroutine(TestPost());
        }

        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            // UPLOAD TEST FILE
            StartCoroutine(TestUploadFile());
        }

        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            // DOWNLOAD TEST FILE
            StartCoroutine(TestDownloadFile());
        }

        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            // DOWNLOAD TEST ASSET
            StartCoroutine(AssetDownloadAsset());
        }
    }

    IEnumerator TestGet()
    {
        using(UnityWebRequest request = UnityWebRequest.Get("https://postman-echo.com/get?test1=hello&test2=world"))
        {
            yield return request.SendWebRequest();

            if(request.result == UnityWebRequest.Result.Success)
            {
                Debug.Log("BASARILI GET" + request.downloadHandler.text);
            }
            else
            {
                Debug.Log("BASARISIZ GET" + request.error);
            }
        }
    }

    IEnumerator TestPost()
    {
        //WWWForm form = new WWWForm();
        //form.AddField("test1", "hello");
        //form.AddField("test2", "world");

        using (UnityWebRequest request = UnityWebRequest.PostWwwForm("https://postman-echo.com/post?test1=post+test", "Akdeniz ikliminde kışlar serin ve yağışlıdır"))
        {
            yield return request.SendWebRequest();

            if (request.result == UnityWebRequest.Result.Success)
            {
                Debug.Log("BASARILI POST" + request.downloadHandler.text);
            }
            else
            {
                Debug.Log("BASARISIZ POST" + request.error);
            }
        }
    }

    IEnumerator TestUploadFile()
    {

        using (UnityWebRequest request = UnityWebRequest.PostWwwForm("https://postman-echo.com/post?test3=file+upload+test", "formData=Akdeniz ikliminde kışlar serin ve yağışlıdır"))
        {

            UploadHandler uploadHandler = new UploadHandlerFile("C:/Users/ozan/Downloads/Instructions.csv");
            request.uploadHandler = uploadHandler;
            //request.SetRequestHeader("User-Agent", "Ozan");

            yield return request.SendWebRequest();

            if (request.result == UnityWebRequest.Result.Success)
            {
                Debug.Log("BASARILI FILE UPLOAD" + request.downloadHandler.text);
            }
            else
            {
                Debug.Log("BASARISIZ FILE UPLOAD" + request.error);
            }
        }
    }

    IEnumerator TestDownloadFile()
    {
        using (UnityWebRequest request = UnityWebRequest.Get("https://image5.sahibinden.com/staticContent/vehicleStockImagesV2/1358/939987/x_1216_684_88234001358-0dbf357cd72405143206089162de8106.jpg"))
        {

            DownloadHandlerFile downloadHandler = new DownloadHandlerFile(Path.Combine(Application.persistentDataPath, "image.png"));
            downloadHandler.removeFileOnAbort = true;   // eger indirme iptal edilirse dosyayı sil

            request.downloadHandler = downloadHandler;
            //request.SetRequestHeader("User-Agent", "Ozan");

            yield return request.SendWebRequest();

            if (request.result == UnityWebRequest.Result.Success)
            {
                Debug.Log("BASARILI FILE DOWNLOAD TO " + Path.Combine(Application.persistentDataPath, "image.png"));
            }
            else
            {
                Debug.Log("BASARISIZ FILE UPLOAD" + request.error);
            }
        }
    }

    IEnumerator AssetDownloadAsset()
    {
        using (UnityWebRequest request = UnityWebRequestTexture.GetTexture("https://image5.sahibinden.com/staticContent/vehicleStockImagesV2/1358/939987/x_1216_684_88234001358-0dbf357cd72405143206089162de8106.jpg"))
        {
            yield return request.SendWebRequest();

            if (request.result == UnityWebRequest.Result.Success)
            {
                Debug.Log("BASARILI ASSET DOWNLOAD");

                Texture2D texture = ((DownloadHandlerTexture)request.downloadHandler).texture;
                //Texture2D texture = DownloadHandlerTexture.GetContent(request);

                File.WriteAllText(Path.Combine(Application.persistentDataPath, "image.png"), texture.EncodeToPNG().ToString());

                CanvasImg.sprite = TextureToSprite(texture);


            }
            else
            {
                Debug.Log("BASARISIZ ASSET DOWNLOAD" + request.error);
            }
        }
    }

    private Sprite TextureToSprite(Texture2D texture)
    {
        return Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), Vector2.zero);
    }


    // Kısıtlamalar
    // fbx indiremeyiz. Bunu yapan harici assetler var. Yada assetBundle olarak indirebiliriz.
}

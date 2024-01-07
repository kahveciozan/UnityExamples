using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class TestJson : MonoBehaviour
{
    void Start()
    {
        //StartCoroutine(StartTest());
        //StartCoroutine(ArraysWithLINQ());
        //StartCoroutine(Deserialize());
        StartCoroutine(Serialize());
    }


    IEnumerator StartTest()
    {
        using (UnityWebRequest request = UnityWebRequest.Get("https://jsonplaceholder.typicode.com/todos/1"))
        {
            yield return request.SendWebRequest();

            if (request.result == UnityWebRequest.Result.Success)
            {
                JObject json = JObject.Parse(request.downloadHandler.text);

                Debug.Log("USER ID: " + json["userId"]);
                Debug.Log("ID: " + json["id"]);
                Debug.Log("TITLE: " + json["title"]);
                Debug.Log("IS COMPLETED: " + json["completed"]);
            }
            else
            {
                Debug.Log(request.error);
            }
        }
    }

    IEnumerator ArraysWithLINQ()
    {

        // LINQ yaklaşımı
        using (UnityWebRequest request = UnityWebRequest.Get("https://jsonplaceholder.typicode.com/users"))
        {
            yield return request.SendWebRequest();

            if (request.result == UnityWebRequest.Result.Success)
            {
                JArray jsonArray = JArray.Parse(request.downloadHandler.text);
                Debug.Log("USERS COUNT: " + jsonArray.Count);

                foreach(JObject json in jsonArray)
                {
                    Debug.Log("USER ID: " + json["id"]);
                    Debug.Log("NAME: " + json["name"]);
                    Debug.Log("USERNAME: " + json["username"]);
                    Debug.Log("EMAIL: " + json["email"]);
                    Debug.Log("ADDRESS: " + json["address"]);
                    Debug.Log("PHONE: " + json["phone"]);
                    Debug.Log("WEBSITE: " + json["website"]);
                    Debug.Log("COMPANY: " + json["company"]);
                }
            }
            else
            {
                Debug.Log(request.error);
            }
        }
    }


    IEnumerator Deserialize()
    {

        // LINQ yaklaşımı
        using (UnityWebRequest request = UnityWebRequest.Get("https://jsonplaceholder.typicode.com/todos/1"))
        {
            yield return request.SendWebRequest();

            if (request.result == UnityWebRequest.Result.Success)
            {
                string data = request.downloadHandler.text;

                Todo todo = JsonConvert.DeserializeObject<Todo>(data);

                Debug.Log("USER ID: " + todo.userId);
                Debug.Log("ID: " + todo.id);
                Debug.Log("TITLE: " + todo.title);
                Debug.Log("IS COMPLETED: " + todo.completed);

            }
            else
            {
                Debug.Log(request.error);
            }
        }
    }



    IEnumerator  Serialize()
    {
        Todo todo = new Todo()
        {
            userId = 14,
            id = 55,
            title = "Dünyanın en bilinçsiz adamı",
            completed = true
        };

        string jsonString = JsonConvert.SerializeObject(todo);
        Debug.Log(JsonConvert.SerializeObject(todo));   // json formatında stringe dönüştürdük


        using (UnityWebRequest request = UnityWebRequest.Post("https://postman-echo.com/post?test1=post+test", jsonString))
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

    public class Todo
    {
        public int userId;
        public int id;
        public string title;
        public bool completed;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestingPlayerPrefs : MonoBehaviour
{
    private void Awake()
    {

        if (!PlayerPrefs.HasKey("playerLevel"))
        {
            PlayerPrefs.SetInt("playerLevel", 2);
            Debug.Log("--- Player Level SAVED ---");
        }

        Debug.Log(PlayerPrefs.GetInt("playerLevel"));

        Debug.Log("playerLevel: " + PlayerPrefs.HasKey("playerLevel"));
        Debug.Log("PlayerLevel: " + PlayerPrefs.HasKey("PlayerLevel"));


        // Save data to json 
        if (!PlayerPrefs.HasKey("SaveObject"))
        {
            SaveObject saveObject = new SaveObject()
            {
                playerName = "Ozan Kahveci",
                playerLevel = 56,
                playerPosition = new Vector3(5, 0, 3)
            };
            JsonUtility.ToJson(saveObject);

            PlayerPrefs.SetString("SaveObject", JsonUtility.ToJson(saveObject));

            Debug.Log("--- SaveObject SAVED ---");

        }


        // Get json sata from local storage
        SaveObject saveObject2 = JsonUtility.FromJson<SaveObject>(PlayerPrefs.GetString("SaveObject"));

        Debug.Log("playerName: " + saveObject2.playerName);
        Debug.Log("playerName: " + saveObject2.playerLevel);
        Debug.Log("playerName: " + saveObject2.playerPosition);

        PlayerPrefs.Save();



        //PlayerPrefs.DeleteAll();
        //PlayerPrefs.DeleteKey("exampleKey");
    }


    private void SetBool(string key, bool value)
    {
        PlayerPrefs.SetInt(key, value ? 1 : 0);
    }

    private bool GetBool(string key)
    {


        return PlayerPrefs.GetInt(key) == 1;
    }


    [System.Serializable]
    public class SaveObject
    {
        public string playerName;
        public int playerLevel;
        public Vector3 playerPosition;
    }



}

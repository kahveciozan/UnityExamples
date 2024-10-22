using Assets.VariablesAndInspector.Typess;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This class is Player Features in Dilmer
public class VariablesAndInspector : MonoBehaviour
{
    [Header("PLAYER DEPENDENCIES")]
    [SerializeField]
    private GameObject playerPrefab;

    [Space]
    [Header("PLAYER INFORMATIONS")]
    [SerializeField]
    private Player player;

    public string PlayerName { get { return player.FirstName; } }

    private void OnEnable()
    {
        Debug.Log("Player Features Enabled");
        DisplayPlayerInfo();
    }

    void DisplayPlayerInfo()
    {
        Logger.Instance.LogInfo($"ID: {player.Id}");
        Logger.Instance.LogInfo($"First Name: {player.FirstName}");
        Logger.Instance.LogInfo($"Last Name: {player.LastName}");
        Logger.Instance.LogInfo($"Country Allowed: {player.CountryAllowed}");
    }

    // Start is called before the first frame update
    void Start()
    {
        //player = new GameObject("Player1");
        Debug.Log("Player Feaures Started");

        if(player == null)
        {
            string warningMessage = "Player has not been set";
            Debug.LogWarning(warningMessage);
            Logger.Instance.LogInfo(warningMessage);
        }
        else
        {
            Logger.Instance.LogInfo($"{playerPrefab.name} was referenced through the inpector");
        }
    }

}

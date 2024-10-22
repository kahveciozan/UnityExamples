using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InternetConnectionChecker : MonoBehaviour
{
    void Start()
    {
        CheckInternetConnection();
    }

    void CheckInternetConnection()
    {
        if (Application.internetReachability == NetworkReachability.NotReachable)
        {
            Debug.Log("No internet connection.");
            // Display a message to the user
        }
        else
        {
            Debug.Log("Internet connection available.");
            // Continue normal operation
        }
    }
}

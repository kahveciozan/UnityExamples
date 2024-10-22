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
        }
        else if (Application.internetReachability == NetworkReachability.ReachableViaCarrierDataNetwork)
        {
            Debug.Log("Connected via mobile data.");
        }
        else if (Application.internetReachability == NetworkReachability.ReachableViaLocalAreaNetwork)
        {
            Debug.Log("Connected via WiFi.");
        }
    }
}

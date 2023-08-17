using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeCycle : MonoBehaviour
{

    private void Awake()
    {
        Debug.Log("Awake");

    }

    private void OnEnable()
    {
        Debug.Log("OnEnable");

    }

    /// Reset is called when the user hits the Reset button in the Inspector's context menu or when adding the component the first time.
    /// This function is only called in editor mode. Reset is most commonly used to give good default values in the Inspector.
    private void Reset()
    {
        Debug.Log("Reset");
    }

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Start");

    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("Update");

    }

    private void FixedUpdate()
    {
        Debug.Log("Fixed Update");

    }

    private void LateUpdate()
    {
        Debug.Log("Late Update");

    }


    ///OnMasueXXX
    ///OnTriggerXXX
    ///OnCollisionXXX
    ///Scene Renderin Functions. Look the doc for more info
    ///OnDrawGizmos
    ///OnGUI
    ///OnApplicationPause
    ///OnDisable
    ///OnDestroy

}

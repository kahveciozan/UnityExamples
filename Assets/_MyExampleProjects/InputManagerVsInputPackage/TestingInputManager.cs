using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestingInputManager : MonoBehaviour
{
    private float horizantalValue;
    private bool jumpValue;


    void Update()
    {
        // Mouse
        if (Input.GetMouseButtonDown(1))    
        {
            Debug.Log("Mouse Right Ding!");
        }

        // Keyboard
        if (Input.GetKeyDown(KeyCode.T))
        {
            Debug.Log("Pressed T!");
        }

        // From Input Manager
        var tempHorizantalValue = Input.GetAxisRaw("Horizontal");
        if (horizantalValue != tempHorizantalValue)
        {
            horizantalValue = tempHorizantalValue;
            Debug.Log("Horizantal Value: " + horizantalValue);
        }

        var tempJumpValue = Input.GetButton("Jump");
        if (jumpValue != tempJumpValue)
        {
            jumpValue = tempJumpValue;
            Debug.Log(Input.GetButton("Jump"));
        }

    }
}

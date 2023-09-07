using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class TestingInputSystem : MonoBehaviour
{

    private void Awake()
    {
        PlayerInputActions playerInputActions = new PlayerInputActions();
        playerInputActions.Enable();
        playerInputActions.Player.Shoot.performed += Shoot_performed;
    }

    private void Shoot_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        Debug.Log("Input System Mouse Left Ding!");
    }

    private void Update()
    {
        if (Mouse.current.middleButton.wasPressedThisFrame)
        {
            Debug.Log("Input System Middle Button- Ding inside UPDATE");
        }

        if (Keyboard.current.yKey.wasPressedThisFrame)
        {
            Debug.Log("Input System Y button - Inside Update");
        }
    }
}

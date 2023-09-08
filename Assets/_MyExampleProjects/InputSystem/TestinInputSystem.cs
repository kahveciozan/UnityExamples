using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering.LookDev;
using UnityEngine;
using UnityEngine.InputSystem;

public class TestinInputSystem : MonoBehaviour
{
    private Rigidbody sphereRigidBody;
    private PlayerInput playerInput;
    private PlayerInputActions2 playerInputActions;

    private void Awake()
    {
        sphereRigidBody = GetComponent<Rigidbody>();
        playerInput = GetComponent<PlayerInput>();

        playerInputActions = new PlayerInputActions2();
        playerInputActions.Player.Enable();
        playerInputActions.Player.Jump.performed += Jump;

        playerInputActions.Player.Disable();
        playerInputActions.Player.Jump.PerformInteractiveRebinding()
            .WithControlsExcluding("Mouse")
            .OnComplete(callback =>
            {
                Debug.Log(callback.action.bindings[0].overridePath);
                callback.Dispose();
                playerInputActions.Player.Enable();
            })
            .Start();


        playerInputActions.Player.Movement.performed += Movement_performed;
    }

    private void Update()
    {
        if (Keyboard.current.uKey.wasPressedThisFrame)
        {
            playerInput.SwitchCurrentActionMap("UI");
            Debug.Log("PRESSED U");
            playerInputActions.Player.Disable();
            playerInputActions.Player.Enable();

        }
        if (Keyboard.current.pKey.wasPressedThisFrame)
        {
            playerInput.SwitchCurrentActionMap("Player");
            Debug.Log("PRESSED P");
            playerInputActions.Player.Disable();
            playerInputActions.Player.Enable();

        }
    }

    private void FixedUpdate()
    {
        Vector2 inputVector = playerInputActions.Player.Movement.ReadValue<Vector2>();
        //Debug.Log(inputVector);
        float speed = 1f;
        sphereRigidBody.AddForce(new Vector3(inputVector.x, 0, inputVector.y) * speed, ForceMode.Force);
    }

    private void Movement_performed(InputAction.CallbackContext context)
    {
        Debug.Log(context);

    }

    public void Jump(InputAction.CallbackContext context)
    {
        Debug.Log(context);

        if (context.performed)
        {
            Debug.Log("Jump! " + context.phase);
            sphereRigidBody.AddForce(Vector3.up * 5f, ForceMode.Impulse);
        }

    }

    public void Submit(InputAction.CallbackContext context)
    {
        Debug.Log("Submit " + context);

    }

}

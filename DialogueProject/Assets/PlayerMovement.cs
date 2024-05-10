using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour
{
    // Control variables
    private bool enableMovement = true;
    
    // Movement variables
    private Vector2 input;
    private Vector2 look;
    private CharacterController characterController;
    private Vector3 direction;
    [SerializeField] private float smoothTime = 0.03f;
    private float currentVelocity;
    [SerializeField] private float speed = 1;
    [SerializeField] private Camera camera; 
    [SerializeField] private float mouseSensitivity = 5f;
    private Vector2 currentMouseDelta = Vector2.zero;
    private Vector2 currentMouseDeltaVelocity = Vector2.zero;
    private float cameraPitch = 0.0f;


    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Awake()
    {
        characterController = GetComponent<CharacterController>();
    }
    
    private void Update()
    {
        if (!enableMovement) return;
        
        // Move functionality
        if (input != Vector2.zero)
        {
            direction = transform.right * input.x + transform.forward * input.y;
        }
        characterController.Move(direction.normalized * (speed * Time.deltaTime));
        
        // Look functionality
        look *= Time.deltaTime;
        currentMouseDelta = Vector2.SmoothDamp(currentMouseDelta, look, ref currentMouseDeltaVelocity, smoothTime);
        cameraPitch -= currentMouseDelta.y * mouseSensitivity;
        cameraPitch = Mathf.Clamp(cameraPitch, -90.0f, 90.0f);
        camera.transform.localEulerAngles = Vector3.right * cameraPitch;
        transform.Rotate(Vector3.up * currentMouseDelta.x * mouseSensitivity);
    }

    public void Move(InputAction.CallbackContext context)
    {
        input = context.ReadValue<Vector2>();
        direction = new Vector3(input.x, 0.0f, input.y).normalized;
    }

    public void Look(InputAction.CallbackContext context)
    {
        look = context.ReadValue<Vector2>() * mouseSensitivity;
    }

    public void DisableMovement()
    {
        enableMovement = false;
    }

    public void EnableMovement()
    {
        enableMovement = true;
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CustomMovement : MonoBehaviour
{
    [Header("Speed")] public float speedX = 1000f;
    public float speedZ = 1000f;

    [Header("Moving Body")] public Rigidbody rigidbody;

    private Vector3 move;

    private PlayerInputActions playerInputManager;

    // Start is called before the first frame update

    private void Awake()
    {
        playerInputManager = new PlayerInputActions();

        playerInputManager.PlayerAction.LS.performed += ctx => ProcessMovement(ctx.ReadValue<Vector2>());
        playerInputManager.PlayerAction.LS.canceled += ctx => ProcessMovement(Vector2.zero);
    }


    void ProcessMovement(Vector2 currMovement)
    {
        move = new Vector3(currMovement.x * speedX, 0, currMovement.y * speedZ) * Time.deltaTime;
    }

    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        rigidbody.AddForce(move);
    }

    private void OnEnable()
    {
        playerInputManager.Enable();
    }

    private void OnDisable()
    {
        playerInputManager.Disable();
    }
}
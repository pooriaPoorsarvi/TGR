using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    PlayerInputActions inputSystem;

    private Vector2 move;

    public float speed = 10f;

    public Transform playerTransform;

    private Vector3 initialDistance;

    // Start is called before the first frame update

    void Awake()
    {
        inputSystem = new PlayerInputActions();
        inputSystem.PlayerAction.RS.performed += ctx => move = ctx.ReadValue<Vector2>();
        inputSystem.PlayerAction.RS.canceled += ctx => move = Vector2.zero;
    }


    void Start()
    {
        initialDistance = playerTransform.position - transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 curr_move = new Vector3(move.x, move.y, 0) * speed * Time.deltaTime;
        transform.Translate(curr_move);
        transform.LookAt(playerTransform);
        initialDistance = playerTransform.position - transform.position;
    }


    private void OnEnable()
    {
        inputSystem.Enable();
    }

    private void OnDisable()
    {
        inputSystem.Disable();
    }
}
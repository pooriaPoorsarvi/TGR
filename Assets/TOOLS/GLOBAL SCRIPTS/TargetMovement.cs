using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetMovement : MonoBehaviour
{
    public float speed = .02f;
    public float adjustTime = .5f;
    public float maxY = 1;
    public float maxX = 1;
    public Transform target;


    public Animator animator;

    public float damping = .125f;


    private PlayerInputActions playerInputActions;
    private Vector2 move;
    private bool moving = false;
    private float _remainingTimer = 0f;



    public Transform charachterTarget;


    private void Awake()
    {
        playerInputActions = new PlayerInputActions();

        playerInputActions.PlayerAction.RS.performed += ctx => ProcessMovement(ctx.ReadValue<Vector2>());
        playerInputActions.PlayerAction.RS.canceled += ctx => ProcessMovement(Vector2.zero);
    }

    void ProcessMovement(Vector2 move)
    {
        this.move = move.normalized;
        if (move.x == 0 && move.y == 0)
        {
            moving = false;
        }
        else
        {
            moving = true;
            _remainingTimer = adjustTime;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (!moving)
        {
            Vector3 dist = transform.position - target.position;
            if (_remainingTimer <= 0)
            {
                _remainingTimer = 0f;
                return;
            }

            float multiplier = Time.deltaTime / _remainingTimer;
            _remainingTimer -= Time.deltaTime;
            transform.position -= dist * multiplier;
        }
        else
        {
            Vector3 newPosition = new Vector3(transform.position.x + move.x * speed,
                transform.position.y + move.y * speed,
                transform.position.z);
            if ((Math.Abs(newPosition.y - target.position.y) <= maxY) &&
                (Math.Abs(newPosition.x - target.position.x) <= maxX))
            {
                transform.position = newPosition;
            }
            
        }
    }


    private void OnEnable()
    {
        playerInputActions.Enable();
    }

    private void OnDisable()
    {
        playerInputActions.Disable();
    }
}
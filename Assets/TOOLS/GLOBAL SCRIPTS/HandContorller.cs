using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandContorller : MonoBehaviour
{
    private PlayerInputActions playerInputActions;

    private bool _grab = false;
    private bool _handMovementMode = false;


    private Vector3 move = Vector3.zero;

    [Header("Which Hand")]
    public bool leftHand;

    [Header("Movement Speed Of Hand")]
    public float ySpeed = 3f;
    public float xSpeed = 3f;
    public float zSpeed = 1f;
    
    [Header("Original Location In Case Of Release")]
    public Transform parent;

    private float _zAction = 0f;
    private void Awake()
    {
        playerInputActions = new PlayerInputActions();
        if (leftHand)
        {
            playerInputActions.PlayerAction.LB.started += ctx => GrabStarted();
            playerInputActions.PlayerAction.LB.canceled += ctx => GrabCancled();   
            
            playerInputActions.PlayerAction.LT.started += ctx => HandMovementEnabled();
            playerInputActions.PlayerAction.LT.canceled += ctx => HandMovementCancled();
        }
        else
        {
            playerInputActions.PlayerAction.RB.started += ctx => GrabStarted();
            playerInputActions.PlayerAction.RB.canceled += ctx => GrabCancled();   
            
            playerInputActions.PlayerAction.RT.started += ctx => HandMovementEnabled();
            playerInputActions.PlayerAction.RT.canceled += ctx => HandMovementCancled();
        }


        playerInputActions.PlayerAction.RS.performed += ctx => SetUpMoventXY(ctx.ReadValue<Vector2>());
        playerInputActions.PlayerAction.RS.canceled += ctx => SetUpMoventXY(Vector2.zero);


        playerInputActions.PlayerAction.UpButton.started += ctx => SetUpMovementZ(1);
        playerInputActions.PlayerAction.UpButton.canceled += ctx => SetUpMovementZ(0f);


        playerInputActions.PlayerAction.DownButton.started += ctx => SetUpMovementZ(-1f);
        playerInputActions.PlayerAction.DownButton.canceled += ctx => SetUpMovementZ(0f);
        // transform.parent = null;

    }

    public void SetUpMovementZ(float moveZ)
    {
        Debug.Log(moveZ);
        move.z = moveZ * zSpeed * Time.deltaTime;
    }
    public void SetUpMoventXY(Vector2 currMove)
    {
        move = new Vector3(currMove.x * xSpeed, currMove.y * ySpeed, move.z/Time.deltaTime) * Time.deltaTime;
    } 

    void GrabStarted()
    {
        _grab = true;
    }


    void GrabCancled()
    {
        _grab = false;
    }

    void HandMovementEnabled()
    {
        _handMovementMode = true;
    }

    void HandMovementCancled()
    {
        _handMovementMode = false;
    }


    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        transform.eulerAngles = Vector3.zero;
        if (_handMovementMode)
        {
            // transform.parent = null;
            transform.Translate(move);
        }
        else
        {
            transform.position = parent.position + Vector3.zero;
        }
    }

    public bool HandActivated()
    {
        return _handMovementMode;
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
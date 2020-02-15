using System;
using System.Collections;
using System.Collections.Generic;
using RootMotion.Dynamics;
using UnityEngine;
using RootMotion.Demos;
using Random = UnityEngine.Random;

public class KillPlayer : MonoBehaviour
{
    private PlayerInputActions playerInputActions;


    private void Awake()
    {
        playerInputActions = new PlayerInputActions();

        playerInputActions.PlayerAction.KillSwitch.performed += ctx =>Application.LoadLevel(0);;

    }


    // Start is called before the first frame update

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.tag);
        if (other.CompareTag("Player")||other.CompareTag("LevelEnder"))
        {
            Application.LoadLevel(0);
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

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RootMotion.Demos;
using RootMotion.Dynamics;

public class GrabDetector : MonoBehaviour
{
    

    

    [Tooltip("The Prop Muscle of the hand.")] 
    public PropMuscle propMuscle;

    [Header("Required For Finding The Right Control")]
    public bool leftHand;

    
    private PlayerInputActions playerInputActions;
    
    private bool _grab = false;


    private void Awake()
    {
        playerInputActions = new PlayerInputActions();
        if (leftHand)
        {
            playerInputActions.PlayerAction.LB.started += ctx => GrabStarted();
            playerInputActions.PlayerAction.LB.canceled += ctx => GrabCancled();
        }
        else
        {
            playerInputActions.PlayerAction.RB.started += ctx => GrabStarted();
            playerInputActions.PlayerAction.RB.canceled += ctx => GrabCancled();
        }
    }

    // Start is called before the first frame update
    void Start()
    {
    }
    
    void GrabStarted()
    {
        _grab = true;
    }


    void GrabCancled()
    {
        _grab = false;
        if (connectTo.currentProp != null)
        {
            connectTo.currentProp = null;
        }
    }

    // Update is called once per frame
    void Update()
    {
    }
    
    void OnTriggerEnter(Collider other)
    {
        if (!other.gameObject.CompareTag("Grabable")) 
        {
            return;
        }
        PuppetMasterProp prop = other.GetComponent<PuppetMasterProp>();
        Rigidbody rigidbody = other.GetComponent<Rigidbody>();
        
        if (connectTo.currentProp == null && (prop != null && rigidbody != null) && _grab)
        {
            connectTo.currentProp = prop;
            rigidbody.useGravity = true;
        } 
    }
    
    private PropMuscle connectTo {
        get {
            return propMuscle;
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

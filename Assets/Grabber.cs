using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grabber : MonoBehaviour
{

    private Rigidbody currGrabbedRigidBody;
    private Transform currGrabbedTransform;

    private PlayerInputActions playerInputActions;
    public bool leftHand;

    private bool _grab = false;
    private Vector3 initialTransform;

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

    


    void GrabStarted()
    {
        _grab = true;
    }


    void GrabCancled()
    {
        _grab = false;
        if (currGrabbedTransform != null)
        {
            currGrabbedTransform.parent = null;
            currGrabbedRigidBody.useGravity = true;
            currGrabbedTransform = null;
            currGrabbedRigidBody = null;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (currGrabbedTransform)
        {
            currGrabbedTransform.position = transform.position + initialTransform;
        }   
    }

    private void OnTriggerEnter(Collider other)
    {
        Rigidbody otherRigidbody = other.GetComponent<Rigidbody>();
        Transform otherTransform = other.GetComponent<Transform>();
        Debug.Log(other.gameObject.layer);
        Debug.Log(otherRigidbody != null);
        Debug.Log(otherTransform != null);
        Debug.Log(_grab);
        if (otherRigidbody != null && otherTransform != null && _grab)
        {
            Debug.Log("Fuck Got Here");
            otherRigidbody.isKinematic = true;
            // otherRigidbody.useGravity = true;
            currGrabbedTransform = otherTransform;
            currGrabbedRigidBody = otherRigidbody;
            initialTransform = transform.position - currGrabbedTransform.position;
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

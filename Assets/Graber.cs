using System;
using System.Collections;
using System.Collections.Generic;
using RootMotion.Dynamics;
using UnityEngine;

public class Graber : MonoBehaviour
{
    public bool is_left_hand;
    public float unmovable_limit = 1000f;
    public PuppetMaster puppetMaster;
    public BehaviourPuppet behaviourPuppet;


    private Rigidbody rb;
    private PlayerInputActions playerInputActions;

    private bool holding = false;
    private FixedJoint currentJoint = null;
    private float prev_pin_weight;


    private void Awake()
    {
        playerInputActions = new PlayerInputActions();


        if (!is_left_hand)
        {
            playerInputActions.PlayerAction.RT.started += ctx => UpdateHandState(true);
            playerInputActions.PlayerAction.RT.canceled += ctx => UpdateHandState(false);
        }

        if (is_left_hand)
        {
            playerInputActions.PlayerAction.LT.started += ctx => UpdateHandState(true);
            playerInputActions.PlayerAction.LT.canceled += ctx => UpdateHandState(false);
        }
    }
    

    private void UpdateHandState(bool holding)
    {
        this.holding = holding;
        if (holding == false && currentJoint != null)
        {
            Destroy(currentJoint);
            currentJoint = null;
            puppetMaster.pinWeight = prev_pin_weight;
            behaviourPuppet.canGetUp = true;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        prev_pin_weight = puppetMaster.pinWeight;
    }

    // Update is called once per frame
    void Update()
    {
    }


    private void OnTriggerEnter(Collider other)
    {
        if (holding == false || currentJoint != null)
        {
            return;
        }

        if (other.gameObject.CompareTag("Grabable"))
        {
            currentJoint = other.gameObject.AddComponent<FixedJoint>();
            Rigidbody other_rb = other.gameObject.GetComponent<Rigidbody>();

            if (other_rb.mass >= unmovable_limit)
            {
                behaviourPuppet.canGetUp = false;
                prev_pin_weight = puppetMaster.pinWeight;
                puppetMaster.pinWeight = 0;
            }
            
            currentJoint.connectedBody = rb;
            // currentJoint.breakForce = 1000;
            currentJoint.enablePreprocessing = false;
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
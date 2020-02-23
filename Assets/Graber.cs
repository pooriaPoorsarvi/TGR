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

    public Rigidbody third_puppet_part;


    private Rigidbody rb;
    private PlayerInputActions playerInputActions;

    private bool holding = false;
    private FixedJoint currentJoint = null;
    private float prev_pin_weight;

    private bool hanging = false;


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
            if (hanging)
            {
                // puppetMaster.pinWeight = prev_pin_weight;
                // behaviourPuppet.enabled = true;
                behaviourPuppet.unpinnedMuscleKnockout = true;
                third_puppet_part.constraints = RigidbodyConstraints.FreezeRotation;
                hanging = false;
            }
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

            if (other_rb.mass >= unmovable_limit || other_rb.isKinematic)
            {
                // prev_pin_weight = puppetMaster.pinWeight;
                // puppetMaster.pinWeight = 0;
                behaviourPuppet.unpinnedMuscleKnockout = false;
                third_puppet_part.constraints = RigidbodyConstraints.FreezeAll;
                hanging = true;
            }
            else
            {
                hanging = false;
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
﻿ using System;
using System.Collections;
using System.Collections.Generic;
using RootMotion.Dynamics;
using UnityEngine;

public class Graber : MonoBehaviour
{
    public GameObject handTick;
    public bool is_left_hand;
    public float unmovable_limit = 1000f;
    public PuppetMaster puppetMaster;
    public BehaviourPuppet behaviourPuppet;
    public int max_use = 4;


    public Rigidbody third_puppet_part;


    private Rigidbody rb;
    private PlayerInputActions playerInputActions;


    private bool holding = false;
    private FixedJoint currentJoint = null;
    private float prev_pin_weight;

    private bool hanging = false;


    private MuscleCollisionBroadcaster muscle;

    // private NPCHandler npcHandler;
    private NPCGrabbed nPCGrabbed = null;

    private GameObject holdingObject = null;

    private bool isHandActive = true;
    public float secondsInActive = 1f;

    public Limb limb;
    public float pushForce = 10f;
    public float additionalNPCPush = 10f;
    
    private void Awake()
    {
        playerInputActions = new PlayerInputActions();
    
        
        playerInputActions.PlayerAction.Push.performed += ctx => Push();

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

    void Push()
    {
        if (holding==null)
        {
            return;
        }
        NPCGrabbed temp = nPCGrabbed;
        Rigidbody rb = holdingObject.GetComponent<Rigidbody>();
        holding = false;
        StartCoroutine(inactivateHand());
        CheckJointAndExit();
        if (temp!=null)
        {
            rb.AddForce(rb.transform.forward* additionalNPCPush  * pushForce, ForceMode.VelocityChange);
        }
        if (rb!=null)
        {
            rb.AddForce(rb.transform.forward * pushForce, ForceMode.VelocityChange);
        }
    }

    IEnumerator inactivateHand()
    {
        isHandActive = false;
        yield return new WaitForSeconds(secondsInActive);
        isHandActive = true;
    }

    private void UpdateHandState(bool holding)
    {
        this.holding = holding;
        CheckJointAndExit();
    }

    private void CheckJointAndExit()
    {
        if (holding == false && currentJoint != null)
        {
            Destroy(currentJoint);
            currentJoint = null;
            holdingObject = null;
            if (nPCGrabbed != null)
            {
                nPCGrabbed.Released();
                nPCGrabbed = null;
            }
            
            if (hanging)
            {
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
        muscle = GetComponent<MuscleCollisionBroadcaster>();
    }

    // Update is called once per frame
    void Update()
    {
        if (max_use <= 0)
        {
            if (muscle != null)
            {
                CheckJointAndExit();
                muscle.puppetMaster.DisconnectMuscleRecursive(muscle.muscleIndex, MuscleDisconnectMode.Explode);
                muscle.Hit(10f, muscle.gameObject.transform.up * 10f, muscle.gameObject.transform.position);
                limb.limbDeactivateAction.Invoke();
                Destroy(handTick);
            }
        }
    }


    private void OnCollisionEnter(Collision other)
    {
        OnTriggerEnter(other.collider);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (holding == false || currentJoint != null || max_use < 0 || isHandActive == false )
        {
            return;
        }


        if (other.gameObject.CompareTag("Grabable")||other.gameObject.CompareTag("NPCGrabable"))
        {
            currentJoint = other.gameObject.AddComponent<FixedJoint>();
            Rigidbody other_rb = other.gameObject.GetComponent<Rigidbody>();

            nPCGrabbed = other.GetComponent<NPCGrabbed>();
            if (nPCGrabbed != null)
            {
                nPCGrabbed.Grabed();
            }

            holdingObject = other.gameObject;

            if (other_rb.mass >= unmovable_limit || other_rb.isKinematic)
            {
                behaviourPuppet.unpinnedMuscleKnockout = false;
                third_puppet_part.constraints = RigidbodyConstraints.FreezeAll;
                hanging = true;
            }
            else
            {
                hanging = false;
            }

            max_use -= 1;
            // notify scoring system that object was used
            ScoringSystem.ObjectWasUsed();

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

    public GameObject getHoldingGameObject()
    {
        return holdingObject;
    }
}
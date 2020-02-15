using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RootMotion.Demos;
using RootMotion.Dynamics;
using UnityEngine.InputSystem;


[RequireComponent(typeof(Animator))]
public class CustomIKAnimator : MonoBehaviour {
    
    [Header("Transforms To Follow For IK")]
    public Transform leftHandIKTarget;
    public Transform rightHandIKTarget;

    public string muscleNameLeft;
    public string muscleNamesRight;
    
    private bool shouldDoIK = false;

    private Animator animator;

    [Header("Hand Controller To Check The State")]
    public HandContorller leftHandContorller;
    public HandContorller rightHandController;

    public PuppetMaster puppetMaster;
    

    private void Awake()
    {
    }

    void Start() {
        animator = GetComponent<Animator>();
    }

    void OnAnimatorIK(int layer) {

        if (leftHandContorller.HandActivated())
        {
            animator.SetIKPosition(AvatarIKGoal.LeftHand, leftHandIKTarget.position);
            animator.SetIKPositionWeight(AvatarIKGoal.LeftHand, 1f);
            puppetMaster.muscles[findMuscle(muscleNameLeft)].props.pinWeight = 0.6f;
        }
        else
        {
            puppetMaster.muscles[findMuscle(muscleNameLeft)].props.pinWeight = 0.1f;
        }

        if (rightHandController.HandActivated())
        {
            animator.SetIKPosition(AvatarIKGoal.RightHand, rightHandIKTarget.position);
            animator.SetIKPositionWeight(AvatarIKGoal.RightHand, 1f);   
            puppetMaster.muscles[findMuscle(muscleNamesRight)].props.pinWeight = 0.6f;
        }
        else
        {
            puppetMaster.muscles[findMuscle(muscleNamesRight)].props.pinWeight = 0.1f;
        }
    }

    public int findMuscle(string name)
    {
        for (int i = 0; i < puppetMaster.muscles.Length; i++)
        {
            if (puppetMaster.muscles[i].name == name)
            {
                return i;
            }
        }
        return -1;
    }


}
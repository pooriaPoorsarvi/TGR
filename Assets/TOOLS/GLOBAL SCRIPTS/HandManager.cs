using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RootMotion.Demos;


[RequireComponent(typeof(Animator))]
public class HandManager : MonoBehaviour
{
    public Transform leftHandIKTarget;
    public Transform rightHandIKTarget;

    private PlayerInputActions playerInputActions;

    private bool _is_right_activated;
    private bool _is_left_activated;

    private enum Hand
    {
        LEFT_HAND,
        RIGHT_HAND
    }

    private void Awake()
    {
        playerInputActions = new PlayerInputActions();

        playerInputActions.PlayerAction.RT.started += ctx => UpdateHandState(Hand.RIGHT_HAND, true);
        playerInputActions.PlayerAction.RT.canceled += ctx => UpdateHandState(Hand.RIGHT_HAND, false);

        playerInputActions.PlayerAction.LT.started += ctx => UpdateHandState(Hand.LEFT_HAND, true);
        playerInputActions.PlayerAction.LT.canceled += ctx => UpdateHandState(Hand.LEFT_HAND, false);
    }

    void UpdateHandState(Hand hand, bool state)
    {
        if (hand == Hand.LEFT_HAND)
        {
            _is_left_activated = state;
        }
        else
        {
            _is_right_activated = state;
        }
    }


    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void OnAnimatorIK(int layer)
    {
        if (_is_left_activated)
        {
            animator.SetIKPosition(AvatarIKGoal.LeftHand, leftHandIKTarget.position);
            animator.SetIKPositionWeight(AvatarIKGoal.LeftHand, 1f);
        }

        if (_is_right_activated)
        {
            animator.SetIKPosition(AvatarIKGoal.RightHand, rightHandIKTarget.position);
            animator.SetIKPositionWeight(AvatarIKGoal.RightHand, 1f);
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
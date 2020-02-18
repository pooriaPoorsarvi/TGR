using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : StateMachineBehaviour
{

    public float damping = 0.15f;
    
    private PlayerInputActions playerInputActions;
    private readonly int m_hor = Animator.StringToHash("Horizontal");
    private readonly int m_ver = Animator.StringToHash("Vertical");


    private Vector2 move;
    
    private void Awake()
    {
        playerInputActions = new PlayerInputActions();
        
        playerInputActions.PlayerAction.LS.performed += ctx => ProcessMovement(ctx.ReadValue<Vector2>());
        playerInputActions.PlayerAction.LS.canceled += ctx => ProcessMovement(Vector2.zero);
    }


    void ProcessMovement(Vector2 move)
    {
        this.move = move.normalized;
    }
    
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        playerInputActions.Enable();
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.SetFloat(m_hor, move.x, damping, Time.deltaTime);
        animator.SetFloat(m_ver, move.y, damping, Time.deltaTime);
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        playerInputActions.Disable();
    }

    // OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that processes and affects root motion
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}

}

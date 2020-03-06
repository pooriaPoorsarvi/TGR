using System;
using System.Collections;
using System.Collections.Generic;
using JeffAI;
using RootMotion.Dynamics;
using UnityEngine;

public class NPCGrabbed : MonoBehaviour
{
    public BasicAI basicAi;

    public PuppetMaster puppetMaster;

    public BehaviourPuppet behaviourPuppet;

    public float AISleepTime = 2f;

    private float puppetMasterPinWeightSave;

    public float animationRemovalDamping = .01f;
    private Animator animator;
    private bool hasNoAnimation = false;
    private readonly int m_hor = Animator.StringToHash("Horizontal");
    private readonly int m_ver = Animator.StringToHash("Vertical");
    
    public void Start()
    {
        puppetMasterPinWeightSave = puppetMaster.pinWeight;
        animator = basicAi.animator;
    }

    private void Update()
    {
        if (hasNoAnimation)
        {
            animator.SetFloat(m_hor, 0, animationRemovalDamping, Time.deltaTime);
            animator.SetFloat(m_ver, 0, animationRemovalDamping, Time.deltaTime);
        }
    }

    public void Grabed()
    {
        basicAi.SetNoAction(true);
        basicAi.enabled = false;

        hasNoAnimation = true;
        
        behaviourPuppet.canGetUp = false;
        puppetMaster.pinWeight = 0;
    }

    public void Released()
    {
        behaviourPuppet.canGetUp = true;
        puppetMaster.pinWeight = 1;
        StartCoroutine(WaitAndWakeUpAI());
    }


    IEnumerator WaitAndWakeUpAI()
    {
        yield return new WaitForSeconds(AISleepTime);
        if (behaviourPuppet.canGetUp && (puppetMaster.pinWeight == puppetMasterPinWeightSave))
        {
            hasNoAnimation = false;
            basicAi.SetNoAction(false);
            basicAi.enabled = true;      
        }
    }
}
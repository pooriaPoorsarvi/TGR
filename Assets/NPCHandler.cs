using System;
using System.Collections;
using System.Collections.Generic;
using RootMotion.Dynamics;
using UnityEngine;

public class NPCHandler : MonoBehaviour
{
    public PuppetMaster puppetMaster;

    public BehaviourPuppet behaviourPuppet;


    public GameObject toDestroy;
    private float beforeWeight;
    
    
    // Start is called before the first frame update
    private Rigidbody rb;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void LetGo()
    {
        beforeWeight = puppetMaster.muscles[3].props.pinWeight;
        puppetMaster.muscles[3].props.pinWeight = 0;
        puppetMaster.muscles[6].props.pinWeight = 0;
    }

    public void GetBack()
    {
        puppetMaster.muscles[3].props.pinWeight = beforeWeight;
        puppetMaster.muscles[6].props.pinWeight = beforeWeight;
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 4)
        {
            Debug.Log(rb.velocity);
            if (Math.Abs(rb.velocity.y) > 3 || Math.Abs(rb.velocity.x) > 3 ||Math.Abs(rb.velocity.z) > 3 )
            {
                Destroy(toDestroy);
            }
        }

        ;
    }
}

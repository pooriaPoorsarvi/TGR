using System;
using System.Collections;
using System.Collections.Generic;
using JeffAI;
using RootMotion.Dynamics;
using UnityEngine;

public class DeathByForce : MonoBehaviour
{

    public float deathForceLimitBeingKilled = 15f;
    
    public BehaviourPuppet behaviourPuppet;
    public PuppetMaster puppetMaster;
    public BasicAI basicAi;
    
    public bool canBeDismembered = true;

    public GameObject NPCToBeDestroyed;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.collider.CompareTag("Grabable"))
        {
            if (other.relativeVelocity.magnitude > deathForceLimitBeingKilled)
            {
                // notify scoring system that kill was made
                ScoringSystem.KillMade();

                if (canBeDismembered)
                {
                 
                    MuscleCollisionBroadcaster to_remove = GetComponent<MuscleCollisionBroadcaster>();

                    to_remove.puppetMaster.DisconnectMuscleRecursive(to_remove.muscleIndex, MuscleDisconnectMode.Explode);
                    to_remove.Hit(10f, to_remove.gameObject.transform.up * 10f, to_remove.gameObject.transform.position);
                }
                Destroy(basicAi);
                behaviourPuppet.canGetUp = false;
                behaviourPuppet.unpinnedMuscleKnockout = true;
                puppetMaster.pinWeight = 0;
                StartCoroutine(DestroyNPC(3));
            }
        }
    }

    IEnumerator DestroyNPC(float waitSeconds)
    {
        yield return  new WaitForSeconds(waitSeconds);
        Destroy(NPCToBeDestroyed);
    }
}
using System.Collections;
using System.Collections.Generic;
using RootMotion.Dynamics;
using UnityEngine;

public class DeathTimer : MonoBehaviour
{
    public float time;
    public PuppetMaster puppetMaster;
    public GameObject[] to_be_removed;
    public BehaviourPuppet behaviourPuppet;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (time <= 0)
        {
            foreach (var obj in to_be_removed)
            {
                MuscleCollisionBroadcaster to_remove = obj.GetComponent<MuscleCollisionBroadcaster>();
                if (to_remove == null)
                {
                    continue;
                }
                to_remove.puppetMaster.DisconnectMuscleRecursive(to_remove.muscleIndex, MuscleDisconnectMode.Explode);
                to_remove.Hit(10f, to_remove.gameObject.transform.up * 10f, to_remove.gameObject.transform.position);
            }

            behaviourPuppet.canGetUp = false;
            behaviourPuppet.unpinnedMuscleKnockout = true;
        }
        else
        {
            time -= Time.deltaTime;
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            if (puppetMaster != null)
            {
                puppetMaster.ReconnectMuscleRecursive(0);
            }
        }
    }
}
using System.Collections;
using System.Collections.Generic;
using RootMotion.Dynamics;
using UnityEngine;

public class headRemover : MonoBehaviour
{
    public MuscleCollisionBroadcaster to_remove;

    public PuppetMaster puppetMaster;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (to_remove!=null)
        {
            to_remove.puppetMaster.DisconnectMuscleRecursive(to_remove.muscleIndex, MuscleDisconnectMode.Explode);
            to_remove.Hit(10f, to_remove.gameObject.transform.up * 10f, to_remove.gameObject.transform.position);
            to_remove = null;
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

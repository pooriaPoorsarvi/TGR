using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathByForce : MonoBehaviour
{

    public GameObject objectToBeKilled; 
    
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
            if (other.relativeVelocity.magnitude > 10f)
            {
                Destroy(objectToBeKilled);
            }
        }
    }
    
}

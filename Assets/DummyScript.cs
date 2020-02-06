using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DummyScript : MonoBehaviour
{

    public String s;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnCollisionEnter(Collision other)
    {
        Debug.Log("Collision");
        Debug.Log(s);
        Debug.Log(other.gameObject.tag.CompareTo("Grabable"));
    }

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("Trigger");
        Debug.Log(s);
        Debug.Log(other.gameObject.tag);
        Debug.Log(other.gameObject.CompareTag("Grabable"));
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class push : MonoBehaviour
{
    private Rigidbody rb;

    public float force;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void OnEnable()
    {
        rb = GetComponent<Rigidbody>();
        rb.AddForce(transform.up * force, ForceMode.VelocityChange);
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraContainerMover : MonoBehaviour
{
    public Transform target;

    private Vector3 initialDistance;

    // Start is called before the first frame update
    void Start()
    {
        initialDistance = transform.position - target.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = initialDistance + target.position;
    }
}

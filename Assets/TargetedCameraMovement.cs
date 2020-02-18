using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetedCameraMovement : MonoBehaviour
{
    public Transform originalImit;
    public Transform targetImit;
    public Transform target;
    public Transform lookAt;
    
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = target.position + (originalImit.position - targetImit.position);
        transform.LookAt(lookAt);
    }
}

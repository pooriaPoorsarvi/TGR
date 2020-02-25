using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RSMovementImitator : MonoBehaviour
{
    private Transform originalImit;
    private Transform targetImit;
    private Transform target;

    public GameObject movementImitation;
    public GameObject movementTarget;

    public Transform imitatorParent;
    public bool inverse = false;

    public float movement_coefficient = 1f;
    
    // Start is called before the first frame update
    void Start()
    {
        if (imitatorParent == null)
        {
            imitatorParent = transform.parent;
        }
        
        GameObject clone = Instantiate(movementImitation, Vector3.zero, Quaternion.identity, imitatorParent);

        originalImit = clone.transform.GetChild(0);
        targetImit = clone.transform.GetChild(1);
        
        target = Instantiate(movementTarget, Vector3.zero, Quaternion.identity, transform.parent).transform;
        target.localPosition = transform.localPosition;
    }

    // Update is called once per frame
    void Update()
    {
        if (!inverse)
        {
            transform.position = target.position + movement_coefficient * (originalImit.position - targetImit.position);
        }
        else
        {
            transform.position = target.position - movement_coefficient * (originalImit.position - targetImit.position);
        }

        // transform.LookAt(lookAt);
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraLooker : MonoBehaviour
{

    public Transform lookAt;

    public float height; 
    public float radius;

    public Material transperentMaterial;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(lookAt);
        
        Vector3 p1 = transform.position - transform.up*height/2;
        Vector3 p2 = transform.position + transform.up*height/2;
        Vector3 dir = lookAt.position - transform.position;
        float length = dir.magnitude;
        dir /= length;
        RaycastHit[] hits = Physics.CapsuleCastAll(p1, p2, radius, dir, length);
        for (int i = 0; i < hits.Length; i++)
        {
            Renderer R = hits[i].collider.GetComponent<Renderer>();
            if (R == null)
                continue;
            AutoTransparent AT = R.GetComponent<AutoTransparent>();
            if (AT == null) // if no script is attached, attach one
            {
                AT = R.gameObject.AddComponent<AutoTransparent>();
                AT.transparentMaterial = transperentMaterial;
            }
            AT.BeTransparent(); // get called every frame to reset the falloff
        }
    }
}

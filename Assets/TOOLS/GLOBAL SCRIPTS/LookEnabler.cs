using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookEnabler : MonoBehaviour
{
    // Start is called before the first frame update
    public CameraLooker cameraLooker;
    void Start()
    {
        cameraLooker.enabled = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

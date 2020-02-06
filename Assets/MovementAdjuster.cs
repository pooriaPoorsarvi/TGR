using System.Collections;
using System.Collections.Generic;
using RootMotion.Dynamics;
using UnityEngine;

public class MovementAdjuster : MonoBehaviour
{
    // Start is called before the first frame update
    private bool thrownOnGround;
    public PuppetMaster puppetMaster;
    public float customPinWeight = 0.5f;
    public float safeTime = 3f;
    private float _currentSafeTime = 0;
    
    [Header("Animator Target")]
    public Rigidbody rigidbody;
    public Transform target;
    public float safetyJumpSpeed = 4f;
    
    void Start()
    {
        thrownOnGround = false;
    }

    // Update is called once per frame
    void Update()
    {
        
        if (thrownOnGround)
        {
            puppetMaster.pinWeight = 1f;
            _currentSafeTime -= Time.deltaTime;
            if (_currentSafeTime <= 0)
            {
                rigidbody.AddForce((target.up+ target.forward + target.right) * safetyJumpSpeed, ForceMode.Acceleration);
                _currentSafeTime = safeTime;
            }
        }
        else
        {
            _currentSafeTime = 0;
            puppetMaster.pinWeight = customPinWeight;
        }
        
    }

    public void ThrownListerer(bool state)
    {
        if (state && !thrownOnGround)
        {
            _currentSafeTime = safeTime;
        }
        thrownOnGround = state;
    }

}

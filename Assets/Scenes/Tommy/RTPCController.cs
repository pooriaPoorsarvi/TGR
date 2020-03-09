using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RTPCController : MonoBehaviour
{
    public float speed = 0;
    public AK.Wwise.Event eventName;
    public bool canPlaySound = true;
    public float waitTimer = 0.2f;

    public HandPublisher handPublisher;
    
    void OnCollisionEnter(Collision collision)
    {
        speed = collision.relativeVelocity.magnitude;

        //Speed is the Parameter of RTPC in Wwise, the higher the speed, the louder the impact sound
        AkSoundEngine.SetRTPCValue("Speed", speed);
        if (canPlaySound && (! handPublisher.isBeingHeld(gameObject)))
        {
            StartCoroutine(playThatSound());
        }
    }

    IEnumerator playThatSound()
    {
        //play the sound and disable all sound
        eventName.Post(gameObject);
        canPlaySound = false;
        //wait for 0.2 seconds and renable sound
        yield return new WaitForSeconds(waitTimer);
        canPlaySound = true;
    }

}
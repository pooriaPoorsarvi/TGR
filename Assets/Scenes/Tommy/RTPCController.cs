using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RTPCController : MonoBehaviour
{
   public float speed = 0;
   public AK.Wwise.Event eventName;
  
   
	void OnCollisionEnter(Collision collision)
		{
			
			speed = collision.relativeVelocity.magnitude;

			//Speed is the Parameter of RTPC in Wwise, the higher the speed, the louder the impact sound
			AkSoundEngine.SetRTPCValue("Speed", speed);

			eventName.Post(gameObject);
			
		}
   
  void Update()
     {
      
     }
   
   
   }

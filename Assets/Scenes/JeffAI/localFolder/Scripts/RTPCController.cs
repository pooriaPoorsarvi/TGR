using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using UnityEngine.Events;

namespace JeffAI{
	public class RTPCController : MonoBehaviour
	{
	   public float speed = 0;
	   public AK.Wwise.Event eventName;
	   public bool canPlaySound = true;
	   public float waitTimer = 0.2f;

	   private int lblrCount = 0;
	   private PlayerInputActions playerInputManager;

	    void SelDown(){
	        lblrCount++;

	        if(lblrCount == 2){
	        	canPlaySound = true;
	        }
	        else{
	        	canPlaySound = false;
	        }
	    }

	    void SelUp(){
	        lblrCount--;
	    }


	    private void Awake()
	    {
	        playerInputManager = new PlayerInputActions();

	        playerInputManager.PlayerAction.LB.started += ctx => SelDown();
	        playerInputManager.PlayerAction.LB.canceled += ctx => SelUp();

	        playerInputManager.PlayerAction.RB.started += ctx => SelDown();
	        playerInputManager.PlayerAction.RB.canceled += ctx => SelUp();

	    }     
	   
		void OnCollisionEnter(Collision collision)
			{
				
				speed = collision.relativeVelocity.magnitude;

				//Speed is the Parameter of RTPC in Wwise, the higher the speed, the louder the impact sound
				AkSoundEngine.SetRTPCValue("Speed", speed);
	        if (canPlaySound)
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
	    void Update()
	     {
			if (Input.GetButton("rightTrigger"))

			canPlaySound = false;
	     }
	   
	   
	   }
}
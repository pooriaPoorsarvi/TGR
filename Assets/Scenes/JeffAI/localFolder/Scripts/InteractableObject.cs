using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class InteractableObject : MonoBehaviour
{
    
    // hint for this object
    public String hint;

    private bool display = false;

    // delay during which hint will be displayed
    public float waitDelay;

    private IEnumerator coroutine;

    public String GetHintText(){
    	return hint;
    }

    public bool NeedToDisplay(){
    	return display;
    }

    // counter for displaying the hint
    IEnumerator DisplayHint()
    {
    	display = true;
        yield return new WaitForSeconds(waitDelay);
        display = false;
    }


    // actions to be performed when player enters the trigger
    void OnTriggerEnter(Collider col){
        if(col.gameObject.tag == "Player"){
            // player entered the trigger zone
            if(coroutine != null){
            	StopCoroutine(coroutine);
            	display = false;
            }
            coroutine = DisplayHint();
            StartCoroutine(coroutine);
        }
    }

    // actions to be performed when player leaves the trigger
    void OnTriggerExit(Collider col){
        if(col.gameObject.tag == "Player"){
            // player exited the trigger zone
            if(coroutine != null){
            	StopCoroutine(coroutine);
            	display = false;
            }
        }
    }



}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AIbridge : MonoBehaviour
{
    public GameObject npc;
    public float offsetY;
    public GameObject animGaud;

    public float maxDistance;
    public float lastDistance;
    public Vector3 lastGoodPos;

    private bool yielded = false;
    public float delay;


IEnumerator InitialDelay()
        {
            yield return new WaitForSeconds(delay);
            yielded = true;  
        } 


    void Start(){
        IEnumerator coroutine = InitialDelay();
        StartCoroutine(coroutine);
    }

    private bool one = false;

    void Update(){
    	gameObject.transform.position = new Vector3(npc.transform.position.x, npc.transform.position.y + offsetY, npc.transform.position.z);
        gameObject.transform.rotation = npc.transform.rotation;
        //animGaud.transform.position = new Vector3(0f, 0f, 0f);
        //animGaud.transform.rotation = new Quaternion(0f, 0f, 0f, 1f);

        if(yielded){

	        lastDistance = Vector3.Distance(animGaud.transform.position, npc.transform.position);
	        if(lastDistance >= maxDistance){
	            animGaud.transform.position = lastGoodPos;
	            animGaud.transform.LookAt(npc.transform);
	        }
	        else if(!one){
	        	lastGoodPos = animGaud.transform.position;
	        	one = true;
	        }

        }
    }
}

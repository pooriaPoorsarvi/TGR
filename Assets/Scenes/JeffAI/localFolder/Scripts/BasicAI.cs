﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System;
using System.Linq;

namespace JeffAI{

	public class BasicAI : MonoBehaviour
	{

	    public Transform curGoal;
	    public float repathDist;
	    public PatrolArea patrolArea;

	    private bool isMoving = false;
	    public int curWaypointIndex = 0;
	    public bool loop = false;
	    public bool pingPong = false;
	    public float waitTime;
	    public String animationControllerRoutineName;
	    private IEnumerator waitTimer;
	    public bool isFreaky = false;
	    public float speedBoost;


        public void BecomeScared(){
        	if(isFreaky){
        		return;
        	}
        	isFreaky = true;
    		gameObject.GetComponent<NavMeshAgent>().speed += speedBoost;
    		if(waitTimer != null){
    			StopCoroutine(waitTimer);
    			NextGoal();
    		}
    		Debug.Log("one civilian got scared.");
        }


	    void Update(){
	    	if(isMoving){
	            float dist = Vector3.Distance(transform.position, curGoal.position);
	            if(dist <= repathDist){
	                isMoving = false;
	                waitTimer = WaitAndProceed();
	                StartCoroutine(waitTimer);
	            }
	        }
	    }


	    private void BeginMovement(){
	    	if(curGoal != null){
	    		gameObject.GetComponent<NavMeshAgent>().SetDestination(curGoal.transform.position);
	    		isMoving = true;
	    	}
	    }


	    IEnumerator WaitAndProceed()
	    {   
	    	if(!isFreaky){
	            yield return new WaitForSeconds(waitTime);
	        }
	        Debug.Log("Waiting first!");
	        NextGoal();

	    }          


	    private void NextGoal(){
	    	Transform newGoal;
	    	if(!isFreaky){
	            newGoal = patrolArea.RequestWaypoint(curWaypointIndex);
	        }
	        else{
	        	newGoal = patrolArea.RequestRandomWaypoint();
	        }
	        if(newGoal != null){
	        	curGoal = newGoal;
	        	curWaypointIndex++;
	        	BeginMovement();
	        }
	        else if(loop && patrolArea.GetWaypointCount() > 0){
	        	curWaypointIndex = 0;
	        	NextGoal();
	        }
	        else if(curWaypointIndex > 0 && pingPong){
	            curWaypointIndex--;
	            NextGoal();
	        }
	    }


	    void Start()
	    {
	    	CivilianViewer.NoticedPlayer += BecomeScared;
	    	waitTimer = WaitAndProceed();
	        StartCoroutine(waitTimer);
	    }
	}

}
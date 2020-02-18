using System.Collections;
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
	    
	    // Period of time for which npc stops at the waypoint
	    public float waitTime;
	    public String animationControllerRoutineName;
	    
	    private IEnumerator waitTimer;
	    public bool isFreaky = false;
	    public float speedBoost;
        

        // number of seconds for which npc remains scared
        public int scaredMinPeriod;
        private IEnumerator scaredTimer;
        public GameObject scaredIndicator;

        // variable that indicates whether npc wants to escape from the level
        public bool wantsToEscape = false;


        // how much time is left
        private String timerText;

        public String GetTimerText(){
        	return timerText;
        }


        IEnumerator WaitAndBecomeUnscared(int mins)
        {
	    	int counter = mins;
	        while(true){
	            yield return new WaitForSeconds(1f);
	            counter--;

	            String hourText = "";
	            String minText = "";
	            String secText = "";

	            int hoursTimer = mins / 60;
	            int minTimer = mins - hoursTimer * 60;
	            int secTimer = counter;

	            if(hoursTimer >= 10){
	                hourText = hoursTimer.ToString();
	            }
	            else{
	                hourText = "0" + hoursTimer.ToString();
	            }
	            if(minTimer >= 10){
	                minText = minTimer.ToString();
	            }
	            else{
	                minText = "0" + minTimer.ToString();
	            }
	            if(secTimer >= 10){
	                secText = secTimer.ToString();
	            }
	            else{
	                secText = "0" + secTimer.ToString();
	            }   
	            timerText = hourText + ":" + minText + ":" + secText; 

	            if(mins == 0 && counter == 0){
	                CalmDown();
	                break;
	            }

	            if(counter == 0){
	                mins -= 1;
	                counter = 60;
	            }            
            }
        } 

        // transition back into normal routine after being scared
	    private void CalmDown(){
            isFreaky = false;
            scaredIndicator.active = false;
            gameObject.GetComponent<NavMeshAgent>().speed -= speedBoost;
	    }


        public void BecomeScared(){
        	if(isFreaky){
        		return;
        	}
        	isFreaky = true;
        	scaredIndicator.active = true;
    		gameObject.GetComponent<NavMeshAgent>().speed += speedBoost;
 
            if(scaredTimer != null){
                StopCoroutine(scaredTimer);
            }

            // start scared timer
            scaredTimer = WaitAndBecomeUnscared(scaredMinPeriod);
            StartCoroutine(scaredTimer);

    		if(waitTimer != null){
    			StopCoroutine(waitTimer);
    			NextGoal();
    		}
    		Debug.Log("one civilian got scared.");
        }


	    void Update(){
	    	if(isMoving){
	            float dist = Vector3.Distance(transform.position, curGoal.position);
                
                float distLeft = dist - gameObject.GetComponent<NavMeshAgent>().stoppingDistance * 2f;
                
                if(wantsToEscape){
                	Debug.Log("distance left to escape the level is " + distLeft);
                }

	            if(distLeft <= repathDist){
	            
	            	if(!wantsToEscape){
	                    isMoving = false;
	                    waitTimer = WaitAndProceed();
	                    StartCoroutine(waitTimer);
	                }
	                else{
	                	// npc that was trying to escape the map has reached its goal, make player lose the game
	                	GameplayManager.LoseGame();
	                }

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
	    		// npc isnt' scared, stop for a break and play some animation
	    		// waiting for character models to be completed to call some actions on the animation controller
	            yield return new WaitForSeconds(waitTime);
	        }
	        NextGoal();
	    }          


	    private void NextGoal(){
	    	Transform newGoal;
	    	if(!isFreaky){
	            newGoal = patrolArea.RequestWaypoint(curWaypointIndex);
	        }
	        else{
	        	if(!wantsToEscape){
	        	    // if ai wants to escape, pick a random waypoint where it should be going
	        	    newGoal = patrolArea.RequestRandomWaypoint();
	        	}
	        	else{
	        		// ai wants to escape from the map
	        		newGoal = patrolArea.RequestEscapeWaypoint();
	        	}
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
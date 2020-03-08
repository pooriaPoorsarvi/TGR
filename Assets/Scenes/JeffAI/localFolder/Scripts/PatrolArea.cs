using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace JeffAI{

	public class PatrolArea : MonoBehaviour
	{
	   
	    // waypoints for general routines
	    public NavmeshWaypoint[] waypoints;

	    // array of points that can be used as exits
	    public Transform[] escapeWaypoints;
    
        
        public Transform RequestEscapeWaypoint(){
            if(escapeWaypoints.Length != 0){
            	System.Random random = new System.Random();
                Transform res = escapeWaypoints[random.Next(0, escapeWaypoints.Length)];
                Debug.Log("escape called " + res.position);
                return res;
            }
            else{
            	// return random waypoint if escape waypoint is undefined
            	return RequestRandomWaypoint();
            }
        }


	    public Transform RequestWaypoint(int index){
	        if(index < waypoints.Length && index >= 0){
	        	return waypoints[index].transform;
	        }
	        else{
	        	return null;
	        }
	    }

	    public Transform RequestRandomWaypoint(){
	        if(waypoints.Length == 0){
                return null;
	        }
	        System.Random random = new System.Random();
            return waypoints[random.Next(0, waypoints.Length)].transform;
	    }

	    public int GetWaypointCount(){
	    	return waypoints.Length;
	    }


	}

}

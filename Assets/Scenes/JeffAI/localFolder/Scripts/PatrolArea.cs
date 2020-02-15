using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace JeffAI{

	public class PatrolArea : MonoBehaviour
	{
	   
	    public NavmeshWaypoint[] waypoints;

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

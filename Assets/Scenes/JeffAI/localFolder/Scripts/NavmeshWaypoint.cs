using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JeffAI{

	public class NavmeshWaypoint : MonoBehaviour
	{
	    // Start is called before the first frame update
	    void Start()
	    {
	        
	    }

	    // Update is called once per frame
	    void Update()
	    {
	        
	    }

	    
	    public virtual void OnDrawGizmos(){
	    	Gizmos.color = Color.red;
	    	Gizmos.DrawWireSphere(transform.position, 1f);
	    }


	}

}
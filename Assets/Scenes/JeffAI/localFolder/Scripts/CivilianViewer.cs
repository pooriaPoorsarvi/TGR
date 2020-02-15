using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JeffAI{

	public class CivilianViewer : AbstractViewer
	{ 

		public delegate void NoticedPlayerEventHandler();
        public static event NoticedPlayerEventHandler NoticedPlayer;

   	    protected override void FreakOut(GameObject player){
            var ai = gameObject.GetComponent<BasicAI>(); 

            if(ai != null){
                // check if event has subscribers
            	if (NoticedPlayer != null){
                    NoticedPlayer();
                    Debug.Log("Civilian spotted the player");
            	}

            }           
	    }

	    protected override void CalmDown(GameObject player){
	    }
	}

}
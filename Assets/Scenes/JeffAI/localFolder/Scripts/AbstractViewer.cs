using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace JeffAI{

	public abstract class AbstractViewer : MonoBehaviour
	{

        public float visionRadius;
        public float forwardRadiusOffset;
        public GameObject player;
        public String playerColliderTag;

        void Start(){
        	SphereCollider col = gameObject.AddComponent<SphereCollider>();
        	col.isTrigger = true;
        	col.center += new Vector3(0f, 0f, forwardRadiusOffset);
        	col.radius = visionRadius;
        }	   

        protected abstract void FreakOut(GameObject player);

        protected abstract void CalmDown(GameObject player);

        void OnTriggerEnter(Collider col){
            if(col.tag == playerColliderTag){
                FreakOut(col.gameObject);
            }
        }

	}


}
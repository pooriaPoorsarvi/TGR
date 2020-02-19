using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InteractableObjectManager : MonoBehaviour
{

    // interactable objects
    public List<InteractableObject> interactableObjects = new List<InteractableObject>();

    public GameObject mainCanvas;

    public GameObject sampleHint;
    private List<GameObject> uiPanels = new List<GameObject>();

    private bool setup = false;


    void Start(){
     
        for(int i = 0; i < interactableObjects.Count; i++){
            GameObject newPanel = (GameObject)Instantiate(sampleHint,
                                        sampleHint.transform.position,
                                        Quaternion.identity);

            newPanel.transform.parent = mainCanvas.transform;
            newPanel.active = true;

            // set sibling index of the panel to 0, so that it doesn't overlap with other ui elements
            newPanel.transform.SetSiblingIndex(0);

            newPanel.transform.position = sampleHint.transform.position;
            newPanel.transform.localScale = sampleHint.transform.localScale;
            newPanel.transform.rotation = sampleHint.transform.rotation;

            uiPanels.Add(newPanel);
        }

        setup = true;
    }

    void Update(){
        if(setup){
	        for(int i = 0; i < interactableObjects.Count; i++){
	            uiPanels[i].GetComponent<RectTransform>().localPosition = 
	            GetCanvasPosition(mainCanvas.GetComponent<RectTransform>(), Camera.main, interactableObjects[i].transform.position);

                if(interactableObjects[i].GetComponent<InteractableObject>().NeedToDisplay()){
                    uiPanels[i].active = true;
                
	                foreach(Transform child in uiPanels[i].transform){
	                    if(child.gameObject.name == "HintText"){
	                        child.gameObject.GetComponent<Text>().text = interactableObjects[i].GetComponent<InteractableObject>().GetHintText();
	                    }
	                }

                }    
                else{
                	uiPanels[i].active = false;
                }

	        }
        }
    }


    // Get canvas position of Vector3, which is the world position
    private Vector2 GetCanvasPosition(RectTransform canvas, Camera camera, Vector3 position) {
         Vector2 temp = camera.WorldToViewportPoint(position);
 
         temp.x *= canvas.sizeDelta.x;
         temp.y *= canvas.sizeDelta.y;
 
         temp.x -= canvas.sizeDelta.x * canvas.pivot.x;
         temp.y -= canvas.sizeDelta.y * canvas.pivot.y;
 
         return temp;
    }       



}

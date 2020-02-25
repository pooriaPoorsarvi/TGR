using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using JeffAI;

public class InterfaceManager : MonoBehaviour
{
    

    public List<GameObject> npcs;
    private List<GameObject> uiPanels = new List<GameObject>();

    public GameObject mainCanvas;
    public GameObject sampleNpcUi;

    private bool setup = false;

    void Start(){
     
        for(int i = 0; i < npcs.Count; i++){
            GameObject newPanel = (GameObject)Instantiate(sampleNpcUi,
                                        sampleNpcUi.transform.position,
                                        Quaternion.identity);

            newPanel.transform.parent = mainCanvas.transform;
            newPanel.active = true;

            // set sibling index of the panel to 0, so that it doesn't overlap with other ui elements
            newPanel.transform.SetSiblingIndex(0);

            newPanel.transform.position = sampleNpcUi.transform.position;
            newPanel.transform.localScale = sampleNpcUi.transform.localScale;
            newPanel.transform.rotation = sampleNpcUi.transform.rotation;

            uiPanels.Add(newPanel);
        }

        setup = true;
    }

    void Update(){
        if(setup){
	        for(int i = 0; i < npcs.Count; i++){
                
                if(npcs[i].GetComponent<BasicAI>().IsFreaky()){

                    uiPanels[i].active = true;

    	            uiPanels[i].GetComponent<RectTransform>().localPosition = 
    	            GetCanvasPosition(mainCanvas.GetComponent<RectTransform>(), Camera.main, npcs[i].transform.position);

                    foreach(Transform child in uiPanels[i].transform){
                        if(child.gameObject.name == "TimeText"){
                            child.gameObject.GetComponent<Text>().text = npcs[i].GetComponent<BasicAI>().GetTimerText();
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

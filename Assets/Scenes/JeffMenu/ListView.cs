using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using DesertCity;

public class ListView : MonoBehaviour
{

    public ButtonManager buttonManager;
    public GameObject newFilesMenu;
    private List<GameObject> newFilesUiRepr = new List<GameObject>();
    
    public GameObject sampleFileUi;

    public GameObject newFilesContent;
    public GameObject newFilesContentInner;
    private Vector2 newFilesContentSizeDeltaInitial = new Vector2(0f, 0f);
    private Vector3 newFilesContentInnerPositionInitial = new Vector3(0f, 0f, 0f);	

    public float GetWidth(){
    	return newFilesMenu.GetComponent<RectTransform>().rect.width;
    }

    public float GetHeight(){
    	return newFilesMenu.GetComponent<RectTransform>().rect.height;
    }

    void Start(){

    	newFilesMenu = GameObject.FindGameObjectWithTag("listMenu");

    	sampleFileUi = GameObject.FindGameObjectWithTag("listFileUi");

    	sampleFileUi.SetActive(false);

    	newFilesContent = GameObject.FindGameObjectWithTag("newListContent");
    	newFilesContentInner = GameObject.FindGameObjectWithTag("newListContentInner");  

    	newFilesMenu.SetActive(false);	

    }

    public List<GameObject> GetButtons(){
    	return newFilesUiRepr;
    }


    public void Deactivate(){

        if(newFilesUiRepr.Count != 0){
            foreach(GameObject g in newFilesUiRepr){
                g.GetComponent<CustomButton>().isActive = false;
            }
        }

	
    	newFilesMenu.SetActive(false);
    }
  

    public void SetPosition(Vector3 newPos){
        newFilesMenu.transform.position = newPos;
    }

    public void SetScale(GameObject initialButton){
        newFilesMenu.GetComponent<RectTransform>().sizeDelta = new Vector2(initialButton.GetComponent<RectTransform>().sizeDelta.x, 
            newFilesMenu.GetComponent<RectTransform>().sizeDelta.y);
    }

    public void GenerateListView(List<String> names, ButtonManager sideButtonManager=null){

        newFilesMenu.SetActive(true);
        List<GameObject> trashCan = new List<GameObject>();
        while(newFilesUiRepr.Count > 0){
            trashCan.Add(newFilesUiRepr[0]);
            newFilesUiRepr.RemoveAt(0);
        }
        for(int k = 0; k < trashCan.Count; k++){
            Destroy(trashCan[k]);
        }

        if(newFilesContentSizeDeltaInitial.x == 0 && newFilesContentSizeDeltaInitial.y == 0){
            newFilesContentSizeDeltaInitial = newFilesContent.GetComponent<RectTransform>().sizeDelta;
            newFilesContentInnerPositionInitial = newFilesContentInner.GetComponent<RectTransform>().localPosition;
        }
        else{
            newFilesContent.GetComponent<RectTransform>().sizeDelta = newFilesContentSizeDeltaInitial;
            newFilesContentInner.GetComponent<RectTransform>().localPosition = newFilesContentInnerPositionInitial;
        }

        for(int i = 0; i < names.Count; i++){
            GameObject newFile = UnityEngine.Object.Instantiate(sampleFileUi, new Vector3(0f, 0f, 0f), new Quaternion(0f, 0f, 0f, 1f));    

            newFile.transform.SetParent(sampleFileUi.transform.parent);
            newFile.GetComponent<RectTransform>().localScale = sampleFileUi.GetComponent<RectTransform>().localScale;
            newFile.GetComponent<RectTransform>().localRotation = sampleFileUi.GetComponent<RectTransform>().localRotation;
            newFile.SetActive(true);
            bool wasZero = false;
            if(newFilesUiRepr.Count == 0){
                // do nothing here   
                newFile.GetComponent<RectTransform>().localPosition = sampleFileUi.GetComponent<RectTransform>().localPosition;
                wasZero = true;
            }
            newFilesUiRepr.Add(newFile);

            newFile.GetComponent<RectTransform>().localPosition = new Vector3(sampleFileUi.GetComponent<RectTransform>().localPosition.x, sampleFileUi.GetComponent<RectTransform>().localPosition.y - 100f * newFilesUiRepr.Count + 100f, sampleFileUi.GetComponent<RectTransform>().localPosition.z);
            newFilesContent.GetComponent<RectTransform>().sizeDelta = new Vector2(newFilesContent.GetComponent<RectTransform>().sizeDelta.x, newFilesContent.GetComponent<RectTransform>().sizeDelta.y + 100f);
            newFilesContentInner.GetComponent<RectTransform>().localPosition += new Vector3(0f, 50f, 0f);                           
            
            if(sideButtonManager == null){
                newFile.GetComponent<CustomButton>().buttonManager = buttonManager;
            }
            else{
                newFile.GetComponent<CustomButton>().buttonManager = sideButtonManager;
            }
            newFile.GetComponent<CustomButton>().index = newFilesUiRepr.Count - 1;
            newFile.GetComponent<CustomButton>().Setup();
            newFile.GetComponent<CustomButton>().isActive = true;

            foreach(Transform child in newFile.transform){

                if(child.gameObject.tag == "uiName"){
                    child.gameObject.GetComponent<Text>().text = names[i].ToString();
                }
            }
        }
    }


}

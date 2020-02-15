using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class SliderMenu : ButtonManager
{   

    public GameObject newFilesMenu;
    private List<GameObject> newFilesUiRepr = new List<GameObject>();
    
    public GameObject sampleFileUi;
 
    public GameObject newFilesContentInner;
    private Vector3 newFilesContentInnerPositionInitial = new Vector3(0f, 0f, 0f);	

    private Vector3 minContentPosition = new Vector3(0f, 0f, 0f);
    private Vector3 maxContentPosition = new Vector3(0f, 0f, 0f);

    public GameObject startingPoint;

    public int offset;
    public int divMult;

    public bool unlockAll = false;

    public LevelInfo[] levels;
    public Slider slider;
    private bool setup = false;
    public LevelManager levelManager;

    public List<SliderMenuTransition> transitions = new List<SliderMenuTransition>();

    public override void ActOnButton(int index){
        if(levels[index].unlocked){
            levelManager.LoadLevel(levels[index].exactSceneName);
        }
        else{
            // lvl hasn't been unlocked yet
            PromptManager.SetSingleOptionMode();
            PromptManager.SetText("You haven't unlocked this level yet.");
            PromptManager.SetTitle("Locked");
            PromptManager.SetReply("OK");
            PromptManager.ShowPrompt();               
        }
    }

    public override void MouseEntered(int index){

    }

    public override void MouseExit(int index){

    }    

    public void Start(){
    	Refresh();
    	slider.onValueChanged.AddListener(UpdatePos);
    	LevelManager.SetLevelCount(levels.Length);

        curTransitionIndex = 0;
    }

    public int curTransitionIndex = 0;
    private IEnumerator coroutine = null;
    public Animator animator;

    public IEnumerator WaitForMovement(){
        animator.enabled = false;
        yield return new WaitForSeconds(0.5f);
        animator.enabled = true;
    }

    public void UpdatePos(float newValue){
        if(setup){

            if(coroutine != null){
                StopCoroutine(coroutine);
            }
            coroutine = WaitForMovement();
            StartCoroutine(coroutine);
 
            int index = 0;
            foreach(SliderMenuTransition smt in transitions){

                if(slider.value > smt.lowerBound && slider.value <= smt.upperBound && curTransitionIndex != index){
                    Camera.main.gameObject.transform.position = smt.position.position;
                    curTransitionIndex = index;
                    smt.lightSource.SetActive(true);
                    for(int j = 0; j < transitions.Count; j++){
                        if(j != index){
                            transitions[j].lightSource.SetActive(false);
                        }
                    }
                }

                index++;
            }

        	float distance = maxContentPosition.x - startingPoint.GetComponent<RectTransform>().localPosition.x;
            newFilesContentInner.GetComponent<RectTransform>().localPosition = new Vector3((startingPoint.GetComponent<RectTransform>().localPosition.x + 
            	distance * (newValue)), 
            	newFilesContentInner.GetComponent<RectTransform>().localPosition.y, newFilesContentInner.GetComponent<RectTransform>().localPosition.z);
        }
    }

    public void Refresh(){
    	LoadData();
    	List<String> names = new List<String>();
        foreach(LevelInfo l in levels){
        	names.Add(l.name);
        }
        GenerateSlideMenu(names);
    }

    private void LoadData(){
    	
    }

    public float maxScore;
    public Sprite lockedSprite;
    public Sprite unlockedSprite;

    private void SetUIScore(GameObject scoringPanel, List<float> scores, int childIndex){
        List<Image> stars = new List<Image>();

        Image lockc = null;
        foreach(Transform child in scoringPanel.transform){
            if(child.gameObject.name != "Locked"){       	
                stars.Add(child.gameObject.GetComponent<Image>());
                child.gameObject.GetComponent<Image>().color = Color.grey;
                child.gameObject.GetComponent<Image>().fillAmount = 1f;
            }
            else{
            	foreach(Transform child2 in child.gameObject.transform){
            		if(child2.gameObject.name == "lockIcon"){
                        lockc = child2.gameObject.GetComponent<Image>();
            		}
            	}
            }
        }
        float result = scores[childIndex];

        int index = 0;
        while(result >= 1f){
            stars[index].fillAmount = 1f;
            stars[index].color = Color.yellow;
            result -= 1f;
            index++;
        }

        if(index < stars.Count && result > 0f){
            stars[index].fillAmount = result;
            stars[index].color = Color.yellow;
        }
        else if(index < stars.Count && result == 0f){
            stars[index].fillAmount = 1f;
            stars[index].color = Color.grey;            
        }


        if(childIndex == 0){
            levels[childIndex].unlocked = true;
            lockc.sprite = unlockedSprite;    
            return;        
        }

        if(childIndex > 0 && scores[childIndex - 1] > 0f){
            levels[childIndex].unlocked = true;
            lockc.sprite = unlockedSprite;
        }
        else{
        	levels[childIndex].unlocked = false;
            lockc.sprite = lockedSprite;
        }

        if(unlockAll){
            levels[childIndex].unlocked = true;
            lockc.sprite = unlockedSprite;            
        }

    }

    public void GenerateSlideMenu(List<String> names){
    	
    	LevelManager.SetLevelCount(levels.Length);
    	List<float> scores = LevelManager.LoadLevelData();

        newFilesMenu.SetActive(true);
        List<GameObject> trashCan = new List<GameObject>();
        while(newFilesUiRepr.Count > 0){
            trashCan.Add(newFilesUiRepr[0]);
            newFilesUiRepr.RemoveAt(0);
        }
        for(int k = 0; k < trashCan.Count; k++){
            Destroy(trashCan[k]);
        }

        if(newFilesContentInnerPositionInitial.x == 0 && newFilesContentInnerPositionInitial.y == 0){
            newFilesContentInnerPositionInitial = newFilesContentInner.GetComponent<RectTransform>().localPosition;
        }
        else{
            newFilesContentInner.GetComponent<RectTransform>().localPosition = newFilesContentInnerPositionInitial;
        }

        for(int i = 0; i < names.Count; i++){
            GameObject newFile = UnityEngine.Object.Instantiate(sampleFileUi, new Vector3(0f, 0f, 0f), new Quaternion(0f, 0f, 0f, 1f));    

            newFile.transform.SetParent(sampleFileUi.transform.parent);
            newFile.GetComponent<RectTransform>().localScale = sampleFileUi.GetComponent<RectTransform>().localScale;
            newFile.GetComponent<RectTransform>().localRotation = sampleFileUi.GetComponent<RectTransform>().localRotation;
            newFile.SetActive(true);

            newFile.GetComponent<RectTransform>().localPosition = new Vector3(sampleFileUi.GetComponent<RectTransform>().localPosition.x + 400f * newFilesUiRepr.Count, sampleFileUi.GetComponent<RectTransform>().localPosition.y, sampleFileUi.GetComponent<RectTransform>().localPosition.z);
            newFilesContentInner.GetComponent<RectTransform>().localPosition -= new Vector3(400f, 0f, 0f);       

            if(newFilesUiRepr.Count == 0){
            	minContentPosition = newFilesContentInner.transform.localPosition;
            } 

            if(i == names.Count - 1){
            	maxContentPosition = newFilesContentInner.transform.localPosition;
            }

            newFilesUiRepr.Add(newFile);         
            
            newFile.GetComponent<CustomButton>().buttonManager = this;
            newFile.GetComponent<CustomButton>().index = newFilesUiRepr.Count - 1;
            newFile.GetComponent<CustomButton>().Setup();
            newFile.GetComponent<CustomButton>().isActive = true;

            foreach(Transform child in newFile.transform){

                if(child.gameObject.name == "levelName"){
                    child.gameObject.GetComponent<Text>().text = names[i].ToString();
                }
                else if(child.gameObject.name == "scoringPanel"){
                	SetUIScore(child.gameObject, scores, i);
                }
                else if(child.gameObject.name == "levelPic"){
                	child.gameObject.GetComponent<Image>().sprite = levels[i].img;
                }                
            }
        }
        setup = true;
        UpdatePos(0f);
    }

}


[System.Serializable]
public struct LevelInfo{
    public String name;
    public int levelIndex;
    public Sprite img;
    public float score;
    public bool unlocked;
    public String exactSceneName;
}

[System.Serializable]
public struct SliderMenuTransition{
    public Transform position;
    public float lowerBound;
    public float upperBound;
    public GameObject lightSource;
}
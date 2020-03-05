using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using UnityEngine.Events;

public class MainMenu : MonoBehaviour
{

    public void PlayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex -1);
    }

    public void QuitGame()
    {
        Debug.Log("QUIT!");
        Application.Quit();
    }

    public RectTransform mousePointer;
    public float speedX;
    public float speedY;
    private Vector3 move;
    private PlayerInputActions playerInputManager;
    public ButtonAction[] buttonActions;

    private bool ok = false;

    void SelectDown(){
        ok = true;
    }

    void SelectUp(){
        ok = false;
    }

    private void Awake()
    {
        playerInputManager = new PlayerInputActions();

        playerInputManager.PlayerAction.LS.performed += ctx => ProcessMovement(ctx.ReadValue<Vector2>());
        playerInputManager.PlayerAction.LS.canceled += ctx => ProcessMovement(Vector2.zero);

        playerInputManager.PlayerAction.LB.started += ctx => SelectDown();
        playerInputManager.PlayerAction.LB.canceled += ctx => SelectUp();
    }    

	private bool Overlaps(RectTransform rectTr1, RectTransform rectTr2)
	{
	    return (new Rect(rectTr1.localPosition.x, rectTr1.localPosition.y, rectTr1.rect.width, rectTr1.rect.height)).Overlaps(
	    	new Rect(rectTr2.localPosition.x, rectTr2.localPosition.y, rectTr2.rect.width, rectTr2.rect.height));
	}    
    
    void ProcessMovement(Vector2 currMovement)
    {
        move = new Vector3(currMovement.x * speedX, currMovement.y * speedY, 0);
        Debug.Log("curMovement is " + currMovement);
    }

    void Update()
    {
        mousePointer.transform.position += move;

        if(Input.GetKeyDown(KeyCode.W)){
        	mousePointer.transform.position += new Vector3(0f, 20f, 0f);
        }

        if(Input.GetKeyDown(KeyCode.S)){
        	mousePointer.transform.position += new Vector3(0f, -20f, 0f);
        }

        for(int i = 0; i < buttonActions.Length; i++){
        	if(Overlaps(buttonActions[i].button, mousePointer)){

        		buttonActions[i].button.GetComponent<Image>().enabled = true;

        		print("Overlaps");
        		if(ok){
                    buttonActions[i].invokeMethod.Invoke();
        		} 
        	}

        	else{

        		buttonActions[i].button.GetComponent<Image>().enabled = false;
        	}
        }

    }

}


[System.Serializable]
public struct ButtonAction{
    public UnityEvent invokeMethod;
    public RectTransform button;
}
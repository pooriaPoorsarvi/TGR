using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Events;
using UnityEngine.UI;

public class TimerSystem : MonoBehaviour
{

    // number of minutes this timer should work for
    public int mins;
    
    // text in the User interface that will display the amount of time left
    public Text timerText;

    public ScoringSystem scoringSystem;

    // event that will be raised when timer ends
    public delegate void TimerEndedEventHandler();
    public static event TimerEndedEventHandler TimerEnded;


    // Start is called before the first frame update
    void Start()
    {
        IEnumerator coroutine = Timer(mins);
        StartCoroutine(coroutine);
    }


    // for declaring behavior when timer ends
    private void EndTimer(){
    	// make scoring system calculate and save the score
        
        if (TimerEnded != null){
        	// check if there are subscribers
            TimerEnded();
        }
    }

    IEnumerator Timer(int mins)
    {
    	int counter = mins;
        while(true){
            yield return new WaitForSeconds(1f);
            counter--;

            String hourText = "";
            String minText = "";
            String secText = "";

            int hoursTimer = mins / 60;
            int minTimer = mins - hoursTimer * 60;
            int secTimer = counter;

            if(hoursTimer >= 10){
                hourText = hoursTimer.ToString();
            }
            else{
                hourText = "0" + hoursTimer.ToString();
            }
            if(minTimer >= 10){
                minText = minTimer.ToString();
            }
            else{
                minText = "0" + minTimer.ToString();
            }
            if(secTimer >= 10){
                secText = secTimer.ToString();
            }
            else{
                secText = "0" + secTimer.ToString();
            }   
            timerText.text = hourText + ":" + minText + ":" + secText; 

            if(mins == 0 && counter == 0){
                EndTimer();
                break;
            }

            if(counter == 0){
                mins -= 1;
                counter = 60;
            }            
        }
    }


}

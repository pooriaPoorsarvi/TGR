using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class ScoringSystem : MonoBehaviour
{

    // index of the current level
    public int levelIndex;

    public GameObject limbsLostText;
    public GameObject killsText;
    public GameObject timeElapsedText;
    public GameObject objectsUsedText;
    public GameObject timesFallenText;

    private static int limbsLost;
    private static int kills;
    private static int objectsUsed;
    private static int timesFallen;

    public int limbsUpperLimit;
    public int killsUpperLimit;
    public int objectsUsedUpperLimit;
    public int timesFallenUpperLimit;

    public GameObject finalGrade;
    
    public Text killCountWidget;

    public static void LimbLost(){
        limbsLost++;
    }

    public static void KillMade(){
        kills++;
    }

    public static void ObjectWasUsed(){
        objectsUsed++;
    }

    public static void FellDown(){
        timesFallen++;
    }

    private String AssignLetterGrade(float result){
        if(result >= 4.5f){
            return "A+";
        }
        else if(result >= 4.2f){
            return "A";
        }
        else if(result >= 4f){
            return "B+";
        }
        else if(result >= 3.7){
            return "B";
        }
        else if(result >= 3.2){
            return "B";
        }
        else if(result >= 3){
            return "B-";
        }
        else if(result >= 2.5){
            return "C+";
        }
        else if(result >= 2f){
            return "C";
        }
        else if(result >= 1.5f){
            return "C-";
        }
        else{
            return "F";
        }                                                
    }

    public void UpdateScore(int timeElapsed, int timeUpperLimit){
        limbsLostText.GetComponent<TextMeshProUGUI>().text = limbsLost + "/" + limbsUpperLimit;
        killsText.GetComponent<TextMeshProUGUI>().text = kills + "/" + killsUpperLimit;
        timeElapsedText.GetComponent<TextMeshProUGUI>().text = timeElapsed + "/" + timeUpperLimit;
        objectsUsedText.GetComponent<TextMeshProUGUI>().text = objectsUsed + "/" + objectsUsedUpperLimit;
        timesFallenText.GetComponent<TextMeshProUGUI>().text = timesFallen + "/" + timesFallenUpperLimit;

        float result = CalculateScore(timeElapsed, timeUpperLimit);
        String letterGrade = AssignLetterGrade(result);
        finalGrade.GetComponent<TextMeshProUGUI>().text = letterGrade;
    }



    // Calculate and return score for the level
    private float CalculateScore(int timeElapsed, int timeUpperLimit){
        return (limbsLost / (float)limbsUpperLimit + kills / (float)killsUpperLimit - 
             timeUpperLimit / (float)timeElapsed + objectsUsed / (float) objectsUsedUpperLimit + 
            timesFallen / (float)timesFallenUpperLimit);
    }

    // Uses PlayerPrefs to save data
    private void SaveScore(int curScore){
        PlayerPrefs.SetInt("level" + levelIndex, curScore);
	    PlayerPrefs.Save();

    }

    void Update(){
        killCountWidget.text = kills + "/" + killsUpperLimit;
    }


}

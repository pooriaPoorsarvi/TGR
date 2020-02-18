using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoringSystem : MonoBehaviour
{

    // index of the current level
    public int levelIndex;

    // Calculate and return score for the level
    private int CalculateScore(){
        return 0; // for simplicity
    }

    // added score for the current level
    private void SaveScore(){
  
        int curScore = CalculateScore();
        PlayerPrefs.SetInt("level" + levelIndex, curScore);
	    PlayerPrefs.Save();

    }

    void Start(){

        TimerSystem.TimerEnded += SaveScore;

    }


}

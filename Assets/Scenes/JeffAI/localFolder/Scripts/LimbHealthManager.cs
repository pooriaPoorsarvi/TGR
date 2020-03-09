using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class LimbHealthManager : MonoBehaviour
{

    public enum Limbs{
    	Lthigh, // 0
    	Rthigh, // 1 
    	Lleg, // 2
    	Rleg // 3
    }

    public LimbStats[] stats;

    public int subAmount = 100;

    public Color deadColor, aliveColor;

    void Awake(){
        for(int i = 0; i < stats.Length; i++){
	        if(stats[i].limbHealth <= 0){
	        	stats[i].limbTick.color = deadColor;
	        }
	        else{
	        	stats[i].limbTick.color = aliveColor;
	        }
        }    	
    }

    private LimbStats findLimbsStatsWithIndex(int index){
        for(int i = 0; i < stats.Length; i++){
        	if(stats[i].limbIndex == index){
        		return stats[i];
        	}
        }
        throw new Exception("limb data not provided");
    }

    private void UpdateUI(LimbStats limbStats){
        if(limbStats.limbHealth <= 0){
        	limbStats.limbTick.color = deadColor;
        }
        else{
        	limbStats.limbTick.color = aliveColor;
        }
    }

    public void SubLimbHealthAtIndex(int i){
        LimbStats limbStats = findLimbsStatsWithIndex(i);
    	limbStats.limbHealth -= subAmount;
    	if(limbStats.limbHealth < 0){
    		limbStats.limbHealth = 0;
    	}
    	UpdateUI(limbStats);
    }


}

[System.Serializable]
public struct LimbStats{
    public int limbIndex;
    public int limbHealth;
    public int healthInitial;
    public Image limbTick;
}
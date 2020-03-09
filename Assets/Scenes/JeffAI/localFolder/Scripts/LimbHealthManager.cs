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
    	LArm, // 2
    	RArm // 3
    }

    public LimbStats[] stats;

    public int subAmount = 100;

    public Color deadColor, aliveColor;

    public Sprite crossIcon;
    public Sprite tickIcon;

    void Awake(){
        for(int i = 0; i < stats.Length; i++){
	        if(stats[i].limbHealth <= 0){
	        	stats[i].limbTick.color = deadColor;
	        	stats[i].limbTick.sprite = crossIcon;
	        }
	        else{
	        	stats[i].limbTick.color = aliveColor;
	        	stats[i].limbTick.sprite = tickIcon;
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
        	limbStats.limbTick.sprite = crossIcon;
        }
        else{
        	limbStats.limbTick.color = aliveColor;
        	limbStats.limbTick.sprite = tickIcon;
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
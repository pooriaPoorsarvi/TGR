﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class timerDisplay : MonoBehaviour
{
    Image fillImg;
    public TextMeshProUGUI timeText;
    public float timeAmt=90;
    float time;

    public delegate void TimerEndedEventHandler();
    public static event TimerEndedEventHandler TimerEnded;

    // Start is called before the first frame update
    void Start()
    {
        fillImg = GetComponent<Image>();
        time = timeAmt;
    }

    public int GetTotalTime(){
        Debug.Log("jk1 " + (int)timeAmt);
        return (int)timeAmt;
    }

    public int GetTimeElapsed(){
        int num = (int)timeAmt - (int)time;
        Debug.Log("jk2 " + num);
        return (int)timeAmt - (int)time;
    }

    private bool gameOver = false;

    // Update is called once per frame
    void Update()
    {
        if (time > 0) {
            time -= Time.deltaTime;
            fillImg.fillAmount = time / timeAmt;
            timeText.text = time.ToString("F0");
        } 
        else if(!gameOver){
            time = 0;
            timeText.text = time.ToString("F0");
            TimerEnded();
            gameOver = true;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class timerDisplay : MonoBehaviour
{
    Image fillImg;
    public TextMeshProUGUI timeText;
    public DeathTimer DeathTimer;
    float time;

    public delegate void TimerEndedEventHandler();
    public static event TimerEndedEventHandler TimerEnded;

    // Start is called before the first frame update
    void Start()
    {
        fillImg = GetComponent<Image>();
        time = DeathTimer.time;
    }

    public int GetTotalTime(){
        return (int)DeathTimer.time;
    }

    public int GetTimeElapsed(){
        int num = GetTotalTime() - (int)time;
        return GetTotalTime() - (int)time;
    }

    private bool gameOver = false;

    // Update is called once per frame
    void Update()
    {
        if (time > 0) {
            time -= Time.deltaTime;
            fillImg.fillAmount = time / (float)GetTotalTime();
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

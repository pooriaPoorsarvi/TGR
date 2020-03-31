using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameplayManager : MonoBehaviour
{

    public LevelFinisher levelFinisher;

    // event that will be raised when game ends due to any reason except time out.
    public delegate void GameOverEventHandler();
    public static event GameOverEventHandler GameOver;

    // ui element that represents the game over screen
    public static GameObject gameOverScreen;

    public GameObject pauseMenu;

    public GameObject finalScoreScreen;

    public ScoringSystem scoringSystem;

    public timerDisplay td;

    // restart the game after player has lost
    public void Retry(){
        Application.LoadLevel(Application.loadedLevel);
    }
    
    public void PauseGame(){
    	Debug.Log("Game paused");
    	pauseMenu.active = true;
    }

    // continue game
    public void ContinueGame(){
        pauseMenu.active = false;
    }

    // quit game
    public void QuitGame(){
        Application.Quit();
    }

    void Start(){
        //gameOverScreen = GameObject.FindGameObjectWithTag("GameOverScreen");
        //gameOverScreen.active = false;

        // calculate final score when the timer ends
        timerDisplay.TimerEnded += LevelComplete;
    }

    public void LevelComplete(){
        Debug.Log("Level complete");
        finalScoreScreen.active = true;
        scoringSystem.UpdateScore(td.GetTimeElapsed(), td.GetTotalTime());
    }

    // public void LoseGame(){
    //     
    //     levelFinisher.FinishGame(false);
    //     //gameOverScreen.active = true;
    // }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameplayManager : MonoBehaviour
{

    // event that will be raised when game ends due to any reason except time out.
    public delegate void GameOverEventHandler();
    public static event GameOverEventHandler GameOver;

    // ui element that represents the game over screen
    public static GameObject gameOverScreen;

    public GameObject pauseMenu;

    public GameObject finalScoreScreen;

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
        timerDisplay.TimerEnded += CalculateScore;
    }

    public void CalculateScore(){
        Debug.Log("Game Over");
        finalScoreScreen.active = true;
    }

    public static void LoseGame(){
        
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        //gameOverScreen.active = true;
    }

}

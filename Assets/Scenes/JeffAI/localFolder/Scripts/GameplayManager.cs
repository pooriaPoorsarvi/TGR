using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameplayManager : MonoBehaviour
{

    // event that will be raised when game ends due to any reason except time out.
    public delegate void GameOverEventHandler();
    public static event GameOverEventHandler GameOver;

    // ui element that represents the game over screen
    public GameObject gameOverScreen;

    // restart the game after player has lost
    public void Retry(){
    
    }

}

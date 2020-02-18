using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameplayManager : MonoBehaviour
{

    // event that will be raised when game ends due to any reason except time out.
    public delegate void GameOverEventHandler();
    public static event GameOverEventHandler GameOver;

}

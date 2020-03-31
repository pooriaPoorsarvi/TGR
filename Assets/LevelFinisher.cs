using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelFinisher : MonoBehaviour
{
    public GameObject LevelFailed, LevelSuccess;
    private bool hasBeenCalledBefore = false;

    
    
    public void FinishGame(bool success)
    {
        FinishGame(success, true);
    }
    public void FinishGame(bool success, bool endMustBeShown)
    {
        if (hasBeenCalledBefore)
        {
            return;
        }
        hasBeenCalledBefore = true;

        if (!endMustBeShown)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            return;
        }

        if (success)
        {
            LevelSuccess.SetActive(true);
        }
        else
        {
            LevelFailed.SetActive(true);
        }
        StartCoroutine(EndGame());
    }

    private IEnumerator EndGame()
    {
        yield return new WaitForSeconds(3f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}

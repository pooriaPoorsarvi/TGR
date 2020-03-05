using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFinder : MonoBehaviour
{
    
    public delegate void NoticedPlayerEventHandler();
    public event NoticedPlayerEventHandler NoticedPlayer;
    
    
    public delegate void StopScarceHandler();
    public event StopScarceHandler StopScarce;


    public float coolDownTime = 1f;

    private static bool hasBeenSeen;
   

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (NoticedPlayer != null)
            {
                NoticedPlayer();
                hasBeenSeen = true;
                StartCoroutine(StartCoolDown());
            }
        }
    }

    IEnumerator StartCoolDown()
    {
        yield return new WaitForSeconds(coolDownTime);
        hasBeenSeen = false;
        StopScarce();
    }
}

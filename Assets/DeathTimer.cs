using System;
using System.Collections;
using System.Collections.Generic;
using RootMotion.Dynamics;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class DeathTimer : MonoBehaviour
{
    public float time;
    public PuppetMaster puppetMaster;
    public GameObject[] to_be_removed;
    public BehaviourPuppet behaviourPuppet;

    private PlayerInputActions playerInputActions;

    private void Awake()
    {
        playerInputActions = new PlayerInputActions();
        playerInputActions.PlayerAction.KillSwitch.performed += evn=> Restart();
    }

    void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public float timeToWeightBeforeEndingGame = 3f;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(StartTimer());
    }
    
    IEnumerator StartTimer()
    {
        yield return new WaitForSeconds(time);

        foreach (var obj in to_be_removed)
        {
            MuscleCollisionBroadcaster to_remove = obj.GetComponent<MuscleCollisionBroadcaster>();
            if (to_remove == null)
            {
                continue;
            }

            to_remove.puppetMaster.DisconnectMuscleRecursive(to_remove.muscleIndex, MuscleDisconnectMode.Explode);
            to_remove.Hit(10f, to_remove.gameObject.transform.up * 10f, to_remove.gameObject.transform.position);
        }

        behaviourPuppet.canGetUp = false;
        behaviourPuppet.unpinnedMuscleKnockout = true;
        StartCoroutine(EndGameAfterDeathEffect());
    }

    IEnumerator EndGameAfterDeathEffect()
    {
        yield return new WaitForSeconds(timeToWeightBeforeEndingGame);
        GameplayManager.LoseGame();
    }

    private void OnEnable()
    {
        playerInputActions.Enable();
    }

    private void OnDisable()
    {
        playerInputActions.Disable();
    }
}
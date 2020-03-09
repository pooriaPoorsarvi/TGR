using System;
using System.Collections;
using System.Collections.Generic;
using RootMotion.Dynamics;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.Events;


public class DeathTimer : MonoBehaviour
{
    public float time;
    public PuppetMaster puppetMaster;
    public GameObject[] to_be_removed;
    public BehaviourPuppet behaviourPuppet;

    private PlayerInputActions playerInputActions;

    public Limb[] limbs;

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

            for(int i = 0; i < limbs.Length; i++){
                if( ((GameObject)obj).name == limbs[i].limbName){
                    limbs[i].limbDeactivateAction.Invoke();
                }
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




[System.Serializable]
public struct Limb{
    public UnityEvent limbDeactivateAction;
    public String limbName;
}
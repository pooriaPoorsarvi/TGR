using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandPublisher : MonoBehaviour
{
    public Graber grabberL;
    public Graber grabberR;

    public bool isBeingHeld(GameObject gameObject)
    {
        return (!grabberL.getHoldingGameObject() == null && grabberL.getHoldingGameObject().Equals(gameObject)) ||
               (!grabberR.getHoldingGameObject() == null && grabberR.getHoldingGameObject().Equals(gameObject));
    }
}
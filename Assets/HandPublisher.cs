using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandPublisher : MonoBehaviour
{
    public Graber grabberL;
    public Graber grabberR;

    public bool isBeingHeld(GameObject gameObject)
    {
        return checkEachHand(grabberL, gameObject, "Left hand : ") ||
               checkEachHand(grabberR, gameObject, "Right hand : ");
    }

    bool checkEachHand(Graber graber, GameObject gameObject, String db)
    {
        if (graber.getHoldingGameObject() != null)
        {
            if (graber.getHoldingGameObject().Equals(gameObject))
            {
                return true;
            }
        }

        return false;
    }
}
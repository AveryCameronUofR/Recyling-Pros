using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;
using Valve.VR;

public class broadcastHeldObjInHand : MonoBehaviour
{
    public Hand hand;

    private GameObject itemInHand;

    private void Update()
    {
        if (hand.currentAttachedObject != null)
        {
            if (hand.currentAttachedObject != itemInHand)
            {
                itemInHand = hand.currentAttachedObject;
            }
        }
        else
        {
            itemInHand = null;
        }
    }

    public GameObject getItemInHand()
    {
        return itemInHand;
    }
}

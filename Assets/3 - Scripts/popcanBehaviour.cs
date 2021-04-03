using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;
using Valve.VR;

public class popcanBehaviour : MonoBehaviour
{
    private Hand myHand;

    private void Update()
    {
        if (myHand != null)
        {
            Debug.Log("MY HAND: " + myHand.ToString());
            SteamVR_TrackedObject obj = myHand.GetComponent<SteamVR_TrackedObject>();
            Debug.Log("MY INDEX: " + obj.index);
        }
    }

    private void OnCollisionEnter(Collision col)
    {
        myHand = col.gameObject.GetComponent<Hand>();
        // Debug.Log("MY HAND: " + myHand.ToString());
    }

    private void OnCollisionExit(Collision col)
    {
        //myHand = null;
    }
}

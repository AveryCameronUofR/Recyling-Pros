using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

public class buttonEvent : MonoBehaviour
{
    public void OnPress(Hand hand)
    {
        GameManager.gm.ExitIdle();
    }

    // public void OnCustomButtonPress()
    // {
    //     Debug.Log("We pushed our custom button!");
    // }
}

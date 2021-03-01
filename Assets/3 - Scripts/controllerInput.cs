using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Valve.VR;
using Valve.VR.InteractionSystem;

public class controllerInput : MonoBehaviour
{
    void Start()
    {
        
    }

    void Update()
    {
        bool state = SteamVR_Input.GetState("InteractUI", SteamVR_Input_Sources.RightHand);

        if (state == true)
        {
            if (EventSystem.current.currentSelectedGameObject != null)
            {
                ExecuteEvents.Execute(EventSystem.current.currentSelectedGameObject, new BaseEventData(EventSystem.current), ExecuteEvents.submitHandler);
            }
        }
    }
}

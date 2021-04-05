using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Valve.VR;
using Valve.VR.InteractionSystem;

public class controllerInput : MonoBehaviour
{
    public Player player;
    public float playerHeight = 1.5f;

    private playerStateController pStateController;

    private string selectedHand = "right";
    private bool selectedTriggerState;
    private GameObject itemAttached;
    private bool rTriggerState;
    private bool lTriggerState;
    private bool x_ButtonState;

    private delegate void HandleItemActions();

    void Start() 
    {
        pStateController = player.GetComponent<playerStateController>();
    }

    void Update()
    {
        rTriggerState = SteamVR_Input.GetState("InteractUI", SteamVR_Input_Sources.RightHand);
        lTriggerState = SteamVR_Input.GetState("InteractUI", SteamVR_Input_Sources.LeftHand);

        if (lTriggerState && selectedHand == "right")
        {
            selectedHand = "left";
            pStateController.SelectHand("left");
        } else if (rTriggerState && selectedHand == "left")
        {
            selectedHand = "right";
            pStateController.SelectHand("right");
        }

        selectedTriggerState = (selectedHand == "right") ? rTriggerState : lTriggerState;
        if (selectedTriggerState == true)
        {
            if (EventSystem.current.currentSelectedGameObject != null)
            {
                ExecuteEvents.Execute(EventSystem.current.currentSelectedGameObject, new BaseEventData(EventSystem.current), ExecuteEvents.submitHandler);
            }
        }

        x_ButtonState = SteamVR_Input.GetState("ResetHeight", SteamVR_Input_Sources.LeftHand);
        if (x_ButtonState == true)
            player.transform.localScale = Vector3.one * (playerHeight / player.eyeHeight);

        if (itemAttached && itemAttached.CompareTag("popcan"))
        {
            popcanBehaviour pb = itemAttached.GetComponent<popcanBehaviour>();

            if (gameObject.GetComponent<Hand>().currentAttachedObject == null)
            {
                pb.inHand = false;
                pb.ResetMaterial();
            }
            else
            {
                pb.inHand = true;
            }
        }
        itemAttached = gameObject.GetComponent<Hand>().currentAttachedObject;

        if (itemAttached && itemAttached.CompareTag("spray"))
        {
            sprayCan sc = itemAttached.GetComponent<sprayCan>();

            if (gameObject.GetComponent<Hand>().currentAttachedObject == null)
            {
                sc.inHand = false;
            }
            else
            {
                sc.inHand = true;
            }

            sc.TriggerState(rTriggerState);
        }
        itemAttached = gameObject.GetComponent<Hand>().currentAttachedObject;
    }
}

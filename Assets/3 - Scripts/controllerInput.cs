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

    private Hand hand;
    private string selectedHand = "right";
    private bool selectedTriggerState;
    private GameObject itemAttached;
    private bool rTriggerState;
    private bool lTriggerState;
    private bool x_ButtonState;
    private bool pauseButtonState;

    private bool initialUpdate;
    private float initialTimer = 1.5f;

    private delegate void HandleItemActions();

    void Start() 
    {
        pStateController = player.GetComponent<playerStateController>();
        hand = gameObject.GetComponent<Hand>();
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

            if (hand.currentAttachedObject == null)
            {
                pb.inHand = false;
                pb.ResetMaterial();
            }
            else
            {
                pb.inHand = true;
            }
        }

        if (itemAttached && itemAttached.CompareTag("bomb"))
        {
            bombBehaviour bb = itemAttached.GetComponent<bombBehaviour>();

            if (hand.currentAttachedObject == null)
            {
                bb.inHand = false;
            }
            else
            {
                bb.hand = hand;
                bb.inHand = true;
                bb.triggerPressed = rTriggerState;
            }
        }
        //itemAttached = hand.currentAttachedObject;

        if (itemAttached && itemAttached.CompareTag("spray"))
        {
            sprayCan sc = itemAttached.GetComponent<sprayCan>();

            sc.inHand = false;

            if (gameObject.GetComponent<Hand>().currentAttachedObject != null)
            {
                sc.inHand = true;
            }

            sc.TriggerState(rTriggerState);
        }

        if (itemAttached && itemAttached.CompareTag("fence"))
        {
            fenceTool fence = itemAttached.GetComponent<fenceTool>();

            if (gameObject.GetComponent<Hand>().currentAttachedObject == null)
            {
                fence.inHand = false;
            }
            else
            {
                fence.inHand = true;
            }
        }
        itemAttached = gameObject.GetComponent<Hand>().currentAttachedObject;

        pauseButtonState = SteamVR_Input.GetState("Pause", SteamVR_Input_Sources.LeftHand);
        if (pauseButtonState == true) {
            if (GameManager.gm.paused)
            {
                GameManager.gm.paused = false;
                GameManager.gm.UnPaused();
            }
            else
            {
                GameManager.gm.paused = true;
                GameManager.gm.Paused();
            }
        }
    }
}

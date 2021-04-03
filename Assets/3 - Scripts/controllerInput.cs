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

    private GameObject itemAttached;

    void Update()
    {
        bool rTriggerState = SteamVR_Input.GetState("InteractUI", SteamVR_Input_Sources.RightHand);
        if (rTriggerState == true)
        {
            if (EventSystem.current.currentSelectedGameObject != null)
            {
                ExecuteEvents.Execute(EventSystem.current.currentSelectedGameObject, new BaseEventData(EventSystem.current), ExecuteEvents.submitHandler);
            }
        }

        bool bButtonState = SteamVR_Input.GetState("ResetHeight", SteamVR_Input_Sources.LeftHand);
        if (bButtonState == true)
        {
            float tempPlayerHeight = player.eyeHeight;
            player.transform.localScale = Vector3.one * (playerHeight / tempPlayerHeight);
        }

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
    }
}

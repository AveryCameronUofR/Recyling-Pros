using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;
using Valve.VR;

// CREDIT FOR THIS SOLUTION GOES TO: https://answers.unity.com/questions/1693142/steamvr-20-how-to-implement-distance-grab.html

public class rangedGrab : MonoBehaviour
{
    public Transform pointer;
    public LayerMask rangedGrabable;

    Hand hand;
    bool isAttached = false;
    GameObject attachedObject = null;
    onHover script;

    void Start()
    {
        hand = gameObject.GetComponent<Hand>();
    }

    void Update()
    {
        RaycastHit hit;
        if (Physics.Raycast(pointer.position, pointer.forward, out hit, 10f, rangedGrabable) && hand.currentAttachedObject == null)
        {
            Interactable interactable = hit.collider.gameObject.GetComponent<Interactable>();
            SteamVR_Input_Sources source = hand.handType;

            // if pressing grip and trigger...
            if (hand.grabGripAction[source].state == true && hand.grabPinchAction[source].state == true)
            {
                if (interactable != null)
                {
                    interactable.transform.LookAt(transform);
                    interactable.gameObject.GetComponent<Rigidbody>().AddRelativeForce(Vector3.forward * 500, ForceMode.Force);
                    attachedObject = interactable.gameObject;
                    isAttached = true;
                }
            }

            script = hit.collider.gameObject.GetComponentInChildren<onHover>();
            
            if (script != null)
                script.gameObject.GetComponent<MeshRenderer>().enabled = true;
        }
        else
        {   
            if (script != null)
                script.gameObject.GetComponent<MeshRenderer>().enabled = false;
        }
    }

    private void LateUpdate()
    {
        if (isAttached)
        {
            hand.AttachObject(attachedObject, GrabTypes.Grip);
            //attachedObject = null;
            isAttached = false;
        }
    }
}
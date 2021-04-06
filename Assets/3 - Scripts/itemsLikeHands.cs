using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

public class itemsLikeHands : MonoBehaviour
{
    private GameObject obj;
    private timedObjectDestroyer timedDestroy;

    private void Start()
    {
        obj = gameObject.GetComponent<Hand>().currentAttachedObject;
    }

    private void Update()
    {
        if (obj != gameObject.GetComponent<Hand>().currentAttachedObject)
        {
            obj = gameObject.GetComponent<Hand>().currentAttachedObject;

            if (obj != null)
            {
                timedDestroy = obj.GetComponent<timedObjectDestroyer>();
                
                if (timedDestroy != null) 
                {
                    if (timedDestroy.isDying())
                    {
                        timedDestroy.SaveMe();
                    }
                }     
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class destroyItems : MonoBehaviour
{
    void OnTriggerEnter(Collider col)
    {
        var obj = col.gameObject.GetComponent<Valve.VR.InteractionSystem.Interactable>();

        if (obj)
        {
            GameManager.gm.ItemMissed();
            Destroy(col.gameObject);
        }
    }
}

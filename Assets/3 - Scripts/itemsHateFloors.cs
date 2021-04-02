using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class itemsHateFloors : MonoBehaviour
{
    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.CompareTag("badItem") || col.gameObject.CompareTag("goodItem"))
        {
            timedObjectDestroyer timedDestory = col.gameObject.GetComponent<timedObjectDestroyer>();
            timedDestory.KillMe();
        }
    }
}

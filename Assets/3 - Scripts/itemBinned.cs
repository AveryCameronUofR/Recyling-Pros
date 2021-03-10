using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class itemBinned : MonoBehaviour
{
    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.CompareTag("grabbable"))
        {
            GameManager.gm.ItemBinned();
            Destroy(col.gameObject);
        }
    }
}

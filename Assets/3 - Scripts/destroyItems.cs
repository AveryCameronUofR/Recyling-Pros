using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class destroyItems : MonoBehaviour
{
    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.CompareTag("grabbable"))
        {
            GameManager.gm.ItemMissed();
            Destroy(col.gameObject);
        }
    }
}

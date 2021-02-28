using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class itemBinned : MonoBehaviour
{
    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "grabbable")
        {
            GameManager.gm.itemBinned();
            Debug.Log("TEST");
            Destroy(col.gameObject);
        }
    }
}

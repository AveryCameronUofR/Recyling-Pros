using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class destroyItems : MonoBehaviour
{
    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "grabbable")
        {
            Destroy(col.gameObject);
        }
    }
}

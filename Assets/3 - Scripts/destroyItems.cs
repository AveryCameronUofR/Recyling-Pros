﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class destroyItems : MonoBehaviour
{
    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "grabbable")
        {
            GameManager.gm.itemMissed();
            Destroy(col.gameObject);
        }
    }
}

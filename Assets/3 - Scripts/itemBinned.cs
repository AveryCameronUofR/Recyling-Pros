using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class itemBinned : MonoBehaviour
{
    void OnTriggerEnter(Collider col)
    {
        if (gameObject.CompareTag(col.gameObject.tag))
        {
            GameManager.gm.IncreaseScore();
            Destroy(col.gameObject);
        }
        else
        {
            GameManager.gm.DecreaseScore();
            Destroy(col.gameObject);
        }
    }
}

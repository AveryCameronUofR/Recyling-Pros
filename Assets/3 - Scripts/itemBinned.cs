using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class itemBinned : MonoBehaviour
{
    void OnTriggerEnter(Collider col)
    {
        if (gameObject.CompareTag("goodItem"))
        {
            if (GameManager.gm.goodItems.Contains(col.gameObject.tag))
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
        else
        {
            if (GameManager.gm.badItems.Contains(col.gameObject.tag))
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
}

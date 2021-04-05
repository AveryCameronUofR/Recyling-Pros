using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System.Linq;

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
            else if (GameManager.gm.badItems.Contains(col.gameObject.tag))
            {
                GameManager.gm.DecreaseScore();
                Destroy(col.gameObject);
            }
        }
        else if (gameObject.CompareTag("badItem"))
        {
            if (GameManager.gm.badItems.Contains(col.gameObject.tag))
            {
                GameManager.gm.IncreaseScore();
                Destroy(col.gameObject);
            }
            else if (GameManager.gm.goodItems.Contains(col.gameObject.tag))
            {
                GameManager.gm.DecreaseScore();
                Destroy(col.gameObject);
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class itemsHateFloors : MonoBehaviour
{
    void OnCollisionEnter(Collision col)
    {
        var allList = GameManager.gm.goodItems.Concat(GameManager.gm.badItems);

        if (allList.Contains(col.gameObject.tag))
        {
            timedObjectDestroyer timedDestory = col.gameObject.GetComponent<timedObjectDestroyer>();
            timedDestory.KillMe();
        }
    }
}

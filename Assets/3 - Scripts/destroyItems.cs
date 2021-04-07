using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System.Linq;

public class destroyItems : MonoBehaviour
{
    private List<string> allList;

    private void Update()
    {
        if (allList == null)
            allList = GameManager.gm.goodItems.Concat(GameManager.gm.badItems).ToList();
    }

    void OnTriggerEnter(Collider col)
    {
        if (allList != null)
        {
            if (allList.Contains(col.gameObject.tag))
            {
                GameManager.gm.ItemMissed();
                Destroy(col.gameObject);
            }

            if (col.gameObject.layer == 9)
            {
                Destroy(col.gameObject);
            }
        }
    }
}

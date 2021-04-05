using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class itemsHateFloors : MonoBehaviour
{
    private bool destroying;

    void OnCollisionEnter(Collision col)
    {
        var allList = GameManager.gm.goodItems.Concat(GameManager.gm.badItems);

        if (allList.Contains(col.gameObject.tag))
        {
            if (col.gameObject.CompareTag("jug"))
            {
                if (!destroying)
                {
                    StartCoroutine(DestroyJug(col.gameObject, 0.1f));
                }
            }
            else
            {
                timedObjectDestroyer timedDestory = col.gameObject.GetComponent<timedObjectDestroyer>();
                timedDestory.KillMe();
            }
        }
    }

    IEnumerator DestroyJug(GameObject obj, float delay)
    {
        destroying = true;
        yield return new WaitForSeconds(delay);
        GameManager.gm.DecreaseScore();
        Destroy(obj);
        destroying = false;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class explodeInRadius : MonoBehaviour
{
    private List<Collider> itemsToExplode = new List<Collider>();

    public void DestroyItems()
    {
        foreach (Collider c in itemsToExplode)
        {
            Destroy(c.gameObject);
            GameManager.gm.IncreaseScore(2);
        }
    }

    private void OnTriggerEnter(Collider col)
    {
        if (GameManager.gm.goodItems.Contains(col.tag) || GameManager.gm.badItems.Contains(col.tag))
        {
            itemsToExplode.Add(col);
        }
    }

    private void OnTriggerExit(Collider col)
    {
        if (GameManager.gm.goodItems.Contains(col.tag) || GameManager.gm.badItems.Contains(col.tag))
        {
            itemsToExplode.Remove(col);
        }
    }
}

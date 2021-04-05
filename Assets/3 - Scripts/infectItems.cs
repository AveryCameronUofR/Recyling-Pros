using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class infectItems : MonoBehaviour
{
    public GameObject parentHazardJug;

    private List<string> allList;

    void Update()
    {
        if (allList == null)
            allList = GameManager.gm.goodItems.Concat(GameManager.gm.badItems).ToList();
    }

    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log(allList.Count.ToString());
        //if (allList.Contains(other.gameObject.tag))
        //{
        //    InfectOther(other.gameObject);
        //}
    }

    private void InfectOther(GameObject objToInfect)
    {
        objToInfect.layer = 10; //change layer to infected layer
    }
}

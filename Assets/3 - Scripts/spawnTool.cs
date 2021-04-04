using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawnTool : MonoBehaviour
{
    public GameObject toolToSpawn;

    private bool itemSpawned;

    private void Start()
    {
        itemSpawned = false;
        CreateATool();
    }

    private void OnTriggerExit(Collider other)
    {
        if (!itemSpawned)
        {
            Invoke("CreateATool", 1.0f);
            itemSpawned = true;
        }
    }

    private void CreateATool()
    {
        Vector3 spawnLoc = new Vector3(gameObject.transform.position.x - 0.3f, 
                                        gameObject.transform.position.y, 
                                        gameObject.transform.position.z - 0.25f);

        GameObject spawnedTool = Instantiate(toolToSpawn, spawnLoc, Quaternion.identity, gameObject.transform);
        spawnedTool.tag = toolToSpawn.gameObject.tag;
        spawnedTool.transform.parent = gameObject.transform;

        itemSpawned = false;
    }
}

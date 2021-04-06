using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class toolSpawnMaster : MonoBehaviour
{
    public static toolSpawnMaster toolMaster;

    private List<GameObject> spawnedToolsList = new List<GameObject>();

    private void Start()
    {
        if (toolMaster == null)
            toolMaster = this.gameObject.GetComponent<toolSpawnMaster>();
    }

    public void RegisterToolAsSpawned(GameObject toolToAdd)
    {
        spawnedToolsList.Add(toolToAdd);
    }

    public int AmountOfSpawnedTools()
    {
        return spawnedToolsList.Count;
    }

    public void WipeTools()
    {
        foreach (GameObject tool in spawnedToolsList)
        {
            Destroy(tool);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static GameManager;

public class spawnTool : MonoBehaviour
{
    public GameObject toolToSpawn;
    public Text toolCountDisplay;
    //public toolSpawnMaster toolMaster;

    private WaveMap waveMap;
    private int numItemsForRound;
    private bool firstItemSpawned = false;

    private bool itemSpawned;

    private void Update()
    {
        if (gm.currState.Equals(GameStates.Playing) && !firstItemSpawned)
        {
            // if the number of tools to spawn for this round is not zero
                // spawn a tool
                // decrement numItemsForRound
            if (numItemsForRound > 0)
            {
                CreateATool();
                numItemsForRound--;
                toolCountDisplay.text = GetItemCountString(numItemsForRound);
                firstItemSpawned = true;
            }
            toolCountDisplay.text = GetItemCountString(numItemsForRound);
        }
        else if (gm.currState.Equals(GameStates.Priming) || gm.currState.Equals(GameStates.Idle))
        {
            if (waveMap != gm.currWaveMap && gm.currWaveMap != null)
                waveMap = gm.currWaveMap;

            if (waveMap != null)
            {
                numItemsForRound = GetNumItemsForRound();
                toolCountDisplay.text = "";
                firstItemSpawned = false;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (!itemSpawned && numItemsForRound > 0 && other.gameObject.layer == 9)
        {
            Invoke("CreateATool", 1.0f);
            itemSpawned = true;
            numItemsForRound--;
            toolCountDisplay.text = GetItemCountString(numItemsForRound);
        }
    }

    private void CreateATool()
    {
        Vector3 spawnLoc = new Vector3(
            gameObject.transform.position.x - 0.3f, 
            gameObject.transform.position.y, 
            gameObject.transform.position.z - 0.25f
        );

        GameObject spawnedTool = Instantiate(toolToSpawn, spawnLoc, toolToSpawn.transform.rotation, gameObject.transform);
        
        toolSpawnMaster.toolMaster.RegisterToolAsSpawned(spawnedTool);
        
        spawnedTool.tag = toolToSpawn.gameObject.tag;
        spawnedTool.transform.parent = gameObject.transform;

        itemSpawned = false;
    }

    private int GetNumItemsForRound()
    {
        switch(toolToSpawn.tag) {
            case "bomb":
                return waveMap.num_bombs;
            case "spray":
                return waveMap.num_spray_cans;
            case "fence":
                return waveMap.num_fences;
            default:
                return 0;
        }
    }

    private string GetItemCountString(int numItemsForRound)
    {
        return numItemsForRound.ToString() + " left";
    }
}

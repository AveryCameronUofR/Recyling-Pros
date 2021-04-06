using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static GameManager;

public class spawnItems : MonoBehaviour
{
    public GameObject[] recyclables;
    public GameObject[] contaminants;
    public GameObject popcan;
    public GameObject jug;

    private WaveMap waveMap;
    private Queue<GameObject> itemQueue = new Queue<GameObject>();
    private List<float> itemDelays = new List<float>();

    private float currTime = 0.0f;
    private int numItemsLeft = 0;
    private int itemIndex = 0;
    private float conveyorWidth = 0.6f;

    private void Update()
    {
        if (gm.currState.Equals(GameStates.Playing))
        {
            currTime -= Time.deltaTime;

            numItemsLeft = gameObject.transform.childCount;

            if (numItemsLeft == 0 && itemQueue.Count == 0)
            {
                gm.WaveComplete();
                itemIndex = 0;
                return;
            }

            // if currTime < 0, pop item off queue and spawn
            if (currTime <= 0 && itemQueue.Count != 0)
            {
                var itemToSpawn = itemQueue.Dequeue();

                GameObject spawnedItem = Instantiate(itemToSpawn, gameObject.transform);
                spawnedItem.transform.parent = gameObject.transform;
                spawnedItem.transform.position += Vector3.left * RandomDisplacement();
                currTime = itemDelays.ElementAt(++itemIndex);
            }

            
        }
        else if (gm.currState.Equals(GameStates.Priming) || gm.currState.Equals(GameStates.Idle))
        {
            if (waveMap != gm.currWaveMap && gm.currWaveMap != null)
                waveMap = gm.currWaveMap;

            if (itemQueue.Count == 0 && waveMap != null)
            {
                CreateItemQueue(waveMap);
                currTime = itemDelays.ElementAt(itemIndex);
            }
        }
    }

    private void CreateItemQueue(WaveMap waveMap)
    {
        List<Item> q_items = waveMap.items_to_spawn.ToList();

        foreach (Item item in q_items)
        {
            GameObject objectToAdd = CreateObjectByName(item.name);
            itemQueue.Enqueue(objectToAdd);
            itemDelays.Add(item.delay);
        }
        // add one extra delay for after the last item
        itemDelays.Add(0);
    }

    private GameObject CreateObjectByName(string name)
    {
        switch(name) {
            case "contaminant":
                return GenerateRandomContaminant();
            case "recyclable":
                return GenerateRandomRecyclable();
            case "popcan":
                return popcan;
            case "jug":
                return jug;
            default:
                Debug.Log("trying to create a " + name);
                return null;
        }
    }

    private GameObject GenerateRandomContaminant()
    {
        int rng = Random.Range(0, contaminants.Length);
        return contaminants.ElementAt(rng);
    }

    private GameObject GenerateRandomRecyclable()
    {
        int rng = Random.Range(0, recyclables.Length);
        return recyclables.ElementAt(rng);
    }

    // random displacement for items on the conveyor
    private float RandomDisplacement()
    {
        return (Random.value * conveyorWidth) - (0.5f * conveyorWidth);
    }
}



using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static GameManager;

public class spawnItems : MonoBehaviour
{
    public float timeToSpawn = 2.0f;
    public GameObject[] recyclables;
    public GameObject[] contaminants;

    private WaveMap waveMap;
    private Queue<GameObject> itemQueue = new Queue<GameObject>();
    private bool isPlaying = false;
    private float currTime = 0.0f;
    private int numItemsLeft = 0;

    private void Start()
    {
        currTime = timeToSpawn;
    }

    private void Update()
    {
        if (gm.currState.Equals(GameStates.Playing))
        {
            currTime -= Time.deltaTime;
            numItemsLeft = gameObject.transform.childCount;

            //if currTime < 0, pop item off queue and spawn
            if (currTime <= 0 && itemQueue.Count != 0)
            {
                var itemToSpawn = itemQueue.Dequeue();

                GameObject spawnedItem = Instantiate(itemToSpawn, gameObject.transform);
                spawnedItem.transform.parent = gameObject.transform;

                currTime = timeToSpawn;
            }

            if (numItemsLeft == 0 && itemQueue.Count == 0)
            {
                gm.WaveComplete();
            }
        }
        else if (gm.currState.Equals(GameStates.Priming) || gm.currState.Equals(GameStates.Idle))
        {
            if (waveMap != gm.currWaveMap && gm.currWaveMap != null)
                waveMap = gm.currWaveMap;

            if (itemQueue.Count == 0 && waveMap != null)
            {
                CreateItemQueue(waveMap);
            }
        }
    }

    private void CreateItemQueue(WaveMap waveMap)
    {
        var rand_recycle = GenerateRandomRecyclables(waveMap.num_recycle);
        var rand_contam = GenerateRandomContaminants(waveMap.num_contam);
        List<GameObject> q_items = rand_recycle.Concat(rand_contam).ToList();

        Shuffle(q_items);

        foreach (GameObject item in q_items)
        {
            itemQueue.Enqueue(item);
        }
    }

    private List<GameObject> GenerateRandomRecyclables(int amount)
    {
        List<GameObject> rand_recycle = new List<GameObject>();

        for (int i = 1; i <= amount; i++)
        {
            int rng = Random.Range(0, recyclables.Length - 1);
            var item = recyclables.ElementAt(rng);

            rand_recycle.Add(item);
        }

        return rand_recycle;
    }

    private List<GameObject> GenerateRandomContaminants(int amount)
    {
        List<GameObject> rand_contam = new List<GameObject>();

        for (int i = 1; i <= amount; i++)
        {
            int rng = Random.Range(0, contaminants.Length - 1);
            var item = contaminants.ElementAt(rng);

            rand_contam.Add(item);
        }

        return rand_contam;
    }

    private void Shuffle<T>(IList<T> list)
    {
        for (int i = 0; i < list.Count; i++)
        {
            T tmp = list[i];
            int rng = Random.Range(i, list.Count);
            list[i] = list[rng];
            list[rng] = tmp;
        }
    }
}



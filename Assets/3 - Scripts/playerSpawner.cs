using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerSpawner : MonoBehaviour
{
    public GameObject playerToSpawn;

    private bool doesPlayerExist;

    void Start()
    {
        if (GameObject.FindGameObjectWithTag("Player"))
            doesPlayerExist = true;
        else
            doesPlayerExist = false;
    }

    void Update()
    {
        if (GameObject.FindGameObjectWithTag("Player"))
            doesPlayerExist = true;
        else
            doesPlayerExist = false;

        if (!doesPlayerExist)
        {
            GameObject spawnedPlayer = Instantiate(playerToSpawn, gameObject.transform);
            spawnedPlayer.transform.parent = gameObject.transform;
        }
    }
}

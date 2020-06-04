using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class flockSpawner : MonoBehaviour
{
    public Transform[] spawnPositions;
    public GameObject flock;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < spawnPositions.Length; i++)
        {
            GameObject newFlock = Instantiate(flock, spawnPositions[i].position, spawnPositions[i].rotation);
            newFlock.name = "Flock " + i;
        }
        StartCoroutine(createEnemies());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator createEnemies()
    {
        yield return new WaitForSeconds(30f);
        for (int i = 0; i < spawnPositions.Length; i++)
        {
            GameObject newFlock = Instantiate(flock, spawnPositions[i].position, spawnPositions[i].rotation);
            newFlock.name = "Flock " + i;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogSpawner : MonoBehaviour
{
    public GameObject logPrefab; 
    public Transform[] spawnPoints; 
    public float spawnInterval = 0.5f;
    public float logSpeed = 3f; 

    void Start()
    {
        StartCoroutine(SpawnLogs());
    }

    IEnumerator SpawnLogs()
    {
        while (true)
        {
Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];

            GameObject log = Instantiate(logPrefab, spawnPoint.position, spawnPoint.rotation);

            LogMover logMover = log.AddComponent<LogMover>();
            logMover.speed = logSpeed;

            logMover.moveLeft = Random.Range(0, 2) == 0;

            yield return new WaitForSeconds(spawnInterval);
        }
    }
}

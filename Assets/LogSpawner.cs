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

            // Spawn kayu
            GameObject log = Instantiate(logPrefab, spawnPoint.position, spawnPoint.rotation);

            // Menambahkan skrip penggerak kayu
            LogMover logMover = log.AddComponent<LogMover>();
            logMover.speed = logSpeed;

            // Secara acak atur arah pergerakan (true untuk ke kiri, false untuk ke kanan)
            logMover.moveLeft = Random.Range(0, 2) == 0;

            // Tunggu interval sebelum spawn berikutnya
            yield return new WaitForSeconds(spawnInterval);
        }
    }
}

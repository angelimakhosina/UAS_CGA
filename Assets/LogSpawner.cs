using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogSpawner : MonoBehaviour
{
    public GameObject logPrefab; 
    public Transform[] spawnPoints; 
    public float spawnInterval = 1f; // Interval spawn
    public float minLogSpeed = 1f;     // Kecepatan minimum
    public float maxLogSpeed = 3f;     // Kecepatan maksimum

    void Start()
    {
        StartCoroutine(SpawnLogs());
    }

    IEnumerator SpawnLogs()
    {
        while (true)
        {
            // Pilih spawn point secara acak
            Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];

            // Spawn log dari prefab
            GameObject log = Instantiate(logPrefab, spawnPoint.position, spawnPoint.rotation);

            // Tambahkan script LogMover untuk menggerakkan log
            LogMover logMover = log.AddComponent<LogMover>();
            logMover.speed = Random.Range(minLogSpeed, maxLogSpeed); // Kecepatan random

            // Tentukan arah gerak log berdasarkan posisi X
            float logPositionX = log.transform.position.x;

            // Jika posisi X < 0, bergerak ke kanan, jika posisi X > 0, bergerak ke kiri
            logMover.moveLeft = logPositionX > 0; // Arahkan ke kiri jika berada di posisi positif

            yield return new WaitForSeconds(spawnInterval); // Tunggu sesuai interval spawn
        }
    }
}
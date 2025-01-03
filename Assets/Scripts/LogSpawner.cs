using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogSpawner : MonoBehaviour
{
    public GameObject logPrefab; 
    public Transform[] spawnPoints; // Titik spawn log
    public float spawnInterval = 0.5f; // Interval waktu spawn
    public float logSpeed = 3f; // Kecepatan log
    public float logLifetime = 10f; // Waktu hidup log

    void Start()
    {
        // Panggil coroutine untuk delay 3 detik sebelum mulai spawning
        StartCoroutine(StartSpawningWithDelay());
    }

    // Coroutine untuk delay 3 detik sebelum mulai spawning log
    IEnumerator StartSpawningWithDelay()
    {
        // Tunggu selama 3 detik
        yield return new WaitForSeconds(3f);

        // Setelah 3 detik, mulai spawn logs
        StartCoroutine(SpawnLogs());
    }

    IEnumerator SpawnLogs()
    {
        while (true)
        {
            // Pilih titik spawn secara acak
            Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];

            // Spawn log di titik spawn
            GameObject log = Instantiate(logPrefab, spawnPoint.position, spawnPoint.rotation);

            // Tambahkan LogMover ke log dan atur properti
            LogMover logMover = log.AddComponent<LogMover>();
            logMover.speed = logSpeed;

            // Tentukan arah berdasarkan posisi spawn
            logMover.moveLeft = spawnPoint.position.x > 0; // Jika spawn di kanan, bergerak ke kiri

            // Tentukan waktu log dihancurkan
            logMover.lifetime = logLifetime;

            // Tunggu sebelum spawn log berikutnya
            yield return new WaitForSeconds(spawnInterval);
        }
    }
}

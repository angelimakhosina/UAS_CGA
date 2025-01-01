using System.Collections;
using UnityEngine;

public class LogSpawnManager : MonoBehaviour
{
    public NightLogSpawner nightLogSpawner;
    public NightBrokenLogSpawner nightBrokenLogSpawner;
    public float spawnOffset = 0.25f; // Jeda waktu antar spawner

    void Start()
    {
        StartCoroutine(ManageSpawners());
    }

    IEnumerator ManageSpawners()
    {
        while (true)
        {
            // Jalankan spawner pertama
            nightLogSpawner.SpawnLogOnce();

            // Tunggu jeda sebelum menjalankan spawner kedua
            yield return new WaitForSeconds(spawnOffset);

            // Jalankan spawner kedua
            nightBrokenLogSpawner.SpawnLogOnce();

            // Tunggu interval spawn utama sebelum loop berikutnya
            yield return new WaitForSeconds(nightLogSpawner.spawnInterval - spawnOffset);
        }
    }
}

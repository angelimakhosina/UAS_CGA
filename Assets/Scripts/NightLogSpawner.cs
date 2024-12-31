using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NightLogSpawner : MonoBehaviour
{
    public GameObject logPrefab; 
    public Transform[] spawnPoints; 
    public float spawnInterval = 0.5f; // Interval spawn

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
            logMover.speed = 3f; // Set constant speed

            // Tentukan arah gerak log berdasarkan posisi X
            float logPositionX = log.transform.position.x;

            // Jika posisi X < 0, bergerak ke kanan, jika posisi X > 0, bergerak ke kiri
            logMover.moveLeft = logPositionX > 0; // Arahkan ke kiri jika berada di posisi positif

            // Ubah warna log untuk day mode (kecerahan normal)
            SetNormalColor(log);

            Destroy(log, 1000f);

            yield return new WaitForSeconds(spawnInterval); // Tunggu sesuai interval spawn
        }
    }

    void SetNormalColor(GameObject log)
    {
        // Ambil semua renderer di log
        Renderer[] renderers = log.GetComponentsInChildren<Renderer>();
        foreach (Renderer renderer in renderers)
        {
            // Set warna normal (jika ingin kecerahan tetap)
            Color originalColor = renderer.material.color;
            renderer.material.color = originalColor; // Tidak mengubah kecerahan, tetap dengan warna asli
        }
    }
}
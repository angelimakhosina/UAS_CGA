using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogSpawnerNight : MonoBehaviour
{
    public GameObject logPrefab; 
    public Transform[] spawnPoints; 
    public float spawnInterval = 1.5f; // Interval spawn
    public float minLogSpeed = 1f;     // Kecepatan minimum
    public float maxLogSpeed = 3f;     // Kecepatan maksimum

    public int nightModeLayer = 7;     // Layer untuk Night Mode (pastikan diatur di Project Settings)

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

            // Assign layer Night Mode ke log yang di-spawn
            AssignLayerToLog(log, nightModeLayer);

            // Tambahkan script LogMover untuk menggerakkan log
            LogMover logMover = log.AddComponent<LogMover>();
            logMover.speed = Random.Range(minLogSpeed, maxLogSpeed); // Kecepatan random

            // Tentukan arah gerak log berdasarkan posisi X
            float logPositionX = log.transform.position.x;
            logMover.moveLeft = (logPositionX > 0); // Jika posisi X > 0, maka gerak ke kiri (moveLeft = true)

            // Ubah warna log secara dinamis untuk dark mode
            SetDarkColor(log);

            yield return new WaitForSeconds(spawnInterval); // Tunggu sesuai interval spawn
        }
    }

    void SetDarkColor(GameObject log)
    {
        // Ambil semua renderer di log
        Renderer[] renderers = log.GetComponentsInChildren<Renderer>();
        foreach (Renderer renderer in renderers)
        {
            // Kurangi kecerahan warna asli menjadi lebih gelap
            Color originalColor = renderer.material.color;
            renderer.material.color = originalColor * 0.75f; // 75% dari kecerahan asli
        }
    }

    void AssignLayerToLog(GameObject log, int layer)
    {
        // Assign layer ke gameobject utama
        log.layer = layer;

        // Assign layer ke semua child gameobject
        foreach (Transform child in log.transform)
        {
            child.gameObject.layer = layer;
        }
    }
}

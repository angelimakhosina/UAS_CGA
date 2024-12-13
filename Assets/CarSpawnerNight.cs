using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarSpawnerNight : MonoBehaviour
{
    public GameObject carPrefab; 
    public Transform[] spawnPoints; 
    public float spawnInterval = 1f; // Interval spawn tetap 1 detik

    public int nightModeLayer = 7; // Layer untuk Night Mode (pastikan layer ini diatur di Project Settings)

    public Light leftSpotlightPrefab; // Tambahkan Spotlight kiri prefab untuk setiap mobil
    public Light rightSpotlightPrefab; // Tambahkan Spotlight kanan prefab untuk setiap mobil

    void Start()
    {
        StartCoroutine(SpawnCars());
    }

    IEnumerator SpawnCars()
    {
        while (true)
        {
            // Pilih spawn point secara acak
            Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];

            // Spawn mobil dari prefab
            GameObject car = Instantiate(carPrefab, spawnPoint.position, spawnPoint.rotation);

            // Assign layer Night Mode ke mobil yang di-spawn
            AssignLayerToCar(car, nightModeLayer);

            // Tambahkan spotlight kiri dan kanan ke mobil yang di-spawn
            AddSpotlightsToCar(car);

            // Tambahkan script CarMover untuk menggerakkan mobil
            float randomSpeed = Random.Range(5f, 15f); // Kecepatan random dari 5 - 15
            car.AddComponent<CarMover>().speed = randomSpeed;

            // Ubah warna mobil secara dinamis untuk dark mode
            SetDarkColor(car);

            yield return new WaitForSeconds(spawnInterval); // Interval spawn tetap 1 detik
        }
    }

    void SetDarkColor(GameObject car)
    {
        // Ambil semua renderer di mobil
        Renderer[] renderers = car.GetComponentsInChildren<Renderer>();
        foreach (Renderer renderer in renderers)
        {
            // Kurangi kecerahan warna asli menjadi lebih gelap
            Color originalColor = renderer.material.color;
            renderer.material.color = originalColor * 0.75f; // 75% dari kecerahan asli
        }
    }

    void AssignLayerToCar(GameObject car, int layer)
    {
        // Assign layer ke gameobject utama
        car.layer = layer;

        // Assign layer ke semua child gameobject
        foreach (Transform child in car.transform)
        {
            child.gameObject.layer = layer;
        }
    }

    void AddSpotlightsToCar(GameObject car)
    {
        // Cari posisi untuk spotlight kiri dan kanan
        Transform leftSpotlightPosition = car.transform.Find("Spot Light L");  // Posisi untuk spotlight kiri
        Transform rightSpotlightPosition = car.transform.Find("Spot Light R"); // Posisi untuk spotlight kanan

        if (leftSpotlightPosition != null && rightSpotlightPosition != null)
        {
            // Spawn spotlight kiri sebagai child dari mobil
            Light leftSpotlight = Instantiate(leftSpotlightPrefab, leftSpotlightPosition.position, leftSpotlightPosition.rotation);
            leftSpotlight.transform.SetParent(car.transform); // Tempatkan spotlight kiri sebagai child dari mobil
            leftSpotlight.intensity = 2f; // Atur intensitas lampu sesuai keinginan
            leftSpotlight.range = 10f;    // Atur jarak pencahayaan sesuai kebutuhan
            leftSpotlight.spotAngle = 30f; // Atur sudut spotlight (opsional)

            // Spawn spotlight kanan sebagai child dari mobil
            Light rightSpotlight = Instantiate(rightSpotlightPrefab, rightSpotlightPosition.position, rightSpotlightPosition.rotation);
            rightSpotlight.transform.SetParent(car.transform); // Tempatkan spotlight kanan sebagai child dari mobil
            rightSpotlight.intensity = 2f; // Atur intensitas lampu sesuai keinginan
            rightSpotlight.range = 10f;    // Atur jarak pencahayaan sesuai kebutuhan
            rightSpotlight.spotAngle = 30f; // Atur sudut spotlight (opsional)
        }
        else
        {
            // Debug.LogWarning("Spotlight positions (Left/Right) not found on the car prefab!");
        }
    }
}
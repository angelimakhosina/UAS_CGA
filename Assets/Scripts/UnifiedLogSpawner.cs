using System.Collections;
using UnityEngine;

public class UnifiedLogSpawner : MonoBehaviour
{
    public GameObject normalLogPrefab; // Prefab untuk log normal
    public GameObject brokenLogPrefab; // Prefab untuk log broken
    public Transform[] spawnPoints;   // Posisi spawn
    public float spawnInterval = 2f; // Interval waktu spawn
    public float brokenLogProbability = 0.5f; // Peluang spawn broken log (0.0 - 1.0)

    void Start()
    {
        StartCoroutine(SpawnLogs());
    }

    IEnumerator SpawnLogs()
    {
        while (true)
        {
            // Pilih secara acak antara log normal atau broken
            bool isBrokenLog = Random.value < brokenLogProbability;

            // Pilih spawn point secara acak
            Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];

            // Tentukan prefab yang akan digunakan
            GameObject selectedPrefab = isBrokenLog ? brokenLogPrefab : normalLogPrefab;

            // Spawn log dari prefab yang dipilih
            GameObject log = Instantiate(selectedPrefab, spawnPoint.position, spawnPoint.rotation);

            // Tambahkan script LogMover untuk pergerakan
            LogMover logMover = log.AddComponent<LogMover>();
            logMover.speed = 3f;

            // Tentukan arah gerak berdasarkan posisi X
            float logPositionX = log.transform.position.x;
            logMover.moveLeft = logPositionX > 0;

            // Atur log sesuai jenisnya
            if (isBrokenLog)
            {
                StartCoroutine(HandleBrokenLog(log));
            }
            else
            {
                SetNormalColor(log);
            }

            // Hancurkan log setelah waktu tertentu
            Destroy(log, isBrokenLog ? 50f : 1000f);

            // Tunggu sebelum spawn berikutnya
            yield return new WaitForSeconds(spawnInterval);
        }
    }

    void SetNormalColor(GameObject log)
    {
        Renderer[] renderers = log.GetComponentsInChildren<Renderer>();
        foreach (Renderer renderer in renderers)
        {
            Color originalColor = renderer.material.color;
            renderer.material.color = originalColor; // Tidak mengubah kecerahan
        }
    }

    IEnumerator HandleBrokenLog(GameObject log)
    {
        // Tunggu waktu sebelum broken log jatuh
        yield return new WaitForSeconds(Random.Range(7f, 10f));
        StartCoroutine(FallDown(log));
    }

    IEnumerator FallDown(GameObject log)
    {
        while (log.transform.position.y > -10f) // Atur batas bawah posisi jatuh
        {
            log.transform.position += Vector3.down * Time.deltaTime * 1f; // Kecepatan jatuh
            yield return null;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NightLogSpawner : MonoBehaviour
{
    public GameObject logPrefab;
    public Transform[] spawnPoints;
    public float spawnInterval = 0.5f;

    public void SpawnLogOnce()
    {
        Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
        GameObject log = Instantiate(logPrefab, spawnPoint.position, spawnPoint.rotation);

        LogMover logMover = log.AddComponent<LogMover>();
        logMover.speed = 3f;
        logMover.moveLeft = log.transform.position.x > 0;

        SetNormalColor(log);
        Destroy(log, 1000f);
    }

    void SetNormalColor(GameObject log)
    {
        Renderer[] renderers = log.GetComponentsInChildren<Renderer>();
        foreach (Renderer renderer in renderers)
        {
            Color originalColor = renderer.material.color;
            renderer.material.color = originalColor;
        }
    }
}
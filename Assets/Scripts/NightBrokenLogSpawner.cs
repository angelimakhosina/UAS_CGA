using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NightBrokenLogSpawner : MonoBehaviour
{
    public GameObject logPrefab;
    public Transform[] spawnPoints;

    public void SpawnLogOnce()
    {
        Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
        GameObject log = Instantiate(logPrefab, spawnPoint.position, spawnPoint.rotation);

        LogMover logMover = log.AddComponent<LogMover>();
        logMover.speed = 3f;
        logMover.moveLeft = log.transform.position.x > 0;

        SetNormalColor(log);
        StartCoroutine(HandleLogFall(log));
        Destroy(log, 50f);
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

    IEnumerator HandleLogFall(GameObject log)
    {
        // Wait for a random delay before triggering the effect
        yield return new WaitForSeconds(Random.Range(7f, 10f));

        // Make the log fall
        MakeLogFall(log);
    }

    void MakeLogFall(GameObject log)
    {
        // Change the Y position to make the log fall down
        StartCoroutine(FallDown(log));
    }

    IEnumerator FallDown(GameObject log)
    {
        while (log.transform.position.y > -10f) // Adjust the -10f to your desired ground level
        {
            log.transform.position += Vector3.down * Time.deltaTime * 1f; // Adjust the speed as needed
            yield return null;
        }
    }

    void BreakLog(GameObject log)
    {
        // Simulate breaking effect (destroy the log)
        Debug.Log("Log breaks!");

        // Optional: Add visual effects for breaking (e.g., particles)
        // Destroy the log
        Destroy(log);
    }
}
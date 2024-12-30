using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrokenLogSpawner : MonoBehaviour
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
            // Choose a random spawn point
            Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];

            // Instantiate the log
            GameObject log = Instantiate(logPrefab, spawnPoint.position, spawnPoint.rotation);

            // Add movement behavior
            LogMover logMover = log.AddComponent<LogMover>();
            logMover.speed = 3f; // Set constant speed

            // Determine the movement direction based on X position
            float logPositionX = log.transform.position.x;
            logMover.moveLeft = logPositionX > 0;

            // Apply normal color
            SetNormalColor(log);

            // Always make the log fall after a random time
            StartCoroutine(HandleLogFall(log));

            // Destroy the log after 25 seconds if it doesn't break
            Destroy(log, 25f);

            yield return new WaitForSeconds(spawnInterval);
        }
    }

    void SetNormalColor(GameObject log)
    {
        // Get all renderers in the log
        Renderer[] renderers = log.GetComponentsInChildren<Renderer>();
        foreach (Renderer renderer in renderers)
        {
            // Set normal color (keep original brightness)
            Color originalColor = renderer.material.color;
            renderer.material.color = originalColor; // Keep the original color
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

    public class LogMover : MonoBehaviour
    {
        public float speed = 1f;
        public bool moveLeft = false;

        void Update()
        {
            if (moveLeft)
            {
                transform.position += Vector3.left * speed * Time.deltaTime;
            }
            else
            {
                transform.position += Vector3.right * speed * Time.deltaTime;
            }
        }
    }
}
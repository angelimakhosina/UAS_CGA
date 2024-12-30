using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NightCarSpawner : MonoBehaviour
{
    public GameObject carPrefab; 
    public Transform[] spawnPoints; 
    public float spawnInterval = 0.5f;
    public float carSpeed = 5f;

    void Start()
    {
        StartCoroutine(SpawnCars());
    }

    IEnumerator SpawnCars()
    {
        while (true)
        {
            // Choose a random spawn point
            Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];

            // Instantiate the car
            GameObject car = Instantiate(carPrefab, spawnPoint.position, spawnPoint.rotation);

            // Add movement behavior to the car
            CarMover carMover = car.AddComponent<CarMover>();
            carMover.speed = Random.Range(3f, 7f); // Set random speed between 3 and 7

            // Add the spotlight immediately
            AddSpotlight(car);

            // Destroy the car after 15 seconds
            Destroy(car, 15f);

            yield return new WaitForSeconds(spawnInterval);
        }
    }

    void AddSpotlight(GameObject car)
    {
        // Add a single spotlight as a headlight
        GameObject spotlight = new GameObject("Headlight");
        Light lightComponent = spotlight.AddComponent<Light>();
        lightComponent.type = LightType.Spot;
        lightComponent.intensity = 2f;
        lightComponent.range = 20f;
        lightComponent.spotAngle = 35f;

        // Parent the spotlight to the car
        spotlight.transform.SetParent(car.transform);

        // Position the spotlight at the center of the car
        spotlight.transform.localPosition = new Vector3(0f, 0.5f, 1.5f);

        // Rotate the spotlight to face forward
        spotlight.transform.localRotation = Quaternion.Euler(20f, 0f, 0f);
    }
}
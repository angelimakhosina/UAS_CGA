using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarSpawner : MonoBehaviour
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
            Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];

            GameObject car = Instantiate(carPrefab, spawnPoint.position, spawnPoint.rotation);

            car.AddComponent<CarMover>().speed = carSpeed;

            yield return new WaitForSeconds(spawnInterval);
        }
    }
}

using UnityEngine;
using System.Collections; 

public class FrogCollision : MonoBehaviour
{
    private Vector3 initialPosition;

    void Start()
    {
        // Catat posisi awal katak
        initialPosition = transform.position;
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Jika bertabrakan dengan mobil
        if (collision.gameObject.CompareTag("Car"))
        {
            Respawn();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // Jika memasuki area air
        if (other.gameObject.CompareTag("Water"))
        {
            // Tunggu beberapa detik agar efek menyelam terlihat sebelum respawn
            StartCoroutine(RespawnAfterDelay(0.5f)); // 0.5 detik delay
        }
    }

    private IEnumerator RespawnAfterDelay(float delay)
    {
        // Tunggu selama delay
        yield return new WaitForSeconds(delay);

        // Respawn ke posisi awal
        transform.position = initialPosition;
    }

    private void Respawn()
    {
        // Kembalikan posisi katak langsung
        transform.position = initialPosition;
    }
}
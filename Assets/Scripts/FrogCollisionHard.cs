using UnityEngine;
using System.Collections;

public class FrogCollisionHard : MonoBehaviour
{
    private Vector3 initialPosition;
    private GameManagerHard gameManagerHard;

    [SerializeField]
    private float boundaryX = 20f; 

    void Start()
    {
        // Catat posisi awal katak
        initialPosition = transform.position;

        // Temukan GameManagerHard di scene
        gameManagerHard = FindObjectOfType<GameManagerHard>();

        if (gameManagerHard == null)
        {
            Debug.LogError("GameManagerHard tidak ditemukan di scene!");
        }
    }

    void Update()
    {
        // Periksa apakah posisi x melewati batas
        if (Mathf.Abs(transform.position.x) > boundaryX)
        {
            HandleDeath();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Jika bertabrakan dengan mobil
        if (collision.gameObject.CompareTag("Car"))
        {
            HandleDeath();
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

        HandleDeath();
    }

    private void HandleDeath()
    {
        if (gameManagerHard != null)
        {
            gameManagerHard.FrogDied(); // Kurangi nyawa
        }

        Respawn(); // Kembalikan ke posisi awal
    }

    private void Respawn()
    {
        // Kembalikan posisi katak langsung
        transform.position = initialPosition;
    }
}

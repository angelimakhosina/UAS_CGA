using UnityEngine;

public class LogMover : MonoBehaviour
{
    public float speed = 3f; // Kecepatan log
    public bool moveLeft = true; // Arah awal
    public float lifetime = 1000f; // Waktu sebelum log dihancurkan

    void Start()
    {
        // Hancurkan log setelah waktu tertentu
        Destroy(gameObject, lifetime);
    }

    void Update()
    {
        // Gerakan log berdasarkan arah
        float direction = moveLeft ? -1 : 1;
        transform.Translate(Vector3.right * direction * speed * Time.deltaTime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player")) // Jika katak menyentuh log
        {
            Transform playerTransform = collision.transform;

            // Pastikan transformasi global tetap saat menjadi parent
            playerTransform.SetParent(transform, true); 

            Debug.Log("Katak menjadi child dari log.");
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player")) // Jika katak meninggalkan log
        {
            Transform playerTransform = collision.transform;

            // Lepaskan parent dan pastikan objek tidak null
            if (playerTransform != null)
            {
                playerTransform.SetParent(null, true);
                Debug.Log("Katak dilepaskan dari log.");
            }
        }
    }

    void OnDestroy()
    {
        // Lepaskan semua child sebelum log dihancurkan
        foreach (Transform child in transform)
        {
            if (child != null)
            {
                child.SetParent(null, true);
                // Debug.Log($"Child {child.name} dilepaskan karena log dihancurkan.");
            }
        }
    }
}
using UnityEngine;

public class LogMover : MonoBehaviour
{
    public float speed = 3f; // Kecepatan log
    public bool moveLeft = true; // Arah awal
    public float lifetime = 10f; // Waktu sebelum log dihancurkan

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

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player")) // Katak di atas log
        {
            // Gerakkan katak bersama log
            float direction = moveLeft ? -1 : 1;
            collision.transform.Translate(Vector3.right * direction * speed * Time.deltaTime, Space.World);
        }
    }
}
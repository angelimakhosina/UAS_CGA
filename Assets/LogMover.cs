using UnityEngine;

public class LogMover : MonoBehaviour
{
    public float speed = 3f; // Kecepatan kayu
    public bool moveLeft = true; // Arah gerak kayu (true = kiri, false = kanan)

    void Update()
    {
        // Gerakkan kayu ke arah kiri atau kanan pada sumbu X lokal
        float direction = moveLeft ? -1 : 1; // -1 untuk kiri, 1 untuk kanan
        transform.Translate(Vector3.right * direction * speed * Time.deltaTime);
    }
}